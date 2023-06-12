namespace GHelper.Updates;

public interface ILocalDriversVersionProvider
{
    public void Refresh();
    public string? GetLocalVersion(string deviceId);
}