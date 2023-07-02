using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using GHelper.AppUpdater.Downloaders.GitHub.Models;
using GHelper.AppVersion;
using Newtonsoft.Json;
using Semver;
using Serilog;

namespace GHelper.AppUpdater.Downloaders.GitHub;

public class GitHubAppUpdateDownloader : IAppUpdateDownloader
{
    private readonly IAppVersionProvider _appVersionProvider;
    
    private readonly string _repoSlug;
    private readonly string _releasesApi;
    private readonly HttpClient _httpClient;

    public GitHubAppUpdateDownloader(IAppVersionProvider appVersionProvider)
    {
        _appVersionProvider = appVersionProvider;
        
        _repoSlug = "gallardo994/g-helper";
        _releasesApi = $"https://api.github.com/repos/{_repoSlug}/releases";
        _httpClient = new HttpClient
        {
            DefaultRequestHeaders =
            {
                UserAgent = { new ProductInfoHeaderValue("GHelper", _appVersionProvider.GetCurrentVersion().ToString()) }
            }
        };
    }

    private bool TryGetVersionFromString(string version, out SemVersion semVersion)
    {
        try
        {
            semVersion = SemVersion.Parse(version, SemVersionStyles.AllowV | SemVersionStyles.OptionalPatch);
            return true;
        }
        catch (Exception e)
        {
            Log.Error(e, "Could not parse version string {Version}", version);
            semVersion = null;
            return false;
        }
    }

    private async Task<List<Release>> GetReleases()
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _releasesApi);
            var response = await _httpClient.SendAsync(request);
            var releasesStr = await response.Content.ReadAsStringAsync();
            
            var releases = JsonConvert.DeserializeObject<List<Release>>(releasesStr);
        
            return releases;
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to get releases");
            return null;
        }
    }

    private async Task<Release> GetLatestRelease(ReleaseTrack releaseTrack)
    {
        var releases = await GetReleases();
        
        if (releases == null)
        {
            Log.Error("Could not get releases");
            return null;
        }
        
        return GetLatestReleaseByTrack(releases, releaseTrack);
    }

    private bool IsNewerThanCurrent(Release release)
    {
        var currentVersion = _appVersionProvider.GetCurrentVersion();
        if (currentVersion == null)
        {
            Log.Error("Could not get current version");
            return false;
        }

        if (TryGetVersionFromString(release.TagName, out var semVersion))
        {
            return SemVersion.CompareSortOrder(semVersion, currentVersion) > 0;
        }
        
        return false;
    }

    private Release GetLatestReleaseByTrack(List<Release> releases, ReleaseTrack releaseTrack)
    {
        return releases
            .Where(release => release.IsPreRelease == (releaseTrack == ReleaseTrack.PreRelease))
            .MaxBy(release => release.PublishedAt);
    }

    public async Task<string> Download(Release release, CancellationToken cancellationToken)
    {
        var zipAsset = release.Assets.FirstOrDefault(asset => asset.ContentType == "application/x-zip-compressed");
    
        if (zipAsset == null)
        {
            return string.Empty;
        }

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, zipAsset.DownloadUrl);
            var response = await _httpClient.SendAsync(request, cancellationToken);
    
            var tempZipPath = Path.GetTempFileName();
            await using var tempZipStream = File.OpenWrite(tempZipPath);
            await response.Content.CopyToAsync(tempZipStream, cancellationToken);
        
            return tempZipPath;
        } 
        catch (OperationCanceledException)
        {
            Log.Error("Download operation was cancelled");
            return string.Empty;
        }
        catch (Exception e)
        {
            Log.Error(e, "Failed to download release");
            return string.Empty;
        }
    }


    public async Task<Release> GetSuggestedUpdate()
    {
        var currentVersion = _appVersionProvider.GetCurrentVersion();
        if (currentVersion == null)
        {
            Log.Error("Could not get current version");
            return null;
        }

        var latestTrackRelease = await GetLatestRelease(_appVersionProvider.GetCurrentReleaseTrack());
        if (latestTrackRelease == null)
        {
            Log.Error("Could not find latest release");
            return null;
        }
        
        if (!IsNewerThanCurrent(latestTrackRelease))
        {
            Log.Debug("No update available");
            return null;
        }
        
        Log.Debug("Update available");
        return latestTrackRelease;
    }
}