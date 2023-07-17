using LaptopToolBox.Helpers;

namespace LaptopToolBox.ModelInfo;

public interface IModelInfoProvider : IObservableObject
{
    public string Model { get; }
    public int Bios { get; }
}