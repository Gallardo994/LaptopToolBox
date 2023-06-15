﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
    
    public PageProvider()
    {
        Pages = new ObservableCollection<FlyoutPageItem?>
        {
            new FlyoutPageItem
            {
                Title = "Home",
                TargetType = typeof(MainPage),
                IsHomePage = true,
            },
            new FlyoutPageItem
            {
                Title = "Updates",
                TargetType = typeof(UpdatesPage)
            },
            new FlyoutPageItem
            {
                Title = "Settings",
                TargetType = typeof(SettingsPage)
            },
        };
    }
    
    public FlyoutPageItem? GetPageItem<T>() where T : Page
    {
        return Pages.FirstOrDefault(page => page?.TargetType == typeof(T));
    }
    
    public FlyoutPageItem? GetPageItem(Type type)
    {
        return Pages.FirstOrDefault(page => page?.TargetType == type);
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