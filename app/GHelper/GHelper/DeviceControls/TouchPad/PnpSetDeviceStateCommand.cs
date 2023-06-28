using System.Management.Automation;
using GHelper.Commands;

namespace GHelper.DeviceControls.TouchPad;

public class PnpSetDeviceStateCommand : IBackgroundCommand
{
    private readonly PowerShell _powerShell;
    private readonly PnpTouchPadHandle _touchPadHandle;
    private readonly bool _state;
    
    public PnpSetDeviceStateCommand(PowerShell powerShell, PnpTouchPadHandle touchPadHandle, bool state)
    {
        _powerShell = powerShell;
        _touchPadHandle = touchPadHandle;
        _state = state;
    }
    
    public void Execute()
    {
        _powerShell.Commands.Clear();
        
        var deviceName = _touchPadHandle.DeviceId;

        _powerShell.AddScript(_state
            ? $"Get-PnpDevice -InstanceId \"{deviceName}\" | Enable-PnpDevice -Confirm:$false"
            : $"Get-PnpDevice -InstanceId \"{deviceName}\" | Disable-PnpDevice -Confirm:$false");

        _powerShell.Invoke();
    }
}