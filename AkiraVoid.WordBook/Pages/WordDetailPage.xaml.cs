// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using AkiraVoid.WordBook.Models;
using AkiraVoid.WordBook.Utilities;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WordDetailPage : Page
    {
        public WordDetailPage()
        {
            this.InitializeComponent();
        }

        private Word Word { get; set; }

        private readonly ObservableCollection<IGrouping<PartOfSpeech, WordExplanation>> _groupedExplanations = new();

        public object MemorizeButtonContent
        {
            get => GetValue(MemorizeButtonContentProperty);
            set => SetValue(MemorizeButtonContentProperty, value);
        }

        public static readonly DependencyProperty MemorizeButtonContentProperty = DependencyProperty.Register(
            nameof(MemorizeButtonContent),
            typeof(object),
            typeof(WordDetailPage),
            new("标记为已记住"));

        public FontIcon ImportantButtonIcon
        {
            get => (FontIcon)GetValue(ImportantButtonIconProperty);
            set => SetValue(ImportantButtonIconProperty, value);
        }

        public static readonly DependencyProperty ImportantButtonIconProperty = DependencyProperty.Register(
            nameof(ImportantButtonIcon),
            typeof(FontIcon),
            typeof(WordDetailPage),
            new(null));

        public string ImportantButtonToolTip
        {
            get => (string)GetValue(ImportantButtonToolTipProperty);
            set => SetValue(ImportantButtonToolTipProperty, value);
        }

        public static readonly DependencyProperty ImportantButtonToolTipProperty = DependencyProperty.Register(
            nameof(ImportantButtonToolTip),
            typeof(string),
            typeof(WordDetailPage),
            new("标记为重要"));

        /// <inheritdoc />
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            base.OnNavigatedTo(args);
            Word = args.Parameter is not null ? args.Parameter as Word : new() { Explanations = new() };
            _groupedExplanations.Clear();
            _groupedExplanations.AddRange(Word?.Explanations.GroupBy(e => e.PartOfSpeech));
            Word!.PropertyChanged += OnWordPropertyChange;
            var memorizedContent = new StackPanel { Orientation = Orientation.Horizontal, };
            memorizedContent.Children.Add(
                new FontIcon
                {
                    FontFamily = (FontFamily)Resources["SymbolThemeFontFamily"],
                    Glyph = "\xe73e",
                    Margin = new(
                        0,
                        0,
                        4,
                        0)
                });
            memorizedContent.Children.Add(new TextBlock { Text = "已记住" });

            MemorizeButtonContent = Word.HasMemorized ? memorizedContent : "标记为已记住";
            ImportantButtonIcon = Word.IsImportant
                ? new() { FontFamily = (FontFamily)Resources["SymbolThemeFontFamily"], Glyph = "\xe735" }
                : new() { FontFamily = (FontFamily)Resources["SymbolThemeFontFamily"], Glyph = "\xe734" };
            ImportantButtonToolTip = Word.IsImportant ? "重要" : "标记为重要";
        }

        private void OnWordPropertyChange(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Word.HasMemorized))
            {
                var memorizedContent = new StackPanel { Orientation = Orientation.Horizontal, };
                memorizedContent.Children.Add(
                    new FontIcon
                    {
                        FontFamily = (FontFamily)Resources["SymbolThemeFontFamily"],
                        Glyph = "\xe73e",
                        Margin = new(
                            0,
                            0,
                            4,
                            0)
                    });
                memorizedContent.Children.Add(new TextBlock { Text = "已记住" });

                MemorizeButtonContent = Word.HasMemorized ? memorizedContent : "标记为已记住";
            }
            else if (args.PropertyName == nameof(Word.IsImportant))
            {
                ImportantButtonIcon = Word.IsImportant
                    ? new() { FontFamily = (FontFamily)Resources["SymbolThemeFontFamily"], Glyph = "\xe735" }
                    : new() { FontFamily = (FontFamily)Resources["SymbolThemeFontFamily"], Glyph = "\xe734" };
                ImportantButtonToolTip = Word.IsImportant ? "重要" : "标记为重要";
            }
        }

        private void OnMemorize(object sender, RoutedEventArgs e)
        {
            Word.HasMemorized = !Word.HasMemorized;
            Global.WordBank.SaveChanges();
        }

        private void OnSetImportant(object sender, RoutedEventArgs e)
        {
            Word.IsImportant = !Word.IsImportant;
            Global.WordBank.SaveChanges();
        }
    }

    public class ExplanationGroup : IGrouping<PartOfSpeech, WordExplanation>
    {
        /// <inheritdoc />
        public IEnumerator<WordExplanation> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc />
        public PartOfSpeech Key { get; }
    }
}