using System;

namespace GHelper.DeviceControls;

public interface IAcpiHandleProvider
{
    public bool TryGet(out IntPtr handle);
}