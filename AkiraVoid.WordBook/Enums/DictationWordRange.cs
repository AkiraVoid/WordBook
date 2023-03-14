namespace AkiraVoid.WordBook.Enums;

/// <summary>
/// 指示要听写的单词范围。
/// </summary>
public enum DictationWordRange
{
    /// <summary>
    /// 随机选择单词，除了标记为已记住的单词。
    /// </summary>
    Random,

    /// <summary>
    /// 选择所有单词，包括标记为已记住的单词。
    /// </summary>
    All,

    /// <summary>
    /// 选择今天登记的单词，除了标记为已记住的单词。
    /// </summary>
    Today,

    /// <summary>
    /// 选择昨天登记的单词，除了标记为已记住的单词。
    /// </summary>
    Yesterday,

    /// <summary>
    /// 选择标记为重要的单词，除了标记为已记住的单词。
    /// </summary>
    Important
}