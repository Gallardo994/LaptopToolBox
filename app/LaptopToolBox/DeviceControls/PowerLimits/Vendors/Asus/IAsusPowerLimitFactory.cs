using LaptopToolBox.ModelInfo;

namespace LaptopToolBox.DeviceControls.PowerLimits.Vendors.Asus;

public interface IAsusPowerLimitFactory
{
    public IPowerLimitRangeInfo Resolve(IModelInfoProvider modelInfoProvider);
}