using System.Threading;
using System.Threading.Tasks;
using GHelper.AppUpdater.GitHub.Models;
using Semver;

namespace GHelper.AppUpdater;

public interface IAppUpdater
{
    public Task<Release> GetSuggestedUpdate();
    public Task<string> Download(Release release, CancellationToken cancellationToken);
    public SemVersion GetCurrentVersion();
}