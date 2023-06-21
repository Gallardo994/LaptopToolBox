using System;
using System.Collections.ObjectModel;
using System.Linq;
using GHelper.Pages;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;

namespace GHelper;

public partial class PageProvider : ObservableObject, IPageProvider
{
    [ObservableProperty] private ObservableCollection<FlyoutPageItem> _pages;
    [ObservableProperty] private ObservableCollection<FlyoutPageItem> _normalPages;
    [ObservableProperty] private ObservableCollection<FlyoutPageItem> _footerPages;

    public PageProvider()
    {
        Pages = new ObservableCollection<FlyoutPageItem>
        {
            new FlyoutPageItem
            {
                Title = "Home",
                TargetType = typeof(HomePage),
                IsHomePage = true,
                Icon = new FontIcon
                {
                    Glyph = "\uE80F",
                },
                Tag = "Home",
            },
            new FlyoutPageItem
            {
                Title = "Performance",
                TargetType = typeof(PerformancePage),
                Icon = new FontIcon
                {
                    Glyph = "\uE7B8",
                },
                Tag = "Performance",
            },
            new FlyoutPageItem
            {
                Title = "Peripherals",
                TargetType = typeof(PeripheralsPage),
                Icon = new FontIcon
                {
                    Glyph = "\uF73D",
                },
                Tag = "Peripherals",
            },
            new FlyoutPageItem
            {
                Title = "Updates",
                TargetType = typeof(UpdatesPage),
                Icon = new FontIcon
                {
                    Glyph = "\uE72C",
                },
                Tag = "Updates"
            },
            new FlyoutPageItem
            {
                Title = "About",
                TargetType = typeof(AboutPage),
                Icon = new FontIcon
                {
                    Glyph = "\uE946",
                },
                Tag = "About",
                IsFooter = true,
            },
            new FlyoutPageItem
            {
                Title = "Settings",
                TargetType = typeof(SettingsPage),
                Icon = new FontIcon
                {
                    Glyph = "\uE713",
                },
                Tag = "Settings",
                IsFooter = true,
            },
        };
        
        NormalPages = new ObservableCollection<FlyoutPageItem>(Pages.Where(page => !page!.IsFooter));
        FooterPages = new ObservableCollection<FlyoutPageItem>(Pages.Where(page => page!.IsFooter));
    }
    
    public FlyoutPageItem GetPageItem<T>() where T : Page
    {
        return Pages.FirstOrDefault(page => page?.TargetType == typeof(T));
    }
    
    public FlyoutPageItem GetPageItem(Type type)
    {
        return Pages.FirstOrDefault(page => page?.TargetType == type);
    }
    
    public FlyoutPageItem GetPageItem(int index)
    {
        return Pages.ElementAtOrDefault(index);
    }
    
    public FlyoutPageItem GetHomePageItem()
    {
        return Pages.First(page => page?.IsHomePage == true)!;
    }
}