using System;
using Microsoft.UI.Xaml.Data;

namespace AkiraVoid.WordBook.Converters;

/// <summary>
/// 该转换器只用于帮助调试
/// </summary>
public class DebugConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        string language)
    {
        return value;
    }

    /// <inheritdoc />
    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        string language)
    {
        return value;
    }
}