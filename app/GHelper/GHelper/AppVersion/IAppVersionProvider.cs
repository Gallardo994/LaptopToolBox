using GHelper.AppUpdater;
using Semver;

namespace GHelper.AppVersion;

public interface IAppVersionProvider
{
    public SemVersion GetCurrentVersion();
    public ReleaseTrack GetCurrentReleaseTrack();
}