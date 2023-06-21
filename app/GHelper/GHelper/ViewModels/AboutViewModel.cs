using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GHelper.About;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

public class AboutViewModel : INotifyPropertyChanged
{
    private readonly IAboutProvider _aboutProvider;
    
    public ObservableCollection<IAboutItem> Items => _aboutProvider.Items;

    public AboutViewModel()
    {
        _aboutProvider = Services.ResolutionRoot.Get<IAboutProvider>();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}