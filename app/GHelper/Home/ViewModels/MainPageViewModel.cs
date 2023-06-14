using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GHelper.Home.ViewModels;

public class MainPageViewModel : IMainPageViewModel, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}