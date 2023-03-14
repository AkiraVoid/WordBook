// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using AkiraVoid.WordBook.Enums;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using AkiraVoid.WordBook.ViewModels;
using AkiraVoid.WordBook.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Controls
{
    public sealed partial class SpellChecker : UserControl
    {
        public SpellChecker()
        {
            this.InitializeComponent();
            StateChanged += OnStateChanged;
            Loaded += (_, _) =>
            {
                SetDescription();
                VisualStateManager.GoToState(this, State.ToString() + "State", false);
            };
        }

        public InputValidationState State
        {
            get => (InputValidationState)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
            nameof(State),
            typeof(InputValidationState),
            typeof(SpellChecker),
            new(InputValidationState.Unvalidated, StateChangedCallback));

        public object ErrorMessage
        {
            get => GetValue(ErrorMessageProperty);
            set => SetValue(ErrorMessageProperty, value);
        }

        public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register(
            nameof(ErrorMessage),
            typeof(object),
            typeof(SpellChecker),
            new(null));

        public object PassedMessage
        {
            get => GetValue(PassedMessageProperty);
            set => SetValue(PassedMessageProperty, value);
        }

        public static readonly DependencyProperty PassedMessageProperty = DependencyProperty.Register(
            nameof(PassedMessage),
            typeof(object),
            typeof(SpellChecker),
            new(null));

        public object UnvalidatedMessage
        {
            get => GetValue(UnvalidatedMessageProperty);
            set => SetValue(UnvalidatedMessageProperty, value);
        }

        public static readonly DependencyProperty UnvalidatedMessageProperty = DependencyProperty.Register(
            nameof(UnvalidatedMessage),
            typeof(object),
            typeof(SpellChecker),
            new(null));

        public Word Word
        {
            get => (Word)GetValue(WordProperty);
            set => SetValue(WordProperty, value);
        }

        public static readonly DependencyProperty WordProperty = DependencyProperty.Register(
            nameof(Word),
            typeof(Word),
            typeof(SpellChecker),
            new(null));

        public event EventHandler<ValidationEventArgs> Validate;
        public event EventHandler<ValidationEventArgs> Validated;
        public event EventHandler<ValidationEventArgs> Passed;
        public event EventHandler<ValidationEventArgs> Error;
        public event EventHandler<ValidationEventArgs> StateChanged;

        private object _description;

        private object Description
        {
            get => _description;
            set
            {
                _description = value;
                DescriptionPresenter.Content = Description;
            }
        }

        public Func<Control, object> GetContent { get; set; }

        private void OnValidate()
        {
            Validate?.Invoke(this, new() { State = State });
        }

        private void OnValidated()
        {
            Validated?.Invoke(this, new() { State = State });
        }

        private void OnPassed()
        {
            Passed?.Invoke(this, new() { State = State });
        }

        private void OnError()
        {
            Error?.Invoke(this, new() { State = State });
        }

        private void OnStateChanged(object sender, ValidationEventArgs args)
        {
            SetDescription();
            VisualStateManager.GoToState(this, State.ToString() + "State", false);
        }

        private void SetDescription()
        {
            Description = State switch
            {
                InputValidationState.Error => ErrorMessage,
                InputValidationState.Passed => PassedMessage,
                InputValidationState.Unvalidated => UnvalidatedMessage,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static void StateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var control = (SpellChecker)d;
            control.StateChanged?.Invoke(control, new() { State = control.State });
        }

        public void TriggerValidation()
        {
            OnValidate();
            var isPassed = Spell.Text.Equals(Word?.Spell, StringComparison.InvariantCultureIgnoreCase);
            if (isPassed)
            {
                State = InputValidationState.Passed;
                OnPassed();
            }
            else
            {
                State = InputValidationState.Error;
                OnError();
            }

            OnValidated();
        }

        public void ClearInput()
        {
            Spell.Text = null;
        }
    }
}