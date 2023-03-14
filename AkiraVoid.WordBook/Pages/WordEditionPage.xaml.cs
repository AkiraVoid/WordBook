// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System.Collections.ObjectModel;
using System.ComponentModel;
using AkiraVoid.WordBook.Enums;
using AkiraVoid.WordBook.Models;
using AkiraVoid.WordBook.Utilities;
using AkiraVoid.WordBook.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WordEditionPage : Page
    {
        private bool _isEditing;

        public Word Word
        {
            get => (Word)GetValue(WordProperty);
            set
            {
                NewExplanations.Clear();

                if (NewExplanations.Count <= 0 && value.Explanations is not { Count: > 0 })
                {
                    NewExplanations.Add(new(value.Id));
                }

                SetValue(WordProperty, value);
            }
        }

        public static readonly DependencyProperty WordProperty = DependencyProperty.Register(
            nameof(Word),
            typeof(Word),
            typeof(WordEditionPage),
            new(new()));

        public ObservableCollection<WordExplanation> NewExplanations { get; set; } = new();

        public WordEditionPage()
        {
            this.InitializeComponent();

            LanguagePicker.SelectionChanged += (sender, args) =>
            {
                Word.Language = LanguagePicker.SelectedIndex == 0 ? WordLanguage.English : WordLanguage.Japanese;
            };
        }

        /// <inheritdoc />
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Word = e.Parameter is not null ? e.Parameter as Word : new() { Explanations = new() };
            _isEditing = e.Parameter is not null;
            Word!.PropertyChanged += OnWordLanguageChange;
            LanguagePicker.SelectedIndex = Word.Language == WordLanguage.English ? 0 : 1;
        }

        private void OnExplanationAdded(object sender, ExplanationEditorRoutedEventArgs args)
        {
            NewExplanations.Add(new(Word.Id));
        }

        private void OnNewExplanationRemoved(object sender, ExplanationEditorRoutedEventArgs args)
        {
            NewExplanations.Remove(args.Explanation);
            if (NewExplanations.Count <= 0 && Word.Explanations.Count <= 0)
            {
                NewExplanations.Add(new(Word.Id));
            }
        }

        private void OnExplanationRemoved(object sender, ExplanationEditorRoutedEventArgs args)
        {
            Word.Explanations.Remove(args.Explanation);
            if (NewExplanations.Count <= 0 && Word.Explanations.Count <= 0)
            {
                NewExplanations.Add(new(Word.Id));
            }
        }

        private void OnWordLanguageChange(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Word.Language))
            {
                LanguagePicker.SelectedIndex = Word.Language == WordLanguage.English ? 0 : 1;
            }
        }

        private void OnConfirm(object sender, RoutedEventArgs e)
        {
            ConfirmButton.IsEnabled = false;
            foreach (var explanation in NewExplanations)
            {
                if (!explanation.IsEmpty())
                {
                    Word.Explanations.Add(explanation);
                }
            }

            if (_isEditing)
            {
                Global.WordBank.SaveChanges();
                NewExplanations.Clear();
                if (Word.Explanations.Count <= 0 && NewExplanations.Count <= 0)
                {
                    NewExplanations.Add(new(Word.Id));
                }
            }
            else
            {
                Global.WordBank.Add(Word);
                Global.WordBank.SaveChanges();


                Global.WordList.Add(Word);

                Word = new() { Explanations = new() };
                var binding = new Binding { Source = Word.Explanations };
                OriginalExplanationEditors.SetBinding(ItemsRepeater.ItemsSourceProperty, binding);
            }

            ConfirmButton.IsEnabled = true;
        }
    }
}