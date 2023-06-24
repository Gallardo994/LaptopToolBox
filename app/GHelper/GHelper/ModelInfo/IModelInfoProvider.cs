using GHelper.Helpers;

namespace GHelper.ModelInfo;

public interface IModelInfoProvider : IObservableObject
{
    public string Model { get; }
    public int Bios { get; }
}