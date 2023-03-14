// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Linq;
using AkiraVoid.WordBook.Enums;
using AkiraVoid.WordBook.Utilities;
using AkiraVoid.WordBook.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook.Controls
{
    public sealed partial class TeacherPicker : UserControl
    {
        public TeacherPicker()
        {
            this.InitializeComponent();
        }

        private WordLanguage _teacherLanguage;

        public WordLanguage TeacherLanguage
        {
            get => _teacherLanguage;
            set
            {
                _teacherLanguage = value;
                var teachers = Teachers.GetTeachers(value);
                Picker.ItemsSource = teachers;
                SelectedTeacher = teachers.FirstOrDefault(t => t.Equals(Teachers.GetConfiguredTeacher(value)));
            }
        }

        private Teacher _selectedTeacher;

        public Teacher SelectedTeacher
        {
            get => _selectedTeacher;
            private set
            {
                _selectedTeacher = value;
                Picker.SelectedItem = value;
            }
        }

        public event SelectionChangedEventHandler SelectionChanged;

        private void OnPickerSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            SelectedTeacher = (sender as ComboBox)?.SelectedItem as Teacher;
            Teachers.SetTeacher(TeacherLanguage, SelectedTeacher);
            OnSelectionChanged(args);
        }

        private void OnSelectionChanged(SelectionChangedEventArgs args)
        {
            SelectionChanged?.Invoke(this, args);
        }

        private void OnTestPlay(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            Teachers.SpeakAsync(
                TeacherLanguage == WordLanguage.English ? "Test, test, can you hear me?" : "テスト、テスト。聞こえますか。",
                TeacherLanguage);
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
        }
    }
}