using System.Management.Automation;
using LaptopToolBox.Commands;
using Ninject;

namespace LaptopToolBox.DeviceControls.TouchPad;

public class WindowsPnpTouchPadControl : ITouchPadControl
{
    private readonly IBackgroundCommandLoop _backgroundCommandLoop;
    
    private readonly PowerShell _powerShell;
    private readonly PnpTouchPadHandle _touchPadHandle;

    public bool IsAvailable => !_touchPadHandle.IsNullOrEmpty();
    
    [Inject]
    public WindowsPnpTouchPadControl(IBackgroundCommandLoop backgroundCommandLoop)
    {
        _backgroundCommandLoop = backgroundCommandLoop;
        
        _powerShell = PowerShell.Create();
        _touchPadHandle = new PnpTouchPadHandle();
        
        _backgroundCommandLoop.Enqueue(new PnpDetectTouchPadCommand(_touchPadHandle, _powerShell));
    }

    public void SetState(bool state)
    {
        if (_touchPadHandle.IsNullOrEmpty())
        {
            return;
        }
        
        if (state == _touchPadHandle.State)
        {
            return;
        }
        
        _touchPadHandle.State = state;
        _backgroundCommandLoop.Enqueue(new PnpSetDeviceStateCommand(_powerShell, _touchPadHandle, state));
    }

    public bool GetState()
    {
        return _touchPadHandle.State;
    }
}