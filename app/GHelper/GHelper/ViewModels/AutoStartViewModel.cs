using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.AutoStart;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels;

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