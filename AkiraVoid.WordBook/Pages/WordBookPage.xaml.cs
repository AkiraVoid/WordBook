// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Controls;
using System.Linq;
using AkiraVoid.WordBook.Models;
using AkiraVoid.WordBook.Utilities;
using AkiraVoid.WordBook.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WordBookPage : Page
    {
        public SelectedItemCollection<Word> SelectedWords { get; set; } = new();

        public WordBookPage()
        {
            this.InitializeComponent();
            SelectedWords.CountChanged += OnSelectionChanged;
            EditWordButton.IsEnabled = SelectedWords.IsBetween(1, 1);
            DeleteWordButton.IsEnabled = SelectedWords.HasSelectedItems();
            ReadMoreButton.IsEnabled = SelectedWords.IsBetween(1, 1);
        }

        private void OnSelectionChanged(object sender, CountChangedEventArgs e)
        {
            EditWordButton.IsEnabled = SelectedWords.IsBetween(1, 1);
            DeleteWordButton.IsEnabled = SelectedWords.HasSelectedItems();
            ReadMoreButton.IsEnabled = SelectedWords.IsBetween(1, 1);
        }

        private void OnPaneClose(object sender, RoutedEventArgs args)
        {
            PageRoot.IsPaneOpen = false;
        }

        private void OnAdditionPageOpen(object sender, RoutedEventArgs args)
        {
            Actions.Navigate(typeof(WordEditionPage));
            PageRoot.IsPaneOpen = true;
        }

        private void OnEditionPageOpen(object sender, RoutedEventArgs args)
        {
            Actions.Navigate(typeof(WordEditionPage), SelectedWords[0]);
            PageRoot.IsPaneOpen = true;
        }

        private void OnWordListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (object removedItem in e.RemovedItems)
            {
                SelectedWords.Remove((Word)removedItem);
            }

            foreach (object addedItem in e.AddedItems)
            {
                SelectedWords.Add((Word)addedItem);
            }
        }

        private void OnImportantToggled(object sender, WordListRoutedEventArgs e)
        {
            var word = Global.WordList.FirstOrDefault(w => w.Id == e.WordId);
            if (word != null)
            {
                word.IsImportant = !word.IsImportant;
                Global.WordBank.Entry(word).State = EntityState.Modified;
                Global.WordBank.SaveChanges();
            }
        }

        private void OnDeleted(object sender, RoutedEventArgs e) => DeleteSelection();

        private void DeleteSelection()
        {
            Global.WordBank.Words.RemoveRange(SelectedWords);
            foreach (var selectedWord in SelectedWords.ToList())
            {
                Global.WordList.Remove(selectedWord);
            }

            SelectedWords.Clear();
            Global.WordBank.SaveChanges();
        }

        private void OnMemorizedToggle(object sender, WordListRoutedEventArgs e)
        {
            var word = Global.WordList.FirstOrDefault(w => w.Id == e.WordId);
            if (word != null)
            {
                word.HasMemorized = !word.HasMemorized;
                Global.WordBank.Entry(word).State = EntityState.Modified;
                Global.WordBank.SaveChanges();
            }
        }

        private void OnReadMoreRequested(object sender, RoutedEventArgs e)
        {
            Global.Navigator.Navigate(
                Global.Navigator.Navigations.Find(nav => nav.PageTag == "WordDetail"),
                SelectedWords[0]);
        }
    }
}