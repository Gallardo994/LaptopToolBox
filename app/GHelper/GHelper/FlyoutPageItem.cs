using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace GHelper;

public partial class FlyoutPageItem : ObservableObject
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private Type _targetType;
    [ObservableProperty] private IconElement _icon;
    [ObservableProperty] private bool _isHomePage;
    [ObservableProperty] private string _tag;
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(NotificationsVisible))] private int _notificationCount;
    [ObservableProperty] private bool _isFooter;
    
    public Visibility NotificationsVisible => NotificationCount > 0 ? Visibility.Visible : Visibility.Collapsed;
}