using System.Threading;
using System.Threading.Tasks;
using GHelper.AppUpdater.Downloaders.GitHub.Models;

namespace GHelper.AppUpdater.Downloaders;

public interface IAppUpdateDownloader
{
    public Task<Release> GetSuggestedUpdate();
    public Task<string> Download(Release release, CancellationToken cancellationToken);
}