// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using AkiraVoid.WordBook.Models;
using AkiraVoid.WordBook.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Controls
{
    public sealed partial class WordListItem : UserControl
    {
        public WordListItem()
        {
            this.InitializeComponent();
        }

        public event EventHandler<WordListRoutedEventArgs> MemorizedToggle;
        public event EventHandler<WordListRoutedEventArgs> ImportantToggle;

        public Word Word
        {
            get => (Word)GetValue(WordProperty);
            set
            {
                ChangeButtonIconAndToolTip("IsImportant");
                ChangeButtonIconAndToolTip("HasMemorized");
                value.PropertyChanged += OnWordChange;
                SetValue(WordProperty, value);
            }
        }

        public static readonly DependencyProperty WordProperty = DependencyProperty.Register(
            nameof(Word),
            typeof(Word),
            typeof(WordListItem),
            new(
                null,
                (property, args) =>
                {
                    var a = property.GetValue(WordProperty);
                }));

        private void OnMemorizedToggled(object sender, RoutedEventArgs args)
        {
            var e = new WordListRoutedEventArgs(args) { WordId = Word.Id };
            MemorizedToggle?.Invoke(this, e);
        }

        private void OnImportantToggled(object sender, RoutedEventArgs args)
        {
            var e = new WordListRoutedEventArgs(args) { WordId = Word.Id };
            ImportantToggle?.Invoke(this, e);
        }

        private void OnWordChange(object sender, PropertyChangedEventArgs args) =>
            ChangeButtonIconAndToolTip(args.PropertyName);

        private void ChangeButtonIconAndToolTip(string changedProperty)
        {
            if (Word == null) return;
            if (changedProperty != "IsImportant" && changedProperty != "HasMemorized") return;
            var glyph = "";
            var toolTipContent = "";
            var button = ImportantToggleButton;
            switch (changedProperty)
            {
                case "IsImportant":
                    glyph = Word.IsImportant ? "\xE842" : "\xE840";
                    toolTipContent = Word.IsImportant ? "标记为一般词汇" : "标记为重要词汇";
                    break;
                case "HasMemorized":
                    glyph = Word.HasMemorized ? "\xE735" : "\xE734";
                    toolTipContent = Word.HasMemorized ? "标记为未记住的单词" : "标记为已记住的单词";
                    button = MemorizedToggleButton;
                    break;
            }

            button.IsChecked = changedProperty == "IsImportant" ? Word.IsImportant : Word.HasMemorized;

            var fontIcon = new FontIcon
            {
                FontFamily = (Microsoft.UI.Xaml.Media.FontFamily)Resources["SymbolThemeFontFamily"], Glyph = glyph
            };
            button.Content = fontIcon;
            ToolTip toolTip = new() { Content = toolTipContent };
            ToolTipService.SetToolTip(button, toolTip);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ChangeButtonIconAndToolTip("IsImportant");
            ChangeButtonIconAndToolTip("HasMemorized");
        }
    }
}