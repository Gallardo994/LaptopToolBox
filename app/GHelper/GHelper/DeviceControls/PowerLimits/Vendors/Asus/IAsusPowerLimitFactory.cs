using GHelper.ModelInfo;

namespace GHelper.DeviceControls.PowerLimits.Vendors.Asus;

public interface IAsusPowerLimitFactory
{
    public IPowerLimitRangeInfo Resolve(IModelInfoProvider modelInfoProvider);
}