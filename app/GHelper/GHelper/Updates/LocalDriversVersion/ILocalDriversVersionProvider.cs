namespace GHelper.Updates.LocalDriversVersion;

public interface ILocalDriversVersionProvider
{
    public void Refresh();
    public string? GetLocalVersion(string deviceId);
}