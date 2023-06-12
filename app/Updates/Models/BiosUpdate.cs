namespace GHelper.Updates.Models;

public class BiosUpdate : IUpdate
{
    public string Name { get; init; }
    public string Version { get; init; }
    public string DownloadUrl { get; init; }
    public bool IsNewerThanCurrent { get; init; }
}