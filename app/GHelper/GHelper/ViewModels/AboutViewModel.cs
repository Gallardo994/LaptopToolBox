using System.Collections.ObjectModel;
using GHelper.About;
using GHelper.Injection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Ninject;

namespace GHelper.ViewModels;

public class AboutViewModel : ObservableObject
{
    private readonly IAboutProvider _aboutProvider;
    
    public ObservableCollection<IAboutItem> Items => _aboutProvider.Items;

    public AboutViewModel()
    {
        _aboutProvider = Services.ResolutionRoot.Get<IAboutProvider>();
    }
}