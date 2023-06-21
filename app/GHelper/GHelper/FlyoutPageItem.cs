using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml.Controls;

namespace GHelper;

public class FlyoutPageItem : INotifyPropertyChanged
{
    public string Title { get; set; }
    public Type TargetType { get; set; }
    public IconElement Icon { get; set; }
    public bool IsHomePage { get; set; }
    public string Tag { get; set; }

    private int _notificationCount;
    public int NotificationCount 
    {
        get => _notificationCount;
        set
        {
            _notificationCount = value;
            OnPropertyChanged();
        }
    }
    public bool IsFooter { get; set; }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}