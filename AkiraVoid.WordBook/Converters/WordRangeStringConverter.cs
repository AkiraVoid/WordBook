using System;
using System.Collections.Generic;
using System.Linq;
using AkiraVoid.WordBook.Enums;
using Microsoft.UI.Xaml.Data;

namespace AkiraVoid.WordBook.Converters;

/// <summary>
/// 用于将 WordRange 枚举转换成对应的字符串，或反之。
/// </summary>
public class WordRangeStringConverter : IValueConverter
{
    private readonly Dictionary<DictationWordRange, string> _rangeMap = new()
    {
        { DictationWordRange.All, "所有单词" },
        { DictationWordRange.Random, "随机单词" },
        { DictationWordRange.Today, "今天登记的单词" },
        { DictationWordRange.Yesterday, "昨天登记的单词" },
        { DictationWordRange.Important, "标记为重要的单词" }
    };

    /// <inheritdoc />
    public object Convert(
        object value,
        Type targetType,
        object parameter,
        string language)
    {
        return _rangeMap[(DictationWordRange)value];
    }

    /// <inheritdoc />
    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        string language)
    {
        return _rangeMap.FirstOrDefault(range => range.Value == (string)value).Key;
    }
}