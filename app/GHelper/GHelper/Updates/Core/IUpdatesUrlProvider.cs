using GHelper.Helpers;

namespace GHelper.Updates.Core;

public interface IUpdatesUrlProvider : IObservableObject
{
    public string DriversUrl { get; }
    public string BiosUrl { get; }
}