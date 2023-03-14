using System;

namespace AkiraVoid.WordBook.ViewModels;

public class CountChangedEventArgs : EventArgs
{
    /// <summary>
    /// 先前的数量。
    /// </summary>
    public int Previous { get; set; }

    /// <summary>
    /// 现在的数量。
    /// </summary>
    public int Now { get; set; }
}