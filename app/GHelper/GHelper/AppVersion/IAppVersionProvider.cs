using Semver;

namespace GHelper.AppVersion;

public interface IAppVersionProvider
{
    public SemVersion GetCurrentVersion();
}