using System.Collections.ObjectModel;
using LaptopToolBox.Helpers;

namespace LaptopToolBox.DeviceControls.Display.Screens;

public interface IScreenProvider : IObservableObject
{
    public ObservableCollection<IScreenItem> Items { get; }
    public void Refresh();
}