using System;

namespace AkiraVoid.WordBook.ViewModels;

public class ValueChangedEventArgs<T> : EventArgs
{
    public readonly T Value;

    public ValueChangedEventArgs(T value)
    {
        Value = value;
    }
}