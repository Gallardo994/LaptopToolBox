using GHelper.Updates.Models.Updates;
using Newtonsoft.Json;
using Ninject;
using Serilog;

namespace GHelper.Updates;

public class UpdatesChecker : IUpdatesChecker
{
    private readonly IUpdatesUrlProvider _updatesUrlProvider;
    private readonly IModelInfoProvider _modelInfoProvider;
    private readonly ILocalDriversVersionProvider _localDriversVersionProvider;
    private readonly HttpClient _httpClient;

    public bool IsCheckingForUpdates { get; private set; }
    public int PendingUpdatesCount { get; private set; }
    public List<IUpdate> AllUpdates { get; private set; }


    [Inject]
    public UpdatesChecker(IUpdatesUrlProvider updatesUrlProvider,
        IModelInfoProvider modelInfoProvider,
        ILocalDriversVersionProvider localDriversVersionProvider,
        HttpClient httpClient)
    {
        _updatesUrlProvider = updatesUrlProvider;
        _modelInfoProvider = modelInfoProvider;
        _localDriversVersionProvider = localDriversVersionProvider;
        _httpClient = httpClient;

        AllUpdates = new List<IUpdate>();
    }

    public bool CheckForUpdates()
    {
        if (IsCheckingForUpdates)
        {
            return false;
        }

        IsCheckingForUpdates = true;

        AllUpdates.Clear();

        Task.Run(async () =>
        {
            _localDriversVersionProvider.Refresh();
            
            var biosUpdates = GetBiosUpdates();
            var driverUpdates = GetDriverUpdates();
            
            await Task.WhenAll(biosUpdates, driverUpdates);

            AllUpdates.AddRange(biosUpdates.Result);
            AllUpdates.AddRange(driverUpdates.Result);

            PendingUpdatesCount = AllUpdates.Count(update => update.IsNewerThanCurrent);

            IsCheckingForUpdates = false;
            
            Log.Debug("Checked for updates, total: {Total}, pending: {Pending}", AllUpdates.Count, PendingUpdatesCount);
        }).Forget();

        return true;
    }

    private async Task<List<IUpdate>> GetBiosUpdates()
    {
        var updates = new List<IUpdate>();

        var data = await PerformRequest<DriversModel>(_updatesUrlProvider.BiosUrl);

        if (data == null)
        {
            Log.Debug("Failed to get BIOS data");
            return updates;
        }

        var repeatingTitles = new HashSet<string>();

        foreach (var group in data.Result.Obj)
        {
            foreach (var file in group.Files)
            {
                if (!repeatingTitles.Add(file.Title))
                {
                    continue;
                }
                
                var newer = int.TryParse(file.Version, out var intVersion) && intVersion > _modelInfoProvider.GetNumericBiosVersion();

                var biosUpdate = new BiosUpdate
                {
                    Name = file.Title,
                    Version = file.Version,
                    DownloadUrl = file.DownloadUrl.Global,
                    IsNewerThanCurrent = newer,
                };

                updates.Add(biosUpdate);
            }
        }

        return updates;
    }

    private async Task<List<IUpdate>> GetDriverUpdates()
    {
        var updates = new List<IUpdate>();

        var requestTask = PerformRequest<DriversModel>(_updatesUrlProvider.DriversUrl);

        var data = await requestTask;

        if (data == null)
        {
            Log.Debug("Failed to get drivers data");
            return updates;
        }
        
        var repeatingTitles = new HashSet<string>();
        
        foreach (var group in data.Result.Obj)
        {
            foreach (var file in group.Files)
            {
                if (!repeatingTitles.Add(file.Title))
                {
                    continue;
                }
                
                var isNewer = false;

                foreach (var hardwareInfo in file.HardwareInfoList)
                {
                    var localVersionString = _localDriversVersionProvider.GetLocalVersion(hardwareInfo.HardwareId);
                    
                    if (localVersionString == null)
                    {
                        continue;
                    }

                    if (TryParseVersion(file.Version, out var remoteVersion) && TryParseVersion(localVersionString, out var localVersion))
                    {
                        isNewer = remoteVersion.CompareTo(localVersion) > 0;
                    }
                    
                    if (isNewer)
                    {
                        break;
                    }
                }
                
                var driverUpdate = new DriverUpdate
                {
                    Name = file.Title,
                    Version = file.Version,
                    DownloadUrl = file.DownloadUrl.Global,
                    IsNewerThanCurrent = isNewer,
                };

                updates.Add(driverUpdate);
            }
        }

        return updates;
    }

    private async Task<T?> PerformRequest<T>(string url) where T : class
    {
        var request = new HttpRequestMessage(HttpMethod.Get, url);

        var attempt = 0;
            
        while (attempt < 3)
        {
            try
            {
                var response = await _httpClient.SendAsync(request);
                var stream = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<T>(stream);
                    
                return data;
            }
            catch (Exception e)
            {
                attempt++;
                Log.Debug($"Failed to get data from {url} ({e.Message}), attempt {attempt}");
            }
        }

        return null;
    }

    private bool TryParseVersion(string versionString, out Version version)
    {
        if (string.IsNullOrEmpty(versionString))
        {
            version = null;
            return false;
        }
        
        if (versionString.StartsWith("V"))
        {
            versionString = versionString.Substring(1);
        }

        if (versionString.Contains("."))
        {
            return Version.TryParse(versionString, out version);
        }

        if (int.TryParse(versionString, out var intVersion))
        {
            version = new Version(intVersion, 0);
            return true;
        }

        version = null;
        return false;
    }
}