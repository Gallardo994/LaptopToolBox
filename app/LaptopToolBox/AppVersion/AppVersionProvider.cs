using LaptopToolBox.AppUpdater;
using Semver;

namespace LaptopToolBox.AppVersion;

public class AppVersionProvider : IAppVersionProvider
{
    private static readonly SemVersion CurrentVersion = SemVersion.ParsedFrom(2, 1, 0, "beta");
    
    public SemVersion GetCurrentVersion()
    {
        return CurrentVersion;
    }
    
    public ReleaseTrack GetCurrentReleaseTrack()
    {
        return GetCurrentVersion().IsPrerelease ? ReleaseTrack.PreRelease : ReleaseTrack.Stable;
    }
}