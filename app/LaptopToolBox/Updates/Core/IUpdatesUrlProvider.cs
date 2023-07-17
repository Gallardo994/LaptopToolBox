using LaptopToolBox.Helpers;

namespace LaptopToolBox.Updates.Core;

public interface IUpdatesUrlProvider : IObservableObject
{
    public string DriversUrl { get; }
    public string BiosUrl { get; }
}