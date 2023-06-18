using System;

namespace GHelper.DeviceControls;

public interface IAcpiHandleProvider : IDisposable
{
    public bool TryGet(out IntPtr handle);
}