using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Updates.Models;

namespace GHelper.ViewModels;

public class UpdatesViewModel : IUpdatesViewModel, INotifyPropertyChanged
{
    private int _pendingUpdates;
    public int PendingUpdates
    {
        get => _pendingUpdates;
        set
        {
            _pendingUpdates = value;
            OnPropertyChanged();
        }
    }
    
    private ObservableCollection<IUpdate> _updates = null!;
    public ObservableCollection<IUpdate> Updates
    {
        get => _updates;
        set
        {
            _updates = value;
            OnPropertyChanged();
        }
    }

    private bool _isUpdating;
    public bool IsUpdating
    {
        get => _isUpdating;
        set
        {
            _isUpdating = value;
            OnPropertyChanged();
        }
    }

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

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}