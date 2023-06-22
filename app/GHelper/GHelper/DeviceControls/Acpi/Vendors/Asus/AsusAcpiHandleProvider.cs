using System;
using System.IO;
using Vanara.PInvoke;

namespace GHelper.DeviceControls.Acpi.Vendors.Asus;

public class AsusAcpiHandleProvider
{
    private Kernel32.SafeHFILE _handle;

    public AsusAcpiHandleProvider()
    {
        _handle = null;
    }
    
    public bool TryGet(out Kernel32.SafeHFILE handle)
    {
        if (_handle == null || _handle.IsInvalid)
        {
            _handle = Kernel32.CreateFile(
                @"\\.\\ATKACPI",
                Kernel32.FileAccess.GENERIC_READ | Kernel32.FileAccess.GENERIC_WRITE,
                FileShare.Read | FileShare.Write,
                null,
                FileMode.Open,
                FileFlagsAndAttributes.FILE_ATTRIBUTE_NORMAL,
                IntPtr.Zero
            );
        }

        if (_handle == null || _handle.IsInvalid)
        {
            handle = null;
            return false;
        }

        handle = _handle;
        return true;
    }
    
    public void Dispose()
    {
        if (_handle == null || _handle.IsInvalid)
        {
            return;
        }
        
        _handle.Close();
        _handle = null;
    }
}