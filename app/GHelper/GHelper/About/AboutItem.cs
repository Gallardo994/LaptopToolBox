using System;

namespace GHelper.About;

public class AboutItem : IAboutItem
{
    public string Title { get; init; }
    public string Description { get; init; }
    public string Link { get; init; }
    public string LicenseLink { get; init; }
}