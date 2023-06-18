using System;
using GHelper.Serialization;
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
    
    public bool IsAvailable => EnsureHandle();

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
        var serializer = new BinarySerializer();
        serializer.WriteUint(Devs);
        serializer.WriteUint(sizeof(uint) * 2);
        serializer.WriteUint(deviceId);
        serializer.WriteUint((uint) status);

        var callStatus = CallMethod(serializer);

        Log.Debug(logName + " = " + status + " : " + (callStatus == 1 ? "OK" : callStatus));
        return callStatus;
    }
    
    
    private int CallMethod(BinarySerializer serializer)
    {
        var outBuffer = new byte[20];
        CallDeviceIoControl(ControlCode, serializer.ToArray(), outBuffer);
        return BitConverter.ToInt32(outBuffer, 0);
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