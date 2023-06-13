using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.Updates.Models;

namespace GHelper.Updates;

public class UpdatesViewModel : INotifyPropertyChanged
{
    private int _checkId;
    public int CheckId
    {
        get => _checkId;
        set
        {
            _checkId = value;
            OnPropertyChanged();
        }
    }

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
    
    public UpdatesViewModel()
    {
        CheckId = 0;
        PendingUpdates = 0;
        Updates = new ObservableCollection<IUpdate>();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}