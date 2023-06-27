using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace GHelper.DeviceControls.Fans;

[JsonObject(MemberSerialization.OptIn)]
public partial class FanCurvePoint : ObservableObject
{
    [JsonProperty("value")] [ObservableProperty] private byte _value;
    
    public FanCurvePoint()
    {
        
    }
    
    public FanCurvePoint(FanCurvePoint fanCurvePoint)
    {
        Value = fanCurvePoint.Value;
    }
    
    public FanCurvePoint(byte value)
    {
        Value = value;
    }
}