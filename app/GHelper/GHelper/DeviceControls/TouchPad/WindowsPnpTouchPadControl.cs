using System.Linq;
using System.Management.Automation;
using GHelper.Commands;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.TouchPad;

public class WindowsPnpTouchPadControl : ITouchPadControl
{
    private readonly IBackgroundCommandLoop _backgroundCommandLoop;
    
    private readonly PowerShell _powerShell;
    private readonly string _touchpadDeviceId;

    public bool IsAvailable => !string.IsNullOrEmpty(_touchpadDeviceId);
    
    private bool _state;

    [Inject]
    public WindowsPnpTouchPadControl(IBackgroundCommandLoop backgroundCommandLoop)
    {
        _backgroundCommandLoop = backgroundCommandLoop;
        
        _state = true;
        
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
        
        if (string.IsNullOrEmpty(_touchpadDeviceId))
        {
            return;
        }
        
        _state = ReadState();
    }

    public void SetState(bool state)
    {
        if (string.IsNullOrEmpty(_touchpadDeviceId))
        {
            return;
        }
        
        _state = state;
        _backgroundCommandLoop.Enqueue(new PnpSetDeviceStateCommand(_powerShell, _touchpadDeviceId, state));
    }

    public bool GetState()
    {
        return _state;
    }
    
    private bool ReadState()
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