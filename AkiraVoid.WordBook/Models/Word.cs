using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using AkiraVoid.WordBook.Enums;

namespace AkiraVoid.WordBook.Models;

[Table("words")]
public class Word : INotifyPropertyChanged
{
    [NotMapped] private Guid _id = Guid.NewGuid();
    [NotMapped] private string _spell;
    [NotMapped] private WordLanguage _language;
    [NotMapped] private DateTime _addedAt = DateTime.Now;
    [NotMapped] private DateTime _lastMemorizedAt = default;
    [NotMapped] private int _memorizationTimes = 0;
    [NotMapped] private string _pronunciation;
    [NotMapped] private bool _hasMemorized;
    [NotMapped] private bool _isImportant;
    [NotMapped] private ObservableCollection<WordExplanation> _explanations;

    [Key]
    [Column("id")]
    public Guid Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }

    [Column("spell")]
    public string Spell
    {
        get => _spell;
        set => SetField(ref _spell, value);
    }

    [Column("language")]
    public WordLanguage Language
    {
        get => _language;
        set => SetField(ref _language, value);
    }

    [Column("added_at")]
    public DateTime AddedAt
    {
        get => _addedAt;
        set => SetField(ref _addedAt, value);
    }

    [Column("last_memorized_at")]
    public DateTime LastMemorizedAt
    {
        get => _lastMemorizedAt;
        set => SetField(ref _lastMemorizedAt, value);
    }

    [Column("memorization_times")]
    public int MemorizationTimes
    {
        get => _memorizationTimes;
        set => SetField(ref _memorizationTimes, value);
    }

    [Column("Pronunciation")]
    public string Pronunciation
    {
        get => _pronunciation;
        set => SetField(ref _pronunciation, value);
    }

    [Column("has_memorized")]
    public bool HasMemorized
    {
        get => _hasMemorized;
        set => SetField(ref _hasMemorized, value);
    }

    [Column("is_important")]
    public bool IsImportant
    {
        get => _isImportant;
        set => SetField(ref _isImportant, value);
    }

    public ObservableCollection<WordExplanation> Explanations
    {
        get => _explanations;
        set
        {
            value.CollectionChanged += (_, _) => OnPropertyChanged(nameof(Explanations));
            _explanations = value;
            OnPropertyChanged();
        }
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

    /// <inheritdoc />
    public override string ToString()
    {
        return Spell;
    }
}