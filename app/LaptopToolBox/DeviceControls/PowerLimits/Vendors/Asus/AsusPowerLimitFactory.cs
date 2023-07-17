using System;
using System.Collections.Generic;
using LaptopToolBox.DeviceControls.PowerLimits.Vendors.Asus.Models;
using LaptopToolBox.ModelInfo;

namespace LaptopToolBox.DeviceControls.PowerLimits.Vendors.Asus;

public class AsusPowerLimitFactory : IAsusPowerLimitFactory
{
    private readonly Dictionary<string, Type> _powerLimitRangeInfoByModelName;
    private readonly IPowerLimitRangeInfo _defaultPowerLimitRangeInfo;

    public AsusPowerLimitFactory()
    {
        _powerLimitRangeInfoByModelName = new Dictionary<string, Type>();
        _defaultPowerLimitRangeInfo = new PowerLimitInfo_Default();

        ResolvePowerLimitRangeInfoByModelName();
    }
    
    private void ResolvePowerLimitRangeInfoByModelName()
    {
        _powerLimitRangeInfoByModelName.Add("G733PY", typeof(PowerLimitInfo_G733PY));
        _powerLimitRangeInfoByModelName.Add("G513RW", typeof(PowerLimitInfo_G513RW));
    }
    
    public IPowerLimitRangeInfo Resolve(IModelInfoProvider modelInfoProvider)
    {
        var modelName = modelInfoProvider.Model;
        
        if (_powerLimitRangeInfoByModelName.TryGetValue(modelName, out var value))
        {
            return (IPowerLimitRangeInfo) Activator.CreateInstance(value);
        }
        
        return _defaultPowerLimitRangeInfo;
    }
}