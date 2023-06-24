using GHelper.Helpers;

namespace GHelper.About;

public interface IAboutItem : IObservableObject
{
    public string Title { get; }
    public string Description { get; }
    public string Link { get; }
    public string LicenseLink { get; }
}