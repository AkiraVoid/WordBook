using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace AkiraVoid.WordBook.Models;

[Table("explanations")]
public class WordExplanation : INotifyPropertyChanged
{
    [NotMapped] private Guid _id;
    [NotMapped] private string _explanation;
    [NotMapped] private string _translation;
    [NotMapped] private Guid _wordId;
    [NotMapped] private Guid _partOfSpeechId;
    [NotMapped] private Word _word;
    [NotMapped] private PartOfSpeech _partOfSpeech;

    /// <summary>
    /// 检查该实例是否为一个实践意义上的空实例。
    /// </summary>
    /// <returns>如果是空实例则返回 <see langword="true"/>，否则返回 <see langword="false"/>。</returns>
    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(Explanation) || string.IsNullOrEmpty(Translation) || PartOfSpeech is null;
    }

    [Key]
    [Column("id")]
    public Guid Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    [Column("explanation")]
    public string Explanation
    {
        get => _explanation;
        set => SetField(ref _explanation, value);
    }

    [Column("translation")]
    public string Translation
    {
        get => _translation;
        set => SetField(ref _translation, value);
    }

    [Column("word_id")]
    public Guid WordId
    {
        get => _wordId;
        set => SetField(ref _wordId, value);
    }

    [Column("part_of_speech_id")]
    public Guid PartOfSpeechId
    {
        get => _partOfSpeechId;
        set => SetField(ref _partOfSpeechId, value);
    }

    public Word Word
    {
        get => _word;
        set => SetField(ref _word, value);
    }

    public PartOfSpeech PartOfSpeech
    {
        get => _partOfSpeech;
        set => SetField(ref _partOfSpeech, value);
    }

    public WordExplanation(Guid wordId)
    {
        WordId = wordId;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}