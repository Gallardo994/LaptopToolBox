using System.Collections.ObjectModel;
using GHelper.Helpers;

namespace GHelper.DeviceControls.Display.Screens;

public interface IScreenProvider : IObservableObject
{
    public ObservableCollection<IScreenItem> Items { get; }
    public void Refresh();
}