using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GHelper.Pages;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace GHelper;

public sealed class PageProvider : IPageProvider, INotifyPropertyChanged
{
    private ObservableCollection<FlyoutPageItem?> _pages = null!;
    public ObservableCollection<FlyoutPageItem?> Pages
    {
        get => _pages;
        set
        {
            _pages = value;
            OnPropertyChanged();
        }
    }
    
    private ObservableCollection<FlyoutPageItem?> _normalPages = null!;
    public ObservableCollection<FlyoutPageItem?> NormalPages
    {
        get => _normalPages;
        set
        {
            _normalPages = value;
            OnPropertyChanged();
        }
    }
    
    private ObservableCollection<FlyoutPageItem?> _footerPages = null!;
    public ObservableCollection<FlyoutPageItem?> FooterPages
    {
        get => _footerPages;
        set
        {
            _footerPages = value;
            OnPropertyChanged();
        }
    }
    
    public PageProvider()
    {
        Pages = new ObservableCollection<FlyoutPageItem?>
        {
            new FlyoutPageItem
            {
                Title = "Home",
                TargetType = typeof(HomePage),
                IsHomePage = true,
                Icon = new BitmapIcon
                {
                    UriSource = new Uri("ms-appx:///Assets/home.png"),
                },
                Tag = "Home",
            },
            new FlyoutPageItem
            {
                Title = "Updates",
                TargetType = typeof(UpdatesPage),
                Icon = new BitmapIcon
                {
                    UriSource = new Uri("ms-appx:///Assets/updates.png"),
                },
                Tag = "Updates"
            },
            new FlyoutPageItem
            {
                Title = "Settings",
                TargetType = typeof(SettingsPage),
                Icon = new BitmapIcon
                {
                    UriSource = new Uri("ms-appx:///Assets/settings.png"),
                },
                Tag = "Settings",
                IsFooter = true,
            },
        };
        
        NormalPages = new ObservableCollection<FlyoutPageItem?>(Pages.Where(page => !page!.IsFooter));
        FooterPages = new ObservableCollection<FlyoutPageItem?>(Pages.Where(page => page!.IsFooter));
    }
    
    public FlyoutPageItem? GetPageItem<T>() where T : Page
    {
        return Pages.FirstOrDefault(page => page?.TargetType == typeof(T));
    }
    
    public FlyoutPageItem? GetPageItem(Type type)
    {
        return Pages.FirstOrDefault(page => page?.TargetType == type);
    }
    
    public FlyoutPageItem? GetPageItem(int index)
    {
        return Pages.ElementAtOrDefault(index);
    }
    
    public FlyoutPageItem GetHomePageItem()
    {
        return Pages.First(page => page?.IsHomePage == true)!;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}