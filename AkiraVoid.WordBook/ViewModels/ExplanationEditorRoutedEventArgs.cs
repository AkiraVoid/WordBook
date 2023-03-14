using AkiraVoid.WordBook.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace AkiraVoid.WordBook.ViewModels;

public class ExplanationEditorRoutedEventArgs : RoutedEventArgs
{
    /// <inheritdoc cref="RoutedEventArgs.OriginalSource"/>
    public new object OriginalSource { get; } = null;

    public AppBarButton ActionButton { get; set; }
    public WordExplanation Explanation { get; set; }

    public ExplanationEditorRoutedEventArgs()
    {
    }

    public ExplanationEditorRoutedEventArgs(RoutedEventArgs args)
    {
        OriginalSource = args.OriginalSource;
    }
}