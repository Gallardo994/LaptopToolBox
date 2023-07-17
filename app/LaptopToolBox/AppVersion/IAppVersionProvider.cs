using LaptopToolBox.AppUpdater;
using Semver;

namespace LaptopToolBox.AppVersion;

public interface IAppVersionProvider
{
    public SemVersion GetCurrentVersion();
    public ReleaseTrack GetCurrentReleaseTrack();
}