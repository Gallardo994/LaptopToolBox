using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace GHelper.DeviceControls.Fans;

[JsonObject(MemberSerialization.OptIn)]
public partial class FanCurvePoint : ObservableObject
{
    [JsonProperty("temperature")] [ObservableProperty] private byte _temperature;
    [JsonProperty("value")] [ObservableProperty] private byte _value;
    
    public FanCurvePoint()
    {
        
    }
    
    public FanCurvePoint(FanCurvePoint other)
    {
        Temperature = other.Temperature;
        Value = other.Value;
    }
    
    public FanCurvePoint(byte temperature, byte value)
    {
        Temperature = temperature;
        Value = value;
    }
    
    public bool HasModificationsComparedTo(FanCurvePoint other)
    {
        return other.Temperature != Temperature || other.Value != Value;
    }
}