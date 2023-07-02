using System;
using System.Runtime.InteropServices;
using GHelper.Serialization;
using Serilog;
using Vanara.PInvoke;

namespace GHelper.DeviceControls.Acpi.Vendors.Asus;

public class AsusAcpi : IAcpi
{
    private readonly AsusAcpiHandleProvider _acpiHandleProvider;
    private readonly IoControlCode _acpiIoControlCode = new((uint) FileDeviceType.FILE_DEVICE_UNKNOWN, 0x903, IoControlCode.Method.Buffered, IoControlCode.Access.Any);

    public bool IsAvailable => _acpiHandleProvider.TryGet(out _);
    
    public AsusAcpi()
    {
        _acpiHandleProvider = new AsusAcpiHandleProvider();

        Initialize();
    }

    public void Initialize()
    {
        var serializer = new BinarySerializer();
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_INIT);
        
        CallDeviceIoControl(serializer);
    }
    
    public uint DeviceSet(uint deviceId, uint status) => new BinaryDeserializer(DeviceSetWithBuffer(deviceId, status)).ReadUint();
    public bool TryDeviceSet(uint deviceId, uint status, out uint result)
    {
        var deserializer = new BinaryDeserializer(DeviceSetWithBuffer(deviceId, status));

        try
        {
            result = deserializer.ReadUint();
            return true;
        }
        catch (InvalidOperationException)
        {
            result = 0;
            return false;
        }
    }
    public uint DeviceSet(uint deviceId, byte[] buffer) => new BinaryDeserializer(DeviceSetWithBuffer(deviceId, buffer)).ReadUint();
    
    public byte[] DeviceSetWithBuffer(uint deviceId, uint status)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DEVS);
        serializer.WriteSizeOf<uint>(count: 2);
        serializer.WriteUint(deviceId);
        serializer.WriteUint(status);

        return CallDeviceIoControl(serializer);
    }
    
    public byte[] DeviceSetWithBuffer(uint deviceId, byte[] buffer)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DEVS);
        serializer.WriteSizeOf<uint>(extraBytes: buffer.Length);
        serializer.WriteUint(deviceId);
        serializer.WriteBytes(buffer);

        return CallDeviceIoControl(serializer);
    }
    
    public uint DeviceGet(uint deviceId) => new BinaryDeserializer(DeviceGetWithBuffer(deviceId)).ReadUint();
    public bool TryDeviceGet(uint deviceId, out uint status)
    {
        var deserializer = new BinaryDeserializer(DeviceGetWithBuffer(deviceId));

        try
        {
            status = deserializer.ReadUint();
            return true;
        }
        catch (InvalidOperationException)
        {
            status = 0;
            return false;
        }
    }
    
    public byte[] DeviceGetWithBuffer(uint deviceId)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DSTS);
        serializer.WriteSizeOf<uint>();
        serializer.WriteUint(deviceId);

        return CallDeviceIoControl(serializer);
    }
    
    public byte[] DeviceGetWithBuffer(uint deviceId, uint status)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DSTS);
        serializer.WriteSizeOf<uint>(count: 2);
        serializer.WriteUint(deviceId);
        serializer.WriteUint(status);

        return CallDeviceIoControl(serializer);
    }

    private byte[] CallDeviceIoControl(BinarySerializer serializer)
    {
        if (!_acpiHandleProvider.TryGet(out var handle))
        {
            Log.Error("Failed to get handle to ACPI device");
            return Array.Empty<byte>();
        }
        
        var inBuffer = Marshal.AllocHGlobal(serializer.Position);
        Marshal.Copy(serializer.Buffer, 0, inBuffer, serializer.Position);
        
        const int outLimit = 64;
        var outBuffer = Marshal.AllocHGlobal(outLimit);
        
        Kernel32.DeviceIoControl(handle, 
            _acpiIoControlCode.Numeric, 
            inBuffer, 
            (uint)serializer.Position, 
            outBuffer,
            outLimit,
            out var lpBytesReturned,
            IntPtr.Zero);
        
        var lpOutBuffer = new byte[lpBytesReturned];
        Marshal.Copy(outBuffer, lpOutBuffer, 0, (int) lpBytesReturned);
        
        Marshal.FreeHGlobal(inBuffer);
        Marshal.FreeHGlobal(outBuffer);
        
        return lpOutBuffer;
    }
}