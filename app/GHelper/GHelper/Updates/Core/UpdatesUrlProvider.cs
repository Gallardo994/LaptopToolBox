using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.ModelInfo;
using Ninject;

namespace GHelper.Updates.Core;

public partial class UpdatesUrlProvider : ObservableObject, IUpdatesUrlProvider
{
    [ObservableProperty] private string _driversUrl;
    [ObservableProperty] private string _biosUrl;
    
    [Inject]
    public UpdatesUrlProvider(IModelInfoProvider modelInfoProvider)
    {
        DriversUrl = $"https://rog.asus.com/support/webapi/product/GetPDDrivers?website=global&model={modelInfoProvider.Model}&cpu={modelInfoProvider.Model}&osid=52";
        BiosUrl = $"https://rog.asus.com/support/webapi/product/GetPDBIOS?website=global&model={modelInfoProvider.Model}&cpu=";
    }
}