using Ninject;

namespace GHelper.Updates;

public class UpdatesChecker : IUpdatesChecker
{
    private readonly IUpdatesUrlProvider _updatesUrlProvider;
    private readonly IModelInfoProvider _modelInfoProvider;
    
    private readonly HttpClient _httpClient;
    
    [Inject]
    public UpdatesChecker(IUpdatesUrlProvider updatesUrlProvider,
        IModelInfoProvider modelInfoProvider,
        HttpClient httpClient)
    {
        _updatesUrlProvider = updatesUrlProvider;
        _modelInfoProvider = modelInfoProvider;
        _httpClient = httpClient;
    }

    public void CheckForUpdates()
    {
    }
}