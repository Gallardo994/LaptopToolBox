using System;
using GHelper.Serialization;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.Acpi.Vendors.Asus;

public class AsusAcpi : IAcpi
{
    private readonly AsusAcpiHandleProvider _acpiHandleProvider;
    
    const uint Devs = 0x53564544;

    public bool IsAvailable => _acpiHandleProvider.TryGet(out _);
    
    public AsusAcpi()
    {
        _acpiHandleProvider = new AsusAcpiHandleProvider();
    }

    public int DeviceSet(uint deviceId, int status)
    {
        var serializer = new BinarySerializer();
        serializer.WriteUint(Devs);
        serializer.WriteUint(sizeof(uint) * 2);
        serializer.WriteUint(deviceId);
        serializer.WriteUint((uint) status);

        return CallMethod(serializer);
    }
    
    
    private int CallMethod(BinarySerializer serializer)
    {
        var outBuffer = new byte[20];
        CallDeviceIoControl(0x0022240C, serializer.ToArray(), outBuffer);
        return BitConverter.ToInt32(outBuffer, 0);
    }

    private void CallDeviceIoControl(uint dwIoControlCode, byte[] lpInBuffer, byte[] lpOutBuffer)
    {
        if (!_acpiHandleProvider.TryGet(out var handle))
        {
            Log.Error("Failed to get handle to ACPI device");
            return;
        }
        
        uint lpBytesReturned = 0;
        Native.DeviceIoControl(
            handle,
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