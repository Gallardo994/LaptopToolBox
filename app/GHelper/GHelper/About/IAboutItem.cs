using System;

namespace GHelper.About;

public interface IAboutItem
{
    public string Title { get; }
    public string Description { get; }
    public string Link { get; }
    public string LicenseLink { get; }
}