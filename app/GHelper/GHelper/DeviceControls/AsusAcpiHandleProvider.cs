using System;

namespace GHelper.DeviceControls;

public class AsusAcpiHandleProvider : IAcpiHandleProvider
{
    private IntPtr _handle;

    private const uint GenericRead = 0x80000000;
    private const uint GenericWrite = 0x40000000;
    private const uint OpenExisting = 3;
    private const uint FileAttributeNormal = 0x80;
    private const uint FileShareRead = 1;
    private const uint FileShareWrite = 2;
    
    public AsusAcpiHandleProvider()
    {
        _handle = IntPtr.Zero;
    }
    
    public bool TryGet(out IntPtr handle)
    {
        if (_handle == IntPtr.Zero)
        {
            _handle = Native.CreateFile(
                @"\\.\\ATKACPI",
                GenericRead | GenericWrite,
                FileShareRead | FileShareWrite,
                IntPtr.Zero,
                OpenExisting,
                FileAttributeNormal,
                IntPtr.Zero
            );
            
            if (_handle == new IntPtr(-1) || _handle == IntPtr.Zero)
            {
                handle = IntPtr.Zero;
            }
        }
        
        handle = _handle;
        return _handle != IntPtr.Zero;
    }
    
    public void Dispose()
    {
        if (_handle == IntPtr.Zero)
        {
            return;
        }
        
        Native.CloseHandle(_handle);
        _handle = IntPtr.Zero;
    }
}