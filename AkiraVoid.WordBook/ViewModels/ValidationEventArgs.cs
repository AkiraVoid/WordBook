using System;
using AkiraVoid.WordBook.Enums;

namespace AkiraVoid.WordBook.ViewModels;

public class ValidationEventArgs : EventArgs
{
    public InputValidationState State { get; set; }
}