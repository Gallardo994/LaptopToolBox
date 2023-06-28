using System.Management.Automation;
using GHelper.Commands;

namespace GHelper.DeviceControls.TouchPad;

public class PnpSetDeviceStateCommand : IBackgroundCommand
{
    private readonly PowerShell _powerShell;
    private readonly string _deviceName;
    private readonly bool _state;
    
    public PnpSetDeviceStateCommand(PowerShell powerShell, string deviceName, bool state)
    {
        _powerShell = powerShell;
        _deviceName = deviceName;
        _state = state;
    }
    
    public void Execute()
    {
        _powerShell.Commands.Clear();

        _powerShell.AddScript(_state
            ? $"Get-PnpDevice -InstanceId \"{_deviceName}\" | Enable-PnpDevice -Confirm:$false"
            : $"Get-PnpDevice -InstanceId \"{_deviceName}\" | Disable-PnpDevice -Confirm:$false");

        _powerShell.Invoke();
    }
}