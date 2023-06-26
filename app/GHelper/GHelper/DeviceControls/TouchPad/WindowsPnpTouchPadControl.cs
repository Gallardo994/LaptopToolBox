using System.Linq;
using System.Management.Automation;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.TouchPad;

public class WindowsPnpTouchPadControl : ITouchPadControl
{
    private readonly PowerShell _powerShell;
    private readonly string _touchpadDeviceId;
    
    public bool IsAvailable => !string.IsNullOrEmpty(_touchpadDeviceId);
    
    [Inject]
    public WindowsPnpTouchPadControl()
    {
        _powerShell = PowerShell.Create();
        
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

        _touchpadDeviceId = results.Count > 0 ? results.First().Members["DeviceID"].Value.ToString() : string.Empty;
        
        Log.Information($"Touchpad Device ID: {_touchpadDeviceId}");
    }

    public void SetState(bool state)
    {
        if (string.IsNullOrEmpty(_touchpadDeviceId))
        {
            return;
        }
        
        _powerShell.Commands.Clear();

        _powerShell.AddScript(state
            ? $"Get-PnpDevice -InstanceId \"{_touchpadDeviceId}\" | Enable-PnpDevice -Confirm:$false"
            : $"Get-PnpDevice -InstanceId \"{_touchpadDeviceId}\" | Disable-PnpDevice -Confirm:$false");

        _powerShell.Invoke();
    }
    
    public bool GetState()
    {
        if (string.IsNullOrEmpty(_touchpadDeviceId))
        {
            return false;
        }
        
        _powerShell.Commands.Clear();
        _powerShell.AddScript($"Get-PnpDevice -InstanceId \"{_touchpadDeviceId}\" | Select-Object -ExpandProperty Status");
        
        var result = _powerShell.Invoke();

        return result.First().ToString() == "OK";
    }
}