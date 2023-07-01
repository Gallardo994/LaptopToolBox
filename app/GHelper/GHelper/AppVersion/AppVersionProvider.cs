using Semver;

namespace GHelper.AppVersion;

public class AppVersionProvider : IAppVersionProvider
{
    private static readonly SemVersion CurrentVersion = SemVersion.ParsedFrom(1, 18, 0, "beta");
    
    public SemVersion GetCurrentVersion()
    {
        return CurrentVersion;
    }
}