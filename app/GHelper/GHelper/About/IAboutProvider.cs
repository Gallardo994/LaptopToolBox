using System.Collections.ObjectModel;

namespace GHelper.About;

public interface IAboutProvider
{
    public ObservableCollection<IAboutItem> Items { get; }
}