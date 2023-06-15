using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Pages;
using GHelper.Resources.Strings;

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
                Title = AppResources.page_home,
                TargetType = typeof(MainPage),
                IsHomePage = true,
                Icon = "home.png",
            },
            new FlyoutPageItem
            {
                Title = AppResources.page_updates,
                TargetType = typeof(UpdatesPage),
                Icon = "download.png",
            },
            new FlyoutPageItem
            {
                Title = AppResources.page_settings,
                TargetType = typeof(SettingsPage),
                Icon = "settings.png",
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