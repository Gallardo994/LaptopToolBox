using CommunityToolkit.Mvvm.ComponentModel;

namespace GHelper.About;

public partial class AboutItem : ObservableObject, IAboutItem
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private string _description;
    [ObservableProperty] private string _link;
    [ObservableProperty] private string _licenseLink;
}