using AkiraVoid.WordBook.Utilities;
using Microsoft.UI.Composition;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;

namespace AkiraVoid.WordBook.Helpers
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// 提供用于简化部分 UI 操作的方法的类。
    /// </summary>
    public class UIHelper
    {
        private WindowsSystemDispatcherQueueHelper _queueHelper;
        private MicaController _micaController;
        private DesktopAcrylicController _acrylicController;
        private SystemBackdropConfiguration _backdropConfiguration;
        private readonly Dictionary<string, Window> _windows = new();

        /// <summary>
        /// 使对应的窗口标题栏重新渲染。
        ///
        /// 该方法通常在应用主题切换时自动调用，以更新右上角
        /// 窗口控制按钮的颜色。
        /// </summary>
        /// <param name="window">要操作的窗口。</param>
        public static void TriggerTitleBarRepaint(Window window)
        {
            var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
            var activeWindow = Win32.GetActiveWindow();
            if (windowHandle == activeWindow)
            {
                Win32.SendMessage(
                    windowHandle,
                    Win32.WM_ACTIVATE,
                    Win32.WA_INACTIVE,
                    IntPtr.Zero);
                Win32.SendMessage(
                    windowHandle,
                    Win32.WM_ACTIVATE,
                    Win32.WA_ACTIVE,
                    IntPtr.Zero);
            }
            else
            {
                Win32.SendMessage(
                    windowHandle,
                    Win32.WM_ACTIVATE,
                    Win32.WA_ACTIVE,
                    IntPtr.Zero);
                Win32.SendMessage(
                    windowHandle,
                    Win32.WM_ACTIVATE,
                    Win32.WA_INACTIVE,
                    IntPtr.Zero);
            }
        }

        /// <summary>
        /// 尝试将窗口背景设置成云母材质。
        /// </summary>
        /// <param name="window">要操作的窗口。</param>
        /// <returns>设置成功返回 <see langword="true" />，否则返回 <see langword="false" />。</returns>
        public bool TrySetMicaBackdrop(Window window)
        {
            if (MicaController.IsSupported())
            {
                _queueHelper = new();
                _queueHelper.EnsureWindowsSystemDispatcherQueueController();

                _backdropConfiguration = new();
                window.Activated += Window_Activated;
                window.Closed += Window_Closed;
                var windowContent = (FrameworkElement)window.Content;
                windowContent.ActualThemeChanged += Window_ThemeChanged;

                _backdropConfiguration.IsInputActive = true;
                SetConfigurationSourceTheme(windowContent);

                _micaController = new();

                _micaController.AddSystemBackdropTarget(window as ICompositionSupportsSystemBackdrop);
                _micaController.SetSystemBackdropConfiguration(_backdropConfiguration);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 尝试将窗口背景设置成亚克力材质。
        /// </summary>
        /// <param name="window">要操作的窗口。</param>
        /// <returns>设置成功返回 <see langword="true" />，否则返回 <see langword="false" />。</returns>
        public bool TrySetAcrylicBackdrop(Window window)
        {
            if (DesktopAcrylicController.IsSupported())
            {
                _queueHelper = new();
                _queueHelper.EnsureWindowsSystemDispatcherQueueController();

                _backdropConfiguration = new();
                window.Activated += Window_Activated;
                window.Closed += Window_Closed;
                var windowContent = (FrameworkElement)window.Content;
                windowContent.ActualThemeChanged += Window_ThemeChanged;

                _backdropConfiguration.IsInputActive = true;
                SetConfigurationSourceTheme(windowContent);

                _acrylicController = new();

                _acrylicController.AddSystemBackdropTarget(window as ICompositionSupportsSystemBackdrop);
                _acrylicController.SetSystemBackdropConfiguration(_backdropConfiguration);
                return true;
            }

            return false;
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            _backdropConfiguration.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            if (_micaController != null)
            {
                _micaController.Dispose();
                _micaController = null;
            }

            if (_acrylicController != null)
            {
                _acrylicController.Dispose();
                _acrylicController = null;
            }

            (sender as Window)!.Activated -= Window_Activated;
            _backdropConfiguration = null;
        }

        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (_backdropConfiguration != null)
            {
                SetConfigurationSourceTheme(sender);
            }
        }

        /// <summary>
        /// 根据内容的主题设置云母或亚克力材质的主题。
        /// </summary>
        /// <param name="content">主题发生变化的内容。</param>
        private void SetConfigurationSourceTheme(FrameworkElement content)
        {
            _backdropConfiguration.Theme = content.ActualTheme switch
            {
                ElementTheme.Dark => SystemBackdropTheme.Dark,
                ElementTheme.Light => SystemBackdropTheme.Light,
                ElementTheme.Default => SystemBackdropTheme.Default,
                _ => SystemBackdropTheme.Default,
            };
        }

        /// <summary>
        /// 创建一个 <see cref="Window"/> 实例，并将其加入到监控中。
        /// </summary>
        /// <param name="windowName">该 <see cref="Window"/> 实例的名称。</param>
        /// <returns>创建的实例。</returns>
        public Window CreateWindow(string windowName)
        {
            var window = new Window();
            _windows.Add(windowName, window);
            return window;
        }

        /// <summary>
        /// 创建一个 <see cref="Window"/> 实例，并将其加入到监控中。
        /// </summary>
        /// <param name="windowName">该 <see cref="Window"/> 实例的名称。</param>
        /// <param name="callback">创建完成后触发的操作。</param>
        /// <returns>创建的实例。</returns>
        public Window CreateWindow(string windowName, Action<Window> callback)
        {
            var window = CreateWindow(windowName);
            callback(window);
            return window;
        }

        /// <summary>
        /// 根据 <see cref="Window"/> 实例名称获取对应的实例。
        /// </summary>
        /// <param name="windowName"><see cref="Window"/>实例名称。</param>
        /// <returns>对应的实例。</returns>
        public Window GetWindow(string windowName)
        {
            return _windows[windowName];
        }

        /// <summary>
        /// 尝试根据 <see cref="Window"/> 实例名称获取对应的实例。
        /// </summary>
        /// <param name="windowName">实例名称。</param>
        /// <param name="window">用于接收对应实例的变量。</param>
        /// <returns>获取成功返回 <see langword="true" />，否则返回 <see langword="false" />。</returns>
        public bool TryGetWindow(string windowName, out Window window)
        {
            if (_windows.ContainsKey(windowName))
            {
                window = _windows[windowName];
                return true;
            }

            window = null;
            return false;
        }
    }
}