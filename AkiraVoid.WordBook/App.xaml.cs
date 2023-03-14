// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System.Linq;
using AkiraVoid.WordBook.Pages;
using AkiraVoid.WordBook.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AkiraVoid.WordBook
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();

            // 初始化数据库。
            Global.WordBank.Database.Migrate();

            // 从数据库中获取单词和词性并存储在内存中。
            Global.WordList = new(
                Global.WordBank.Words.OrderBy(w => w.AddedAt)
                    .Include(w => w.Explanations)
                    .ThenInclude(e => e.PartOfSpeech)
                    .ToList());
            Global.PartsOfSpeech = Global.WordBank.PartsOfSpeech.OrderBy(p => p.DisplayName).ToList();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            // 创建主窗口。
            Global.UIHelper ??= new();
            Global.UIHelper.CreateWindow(
                "MainWindow",
                (window) =>
                {
                    var resources = Resources;

                    // 初始化窗口。
                    window.Title = resources["AppTitleText"] as string ?? "AkiraVoid WordBook";
                    window.Content = new RootPage();
                    window.ExtendsContentIntoTitleBar = true;
                });

            // 激活主窗口并设置云母主题。
            var window = Global.UIHelper.GetWindow("MainWindow");
            window.Activate();
            Global.UIHelper.TrySetMicaBackdrop(window);
        }
    }
}