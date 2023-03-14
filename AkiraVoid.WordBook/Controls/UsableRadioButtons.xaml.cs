// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AkiraVoid.WordBook.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Controls
{
    public sealed partial class UsableRadioButtons : UserControl
    {
        public UsableRadioButtons()
        {
            this.InitializeComponent();
            Loaded += (_, _) =>
            {
                if (ItemTemplate == null)
                {
                    ItemTemplate = DefaultTemplate;
                }
            };
        }

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            nameof(Orientation),
            typeof(Orientation),
            typeof(UsableRadioButtons),
            new(Orientation.Vertical));

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
                _radioButtons.Clear();
                if (value is IEnumerable values)
                {
                    foreach (var o in values)
                    {
                        _radioButtons.Add(
                            new(o)
                            {
                                DisplayContent = ItemConverter is null
                                    ? o.ToString()
                                    : ItemConverter.Convert(
                                            o,
                                            o.GetType(),
                                            null,
                                            null)
                                        .ToString()
                            });
                    }
                }
                else
                {
                    _radioButtons.Add(
                        new(value)
                        {
                            DisplayContent = ItemConverter is null
                                ? value.ToString()
                                : ItemConverter.Convert(
                                        value,
                                        value.GetType(),
                                        null,
                                        null)
                                    .ToString()
                        });
                }
            }
        }

        public IValueConverter ItemConverter
        {
            get => (IValueConverter)GetValue(ItemConverterProperty);
            set => SetValue(ItemConverterProperty, value);
        }

        public static readonly DependencyProperty ItemConverterProperty = DependencyProperty.Register(
            nameof(ItemConverter),
            typeof(IValueConverter),
            typeof(UsableRadioButtons),
            new(null));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(object),
            typeof(UsableRadioButtons),
            new(null));

        public object ItemTemplate
        {
            get => GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value ?? DefaultTemplate);
        }

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(object),
            typeof(UsableRadioButtons),
            new(null));

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SelectItemBySourceItem(value, true);
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(UsableRadioButtons),
            new(null));

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(object),
            typeof(UsableRadioButtons),
            new(null));

        public event SelectionChangedEventHandler SelectionChanged;

        private readonly ObservableCollection<RadioButtonItem> _radioButtons = new();

        private void OnSelect(object sender, RoutedEventArgs e)
        {
            var radioButton = (RadioButton)sender;
            if (radioButton != null)
            {
                SelectItemBySourceItem(radioButton.Tag);
            }
        }

        private void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            SelectionChanged?.Invoke(this, e);
        }

        private void SelectItemBySourceItem(object sourceItem, bool checkItem = false)
        {
            var previousSelected = _radioButtons.FirstOrDefault(rb => rb.IsChecked && rb.OriginalItem != sourceItem);
            if (previousSelected != null)
            {
                previousSelected.IsChecked = false;
            }

            if (checkItem)
            {
                var nextSelection = _radioButtons.FirstOrDefault(rb => rb.OriginalItem.Equals(sourceItem));
                if (nextSelection != null)
                {
                    nextSelection.IsChecked = true;
                }
            }

            var previousItem = SelectedItem;
            SetValue(SelectedItemProperty, sourceItem);
            OnSelectionChanged(new(new List<object> { previousItem }, new List<object> { SelectedItem }));
        }
    }
}