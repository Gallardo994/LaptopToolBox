namespace GHelper.DeviceControls.Aura;

public interface IAuraCommandLoop
{
    public void Enqueue(AuraApplyCommand command);
}