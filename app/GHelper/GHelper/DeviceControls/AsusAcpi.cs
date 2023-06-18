using System;
using Serilog;

namespace GHelper.DeviceControls;

public class AsusAcpi : IAcpi
{
    private IntPtr _handle;
    
    const uint ControlCode = 0x0022240C;
    const uint Devs = 0x53564544;
    
    const string FileName = @"\\.\\ATKACPI";
    private const uint GenericRead = 0x80000000;
    private const uint GenericWrite = 0x40000000;
    private const uint OpenExisting = 3;
    private const uint FileAttributeNormal = 0x80;
    private const uint FileShareRead = 1;
    private const uint FileShareWrite = 2;

    private bool EnsureHandle()
    {
        if (_handle == new IntPtr(-1) || _handle == IntPtr.Zero)
        {
            _handle = Native.CreateFile(
                FileName,
                GenericRead | GenericWrite,
                FileShareRead | FileShareWrite,
                IntPtr.Zero,
                OpenExisting,
                FileAttributeNormal,
                IntPtr.Zero
            );
        }

        return _handle != new IntPtr(-1) && _handle != IntPtr.Zero;
    }
    
    public int DeviceSet(uint deviceId, int status, string logName)
    {
        var args = new byte[8];
        BitConverter.GetBytes((uint)deviceId).CopyTo(args, 0);
        BitConverter.GetBytes((uint)status).CopyTo(args, 4);

        var callStatus = CallMethod(Devs, args);
        var result = BitConverter.ToInt32(callStatus, 0);

        Log.Debug(logName + " = " + status + " : " + (result == 1 ? "OK" : result));
        return result;
    }
    
    
    private byte[] CallMethod(uint methodId, byte[] args)
    {
        var acpiBuf = new byte[8 + args.Length];
        var outBuffer = new byte[20];

        BitConverter.GetBytes((uint)methodId).CopyTo(acpiBuf, 0);
        BitConverter.GetBytes((uint)args.Length).CopyTo(acpiBuf, 4);
        Array.Copy(args, 0, acpiBuf, 8, args.Length);
        
        CallDeviceIoControl(ControlCode, acpiBuf, outBuffer);

        return outBuffer;
    }

    private void CallDeviceIoControl(uint dwIoControlCode, byte[] lpInBuffer, byte[] lpOutBuffer)
    {
        if (!EnsureHandle())
        {
            Log.Error("Failed to get handle to ACPI device");
            return;
        }
        
        uint lpBytesReturned = 0;
        Native.DeviceIoControl(
            _handle,
            dwIoControlCode,
            lpInBuffer,
            (uint)lpInBuffer.Length,
            lpOutBuffer,
            (uint)lpOutBuffer.Length,
            ref lpBytesReturned,
            IntPtr.Zero
        );
    }
}