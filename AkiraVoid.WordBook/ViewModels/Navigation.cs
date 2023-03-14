using System;

namespace AkiraVoid.WordBook.ViewModels;

public class Navigation
{
    public Type PageType { get; set; }
    public Action CallBack { get; set; }
    public string PageTitle { get; set; }
    public string PageTag { get; set; }
}