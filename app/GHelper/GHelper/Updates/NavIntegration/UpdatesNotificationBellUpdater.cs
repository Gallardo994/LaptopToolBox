using System.ComponentModel;
using GHelper.Pages;
using GHelper.Updates.Core;
using Ninject;

namespace GHelper.Updates.NavIntegration;

public class UpdatesNotificationBellUpdater : IUpdatesNotificationBellUpdater
{
    private readonly IPageProvider _pageProvider;
    private readonly IUpdatesProvider _updatesProvider;
    
    [Inject]
    public UpdatesNotificationBellUpdater(IPageProvider pageProvider, IUpdatesProvider updatesProvider)
    {
        _pageProvider = pageProvider;
        _updatesProvider = updatesProvider;
    }

    public void Start()
    {
        RefreshNotificationBell();
        _updatesProvider.PropertyChanged += UpdatesProviderOnPropertyChanged;
    }
    
    public void Stop()
    {
        _updatesProvider.PropertyChanged -= UpdatesProviderOnPropertyChanged;
    }
    
    private void UpdatesProviderOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IUpdatesProvider.PendingUpdatesCount))
        {
            RefreshNotificationBell();
        }
    }

    private FlyoutPageItem GetTargetPage()
    {
        return _pageProvider.GetPageItem<UpdatesPage>();
    }
    
    private void RefreshNotificationBell()
    {
        GetTargetPage().NotificationCount = _updatesProvider.PendingUpdatesCount;
    }
}