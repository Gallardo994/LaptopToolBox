using System;

namespace GHelper.DeviceControls.Acpi;

public interface IAcpiHandleProvider : IDisposable
{
    public bool TryGet(out IntPtr handle);
}