using CommunityToolkit.Mvvm.ComponentModel;
using LaptopToolBox.AutoStart;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels;

public partial class AutoStartViewModel : ObservableObject
{
    private readonly IAutoStartController _autoStartController;

    [ObservableProperty] private bool _isAutoStartEnabled;

    public AutoStartViewModel()
    {
        _autoStartController = Services.ResolutionRoot.Get<IAutoStartController>();
        
        IsAutoStartEnabled = _autoStartController.IsAutoStartEnabled();
        
        PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(IsAutoStartEnabled))
            {
                _autoStartController.SetAutoStart(IsAutoStartEnabled);
            }
        };
    }
}