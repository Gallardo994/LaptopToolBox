using System.Threading;
using System.Threading.Tasks;
using LaptopToolBox.AppUpdater.Downloaders.GitHub.Models;

namespace LaptopToolBox.AppUpdater.Downloaders;

public interface IAppUpdateDownloader
{
    public Task<Release> GetSuggestedUpdate();
    public Task<string> Download(Release release, CancellationToken cancellationToken);
}