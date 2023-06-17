using GHelper.ModelInfo;
using Ninject;

namespace GHelper.Updates.Core;

public class UpdatesUrlProvider : IUpdatesUrlProvider
{
    public string DriversUrl { get; private set; }
    public string BiosUrl { get; private set; }
    
    [Inject]
    public UpdatesUrlProvider(IModelInfoProvider modelInfoProvider)
    {
        DriversUrl = $"https://rog.asus.com/support/webapi/product/GetPDDrivers?website=global&model={modelInfoProvider.Model}&cpu={modelInfoProvider.Model}&osid=52";
        BiosUrl = $"https://rog.asus.com/support/webapi/product/GetPDBIOS?website=global&model={modelInfoProvider.Model}&cpu=";
    }
}