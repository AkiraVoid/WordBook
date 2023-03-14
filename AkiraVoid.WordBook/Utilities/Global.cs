using System.Collections.Generic;
using System.Collections.ObjectModel;
using AkiraVoid.WordBook.Helpers;
using AkiraVoid.WordBook.Models;

namespace AkiraVoid.WordBook.Utilities;

public static class Global
{
    /// <summary>
    /// 全局数据库实例。
    /// </summary>
    public static readonly WordBankContext WordBank = new();

#pragma warning disable CA2211
    // ReSharper disable once InconsistentNaming
    public static UIHelper UIHelper = new();

    /// <summary>
    /// 全局词汇列表。
    /// </summary>
    public static ObservableCollection<Word> WordList = new();

    /// <summary>
    /// 全局词性列表。
    /// </summary>
    public static List<PartOfSpeech> PartsOfSpeech = new();

    /// <summary>
    /// RootPage 所使用的导航器。
    /// </summary>
    public static Navigator Navigator;
#pragma warning restore CA2211
}