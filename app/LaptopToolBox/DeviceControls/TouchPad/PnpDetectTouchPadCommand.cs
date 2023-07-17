using System.Linq;
using System.Management.Automation;
using LaptopToolBox.Commands;
using Serilog;

namespace LaptopToolBox.DeviceControls.TouchPad;

public class PnpDetectTouchPadCommand : IBackgroundCommand
{
    private readonly PnpTouchPadHandle _touchPadHandle;
    private readonly PowerShell _powerShell;

    public PnpDetectTouchPadCommand(PnpTouchPadHandle touchPadHandle, PowerShell powerShell)
    {
        _touchPadHandle = touchPadHandle;
        _powerShell = powerShell;
    }

    public void Execute()
    {
        _powerShell.Commands.Clear();
        _powerShell.AddCommand("Set-ExecutionPolicy")
            .AddParameter("ExecutionPolicy", "Bypass")
            .AddParameter("Scope", "Process")
            .Invoke();
        
        _powerShell.Commands.Clear();
        _powerShell.AddCommand("Import-Module")
            .AddParameter("Name", "PnpDevice")
            .Invoke();
        
        _powerShell.Commands.Clear();
        _powerShell.AddScript("Get-PnpDevice | Where-Object {$_.FriendlyName -like '*Touchpad*'} | Select-Object -Property DeviceID");
        
        var results = _powerShell.Invoke();

        _touchPadHandle.DeviceId = results.Count > 0 ? results.First().Members["DeviceID"].Value.ToString() : string.Empty;
        
        Log.Information($"Touchpad Device ID: {_touchPadHandle.DeviceId}");

        if (_touchPadHandle.IsNullOrEmpty())
        {
            return;
        }
        
        _touchPadHandle.State = ReadState();
    }
    
    private bool ReadState()
    {
        if (_touchPadHandle.IsNullOrEmpty())
        {
            return false;
        }
        
        _powerShell.Commands.Clear();
        _powerShell.AddScript($"Get-PnpDevice -InstanceId \"{_touchPadHandle.DeviceId}\" | Select-Object -ExpandProperty Status");
        
        var result = _powerShell.Invoke();

        return result.First().ToString() == "OK";
    }
}