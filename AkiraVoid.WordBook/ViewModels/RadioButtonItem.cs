using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AkiraVoid.WordBook.ViewModels;

public class RadioButtonItem : INotifyPropertyChanged
{
    private object _originalItem;
    private bool _isChecked;
    private string _displayContent;

    public object OriginalItem
    {
        get => _originalItem;
        set => SetField(ref _originalItem, value);
    }

    public bool IsChecked
    {
        get => _isChecked;
        set => SetField(ref _isChecked, value);
    }

    public string DisplayContent
    {
        get => _displayContent;
        set => SetField(ref _displayContent, value);
    }

    public RadioButtonItem()
    {
    }

    public RadioButtonItem(object originalItem)
    {
        OriginalItem = originalItem;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}