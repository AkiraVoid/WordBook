// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using AkiraVoid.WordBook.Models;
using AkiraVoid.WordBook.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Controls
{
    public sealed partial class ExplanationEditor : UserControl
    {
        public event EventHandler<ExplanationEditorRoutedEventArgs> ActionButtonClick;
        public event EventHandler<ExplanationEditorRoutedEventArgs> AddButtonClick;
        public event EventHandler<ExplanationEditorRoutedEventArgs> RemoveButtonClick;
        public event EventHandler<ExplanationEditorEditedEventArgs> Edit;

        public ExplanationEditor()
        {
            this.InitializeComponent();
        }

        public WordExplanation Explanation
        {
            get => (WordExplanation)GetValue(ExplanationProperty);
            set
            {
                value.PropertyChanged += OnPropertyChanged;
                OnEdit(new(Explanation) { EditedProperty = "this" });
                SetValue(ExplanationProperty, value);
            }
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) =>
            OnEdit(new(Explanation) { EditedProperty = e.PropertyName });


        public static readonly DependencyProperty ExplanationProperty = DependencyProperty.Register(
            nameof(Explanation),
            typeof(WordExplanation),
            typeof(ExplanationEditor),
            new(null));


        private void OnAddButtonClicked(object sender, RoutedEventArgs args)
        {
            ActionButtonClick?.Invoke(this, new(args) { ActionButton = AddButton, Explanation = Explanation });
            AddButtonClick?.Invoke(this, new(args) { ActionButton = AddButton, Explanation = Explanation });
        }

        private void OnRemoveButtonClicked(object sender, RoutedEventArgs args)
        {
            ActionButtonClick?.Invoke(this, new(args) { ActionButton = RemoveButton, Explanation = Explanation });
            RemoveButtonClick?.Invoke(this, new(args) { ActionButton = AddButton, Explanation = Explanation });
        }

        private void OnEdit(ExplanationEditorEditedEventArgs args)
        {
            Edit?.Invoke(this, args);
        }
    }
}