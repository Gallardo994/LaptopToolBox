using System.Collections.ObjectModel;

namespace GHelper;

public interface IPageProvider
{
    public ObservableCollection<FlyoutPageItem?> Pages { get; set; }
    public FlyoutPageItem? GetPageItem<T>() where T : Page;
    public FlyoutPageItem? GetPageItem(Type type);
    public FlyoutPageItem? GetPageItem(int index);
    public FlyoutPageItem GetHomePageItem();
}