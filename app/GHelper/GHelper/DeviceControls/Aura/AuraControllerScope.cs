using System;

namespace GHelper.DeviceControls.Aura;

public readonly struct AuraControllerScope : IDisposable
{
    private readonly IAuraController _controller;
        
    public AuraControllerScope(IAuraController controller)
    {
        _controller = controller;
        _controller.IsScopeActive = true;
    }
        
    public void Dispose()
    {
        _controller.IsScopeActive = false;
        _controller.Refresh();
    }
}