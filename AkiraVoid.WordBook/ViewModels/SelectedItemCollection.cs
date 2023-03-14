using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AkiraVoid.WordBook.ViewModels;

public class SelectedItemCollection<T> : ObservableCollection<T>
{
    public SelectedItemCollection()
    {
        _previousCount = Count;
        CollectionChanged += OnCollectionChanged;
    }

    private int _previousCount;
    public event EventHandler<CountChangedEventArgs> CountChanged;

    public bool IsMoreThan(int count) => Count > count;
    public bool IsLessThan(int count) => Count < count;
    public bool HasSelectedItems() => Count > 0;
    public bool NoSelectedItems() => Count <= 0;
    public bool IsBetween(int atLeast, int atMost) => Count >= atLeast && Count <= atMost;

    private void OnCountChanged()
    {
        CountChanged?.Invoke(this, new() { Now = Count, Previous = _previousCount });
        _previousCount = Count;
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
    {
        if (Count != _previousCount)
        {
            OnCountChanged();
        }
    }

    /// <inheritdoc />
    public sealed override event NotifyCollectionChangedEventHandler CollectionChanged
    {
        add => base.CollectionChanged += value;
        remove => base.CollectionChanged -= value;
    }
}