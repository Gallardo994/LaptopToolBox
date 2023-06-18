namespace GHelper.DeviceControls.Lighting.Aura;

public interface IAuraCommandLoop
{
    public void Enqueue(AuraApplyCommand command);
}