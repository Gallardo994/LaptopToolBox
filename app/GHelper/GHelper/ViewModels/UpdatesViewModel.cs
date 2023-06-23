using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.Updates.Models;

namespace GHelper.ViewModels;

public partial class UpdatesViewModel : ObservableObject
{
    [ObservableProperty] private int _pendingUpdates;
    [ObservableProperty] private ObservableCollection<IUpdate> _updates = null!;
    [ObservableProperty] private bool _isUpdating;

    public UpdatesViewModel()
    {
        PendingUpdates = 0;
        Updates = new ObservableCollection<IUpdate>();
    }
    
    public void SetUpdates(List<IUpdate> updates)
    {
        Updates.Clear();
        
        foreach (var update in updates)
        {
            if (update.IsNewerThanCurrent)
            {
                PendingUpdates++;
            }
            
            Updates.Add(update);
        }
    }
}