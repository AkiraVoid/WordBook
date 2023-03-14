using System;
using Microsoft.UI.Xaml;

namespace AkiraVoid.WordBook.ViewModels;

public class WordListRoutedEventArgs : RoutedEventArgs
{
    public new object OriginalSource { get; }
    public Guid WordId { get; set; }

    public WordListRoutedEventArgs(RoutedEventArgs args)
    {
        OriginalSource = args.OriginalSource;
    }
}