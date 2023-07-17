using System.Collections.ObjectModel;

namespace LaptopToolBox.About;

public interface IAboutProvider
{
    public ObservableCollection<IAboutItem> Items { get; }
}