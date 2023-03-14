using System.Collections.Generic;
using System.Linq;
using AkiraVoid.WordBook.Pages;
using AkiraVoid.WordBook.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;

namespace AkiraVoid.WordBook.Helpers;

/// <summary>
/// 用于简化导航操作的类型。
/// </summary>
public class Navigator
{
    private readonly Frame _frame;
    private readonly NavigationView _navigationView;
    private object _selectedItem;

    /// <summary>
    /// 获取或设置 <see cref="NavigationView"/> 中选中的选项。
    /// </summary>
    private object SelectedItem
    {
        get => _selectedItem;
        set
        {
            _navigationView.SelectedItem = value;
            _selectedItem = value;
        }
    }

    /// <summary>
    /// 获取已定义的导航。
    /// </summary>
    public readonly List<Navigation> Navigations = new()
    {
        new() { PageTag = "WordBook", PageTitle = "生词本", PageType = typeof(WordBookPage) },
        new() { PageTag = "Dictation", PageTitle = "听写", PageType = typeof(DictationPage) },
        new() { PageTag = "WordDetail", PageTitle = "单词详情", PageType = typeof(WordDetailPage) }
    };

    /// <summary>
    /// 初始化一个 <see cref="Navigator"/> 实例。
    /// </summary>
    /// <param name="frame">定义该实例操作的 <see cref="Frame"/>。</param>
    /// <param name="navigationView">定义用于导航的 <see cref="NavigationView"/>。</param>
    public Navigator(Frame frame, NavigationView navigationView)
    {
        _frame = frame;
        _navigationView = navigationView;
        _navigationView.ItemInvoked += OnNavigationRequested;
        SelectedItem = _navigationView.MenuItems[0];
        Navigate(Navigations.Find(nav => nav.PageTag == "WordBook"));
    }

    /// <summary>
    /// 手动导航至指定位置。
    /// </summary>
    /// <param name="navigation">要导航的位置。</param>
    /// <param name="parameter">要向目标传递的参数。</param>
    /// <param name="transition">要使用的过渡动画。</param>
    /// <returns></returns>
    public bool Navigate(Navigation navigation, object parameter = null, NavigationTransitionInfo transition = null)
    {
        var isNavigated = _frame.Navigate(navigation.PageType, parameter, transition);
        if (!isNavigated)
            return false;
        _navigationView.Header = navigation.PageTitle;
        navigation.CallBack?.Invoke();

        return true;
    }

    /// <summary>
    /// 当 <see cref="NavigationView"/> 发起导航时自动触发。
    /// </summary>
    /// <param name="sender">事件发起者。</param>
    /// <param name="args">收到的参数。</param>
    private void OnNavigationRequested(NavigationView sender, NavigationViewItemInvokedEventArgs args)
    {
        var pageTag = args.IsSettingsInvoked ? null : args.InvokedItemContainer.Tag as string;
        if (pageTag != null)
        {
            var navigation = Navigations.Find(nav => nav.PageTag == pageTag);
            Navigate(navigation, null, args.RecommendedNavigationTransitionInfo);
            SelectedItem =
                _navigationView.MenuItems.FirstOrDefault(i => (i as NavigationViewItem)?.Tag.ToString() == pageTag);
        }
        else
        {
            SelectedItem = _selectedItem;
        }
    }
}