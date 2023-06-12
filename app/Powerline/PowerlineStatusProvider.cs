using Microsoft.Win32;

namespace GHelper.Powerline;

public class PowerlineStatusProvider : IPowerlineStatusProvider
{
    public event Action<PowerLineStatus> PowerlineStatusChanged;
    public PowerLineStatus IsPlugged => SystemInformation.PowerStatus.PowerLineStatus;
    
    
    public PowerlineStatusProvider()
    {
        SystemEvents.PowerModeChanged += OnPowerModeChanged;
    }
    
    private void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
        PowerlineStatusChanged?.Invoke(IsPlugged);
    }
    
    public void Dispose()
    {
        SystemEvents.PowerModeChanged -= OnPowerModeChanged;
    }
}