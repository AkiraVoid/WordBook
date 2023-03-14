// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.System;
using AkiraVoid.WordBook.Controls;
using AkiraVoid.WordBook.Enums;
using AkiraVoid.WordBook.Models;
using AkiraVoid.WordBook.Utilities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DictationPage : Page
    {
        public DictationPage()
        {
            this.InitializeComponent();
        }

        private int WordCount
        {
            get => (int)GetValue(_wordCountProperty);
            set => SetValue(_wordCountProperty, value);
        }

        private static readonly DependencyProperty _wordCountProperty = DependencyProperty.Register(
            nameof(WordCount),
            typeof(int),
            typeof(DictationPage),
            new(2));

        private readonly ObservableCollection<Word> _words = new();
        private readonly ObservableCollection<DictationWordRange> _ranges = new(Enum.GetValues<DictationWordRange>());

        private int _toPlayIndex;

        private int ToPlayIndex
        {
            get => _toPlayIndex;
            set
            {
                _toPlayIndex = value;
                ReplayButton.IsEnabled = value > 0;
                RefreshButton.IsEnabled = value > 0;
                NextButton.IsEnabled = value < _words.Count;
                Checker.IsEnabled = value > 0;
                Checker.State = InputValidationState.Unvalidated;
                Checker.ClearInput();
            }
        }

        private void WordRangeChanged(object sender, SelectionChangedEventArgs args) => SelectWords();

        private void SelectWords()
        {
            var selectedWordRange = (DictationWordRange)WordRangePicker.SelectedItem;
            _words.Clear();
            switch (selectedWordRange)
            {
                case DictationWordRange.Random:
                    SelectRandomWords();
                    break;
                case DictationWordRange.All:
                    SelectAllWords();
                    break;
                case DictationWordRange.Today:
                    SelectWordsOfToday();
                    break;
                case DictationWordRange.Yesterday:
                    SelectWordsOfYesterday();
                    break;
                case DictationWordRange.Important:
                    SelectImportantWords();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ToPlayIndex = 0;
        }

        private void SelectRandomWords() =>
            _words.AddRange(Global.WordList.Where(w => !w.HasMemorized).ToList().Shuffle().Take(WordCount));

        private void SelectAllWords() =>
            _words.AddRange(Global.WordList.Where(w => !w.HasMemorized).ToList().Shuffle());

        private void SelectWordsOfToday() =>
            _words.AddRange(
                Global.WordList.Where(w => DateTime.Now - w.AddedAt <= TimeSpan.FromDays(1))
                    .Where(w => !w.HasMemorized)
                    .Take(WordCount)
                    .ToList()
                    .Shuffle());

        private void SelectWordsOfYesterday() =>
            _words.AddRange(
                Global.WordList
                    .Where(
                        w => DateTime.Now - w.AddedAt <= TimeSpan.FromDays(2) &&
                            DateTime.Now - w.AddedAt > TimeSpan.FromDays(1))
                    .Where(w => !w.HasMemorized)
                    .Take(WordCount)
                    .ToList()
                    .Shuffle());

        private void SelectImportantWords() =>
            _words.AddRange(Global.WordList.Where(w => w.IsImportant).Take(WordCount).ToList().Shuffle());

        private async void OnPlayNextAsync(object sender, RoutedEventArgs args) => await PlayNextAsync();

        private async Task PlayNextAsync()
        {
            await Teachers.SpeakAsync(_words[ToPlayIndex]);
            Checker.Word = _words[ToPlayIndex];
            if (ToPlayIndex < _words.Count)
            {
                ToPlayIndex++;
            }
        }

        private async void OnReplayAsync(object sender, RoutedEventArgs args) =>
            await Teachers.SpeakAsync(_words[ToPlayIndex - 1]);

        private async void OnEnterPressedAsync(object sender, KeyRoutedEventArgs args)
        {
            if (args.Key == VirtualKey.Enter)
            {
                if (Checker.State != InputValidationState.Unvalidated)
                {
                    await PlayNextAsync();
                }
                else
                {
                    ((SpellChecker)sender).TriggerValidation();
                }
            }
        }

        private void OnReset(object sender, RoutedEventArgs e) => SelectWords();

        private void OnSettingClick(object sender, RoutedEventArgs e)
        {
            Root.IsPaneOpen = !Root.IsPaneOpen;
        }

        private void OnPaneClose(object sender, RoutedEventArgs e)
        {
            Root.IsPaneOpen = false;
        }
    }
}