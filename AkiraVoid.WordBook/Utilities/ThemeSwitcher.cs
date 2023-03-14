using System;
using AkiraVoid.WordBook.Helpers;
using Microsoft.UI;
using Microsoft.UI.Xaml;

namespace AkiraVoid.WordBook.Utilities;

/// <summary>
/// 提供一系列用于操作应用主题的方法。
/// </summary>
public static class ThemeSwitcher
{
    /// <summary>
    /// 切换应用主题到目标主题。
    /// </summary>
    /// <param name="theme">目标主题。</param>
    /// <param name="root">要应用主题的元素。</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="theme"/> 的值不在 <see cref="ElementTheme"/> 的可能值范围内。</exception>
    public static void SwitchTheme(ElementTheme theme, FrameworkElement root)
    {
        root.RequestedTheme = theme;
        var resources = root.Resources;
        var captionButtonColor = theme switch
        {
            ElementTheme.Default => Win32.ShouldSystemUseDarkMode() ? Colors.White : Colors.Black,
            ElementTheme.Light => Colors.Black,
            ElementTheme.Dark => Colors.White,
            _ => throw new ArgumentOutOfRangeException(nameof(theme), theme, null)
        };

        resources["WindowCaptionForeground"] = captionButtonColor;
        UIHelper.TriggerTitleBarRepaint(Global.UIHelper.GetWindow("MainWindow"));

        Configuration.SetConfiguration("theme", theme.ToString());
    }

    /// <summary>
    /// 切换应用主题到配置项中配置的主题。
    /// </summary>
    /// <param name="root">要应用主题的元素。</param>
    /// <remarks>
    /// 如果配置项中没有配置主题，则默认跟随系统主题，并将其记录在配置项中。
    /// </remarks>
    public static void SwitchTheme(FrameworkElement root)
    {
        var themeSetting = Configuration.GetConfiguration("theme");
        if (!Enum.TryParse<ElementTheme>(themeSetting, out var theme))
        {
            theme = ElementTheme.Default;
            Configuration.SetConfiguration("theme", theme.ToString());
        }

        SwitchTheme(theme, root);
    }

    /// <summary>
    /// 获取现在配置项中配置的主题。
    /// </summary>
    /// <returns>配置项中配置的主题。</returns>
    /// <remarks>
    /// 如果配置项中没有配置主题，则默认跟随系统主题，并将其记录在配置项中。
    /// </remarks>
    public static ElementTheme GetTheme()
    {
        var themeSetting = Configuration.GetConfiguration("theme");
        if (!Enum.TryParse<ElementTheme>(themeSetting, out var theme))
        {
            theme = ElementTheme.Default;
            Configuration.SetConfiguration("theme", theme.ToString());
        }

        return theme;
    }
}