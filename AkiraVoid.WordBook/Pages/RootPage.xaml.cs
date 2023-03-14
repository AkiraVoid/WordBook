// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using AkiraVoid.WordBook.Helpers;
using AkiraVoid.WordBook.Models;
using AkiraVoid.WordBook.Utilities;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootPage : Page
    {
        private readonly Navigator _navigator;
        private bool _isContinuousSettingsInvoking = false;

        public RootPage()
        {
            this.InitializeComponent();
            if (!Global.UIHelper.TryGetWindow("MainWindow", out var mainWindow))
            {
                throw new("No window was rendered.");
            }

            mainWindow.SetTitleBar(AppTitleBar);

            _navigator = new(MainFrame, Navigation);
            Global.Navigator = _navigator;
            Navigation.ItemInvoked += OnSettingsClick;

            int selectedTheme = ThemeSwitcher.GetTheme() switch
            {
                ElementTheme.Dark => 2,
                ElementTheme.Light => 1,
                ElementTheme.Default => 0,
                _ => throw new ArgumentException("theme")
            };
            ThemeSelector.SelectedIndex = selectedTheme;
            ThemeSwitcher.SwitchTheme(this);
        }

        public void OnSettingsClick(object sender, NavigationViewItemInvokedEventArgs args)
        {
            if (!args.IsSettingsInvoked) return;
            if (!_isContinuousSettingsInvoking)
            {
                DrawerContainer.IsPaneOpen = !DrawerContainer.IsPaneOpen;
                _isContinuousSettingsInvoking = true;
            }
            else
            {
                _isContinuousSettingsInvoking = false;
            }
        }

        public void OnThemeChange(object sender, SelectionChangedEventArgs args)
        {
            var selectedIndex = (sender as RadioButtons)!.SelectedIndex;
            var theme = selectedIndex == 2 ? ElementTheme.Dark :
                selectedIndex == 1 ? ElementTheme.Light : ElementTheme.Default;
            ThemeSwitcher.SwitchTheme(theme, this);
        }

        private void OnSearching(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                var suggestion = Global.WordList
                    .Where(w => w.Spell.Contains(sender.Text, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
                sender.ItemsSource = suggestion;
            }
        }

        private void OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem is Word word)
            {
                _navigator.Navigate(_navigator.Navigations.FirstOrDefault(nav => nav.PageTag == "WordDetail"), word);
            }
        }
    }
}