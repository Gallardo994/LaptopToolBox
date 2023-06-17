namespace GHelper.DeviceControls.Aura;

public class AuraSpeedModel
{
    public int Index { get; set; }
    public AuraSpeed Speed { get; set; }
    
    public AuraSpeedModel(int index, AuraSpeed speed)
    {
        Index = index;
        Speed = speed;
    }
}