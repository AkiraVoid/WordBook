using System;
using System.Runtime.InteropServices;

namespace AkiraVoid.WordBook.Utilities
{
    /// <summary>
    /// 提供一系列与 Win32 接口操作的方法。
    /// </summary>
    internal static class Win32
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            IntPtr lParam);

        [DllImport("user32.dll")] public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll")] public static extern IntPtr GetActiveWindow();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(IntPtr moduleName);

        [DllImport("UXTheme.dll", SetLastError = true, EntryPoint = "#138")]
        public static extern bool ShouldSystemUseDarkMode();

        public const int WM_ACTIVATE = 0x0006;
        public const int WA_ACTIVE = 0x01;
        public const int WA_INACTIVE = 0x00;

        public const int WM_SETICON = 0x0080;
        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
    }
}