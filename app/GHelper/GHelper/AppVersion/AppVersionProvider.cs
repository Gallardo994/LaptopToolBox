using GHelper.AppUpdater;
using Semver;

namespace GHelper.AppVersion;

public class AppVersionProvider : IAppVersionProvider
{
    private static readonly SemVersion CurrentVersion = SemVersion.ParsedFrom(1, 22, 1, "beta");
    
    public SemVersion GetCurrentVersion()
    {
        return CurrentVersion;
    }
    
    public ReleaseTrack GetCurrentReleaseTrack()
    {
        return GetCurrentVersion().IsPrerelease ? ReleaseTrack.PreRelease : ReleaseTrack.Stable;
    }
}