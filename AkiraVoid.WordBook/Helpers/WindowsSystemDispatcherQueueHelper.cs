using System.Runtime.InteropServices;

namespace AkiraVoid.WordBook.Helpers
{
    /// <summary>
    /// 参考 <see href="winui3gallery://item/SystemBackdrops">WinUI 3 Gallery</see> 上的示例。
    /// </summary>
    internal class WindowsSystemDispatcherQueueHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        struct DispatcherQueueOptions
        {
            internal int _dwSize;
            internal int _threadType;
            internal int _apartmentType;
        }

        [DllImport("CoreMessaging.dll")]
        private static extern int CreateDispatcherQueueController([In] DispatcherQueueOptions options, [In, Out, MarshalAs(UnmanagedType.IUnknown)] ref object dispatcherQueueController);

        object _dispatcherQueueController = null;

        public void EnsureWindowsSystemDispatcherQueueController()
        {
            if (Windows.System.DispatcherQueue.GetForCurrentThread() != null)
            {
                return;
            }
            if (_dispatcherQueueController == null)
            {
                DispatcherQueueOptions options;
                options._dwSize = Marshal.SizeOf(typeof(DispatcherQueueOptions));
                options._threadType = 2; // DQTYPE_THREAD_CURRENT
                options._apartmentType = 2; // DQTAT_COM_STA
                CreateDispatcherQueueController(options, ref _dispatcherQueueController);
            }
        }

    }
}
