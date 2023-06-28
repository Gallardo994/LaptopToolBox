using System;
using System.Runtime.InteropServices;
using GHelper.Serialization;
using Serilog;
using Vanara.PInvoke;

namespace GHelper.DeviceControls.Acpi.Vendors.Asus;

public class AsusAcpi : IAcpi
{
    private readonly AsusAcpiHandleProvider _acpiHandleProvider;

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
        
        CallMethod(serializer);
    }

    public uint DeviceSet(uint deviceId, uint status)
    {
        var deserializer = new BinaryDeserializer(DeviceSetWithBuffer(deviceId, status));
        return deserializer.ReadUint();
    }
    
    public uint DeviceSet(uint deviceId, byte[] buffer)
    {
        var deserializer = new BinaryDeserializer(DeviceSetWithBuffer(deviceId, buffer));
        return deserializer.ReadUint();
    }
    
    public byte[] DeviceSetWithBuffer(uint deviceId, uint status)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DEVS);
        serializer.WriteUint(sizeof(uint) * 2);
        serializer.WriteUint(deviceId);
        serializer.WriteUint(status);

        return CallMethod(serializer);
    }
    
    public byte[] DeviceSetWithBuffer(uint deviceId, byte[] buffer)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DEVS);
        serializer.WriteUint((uint) (sizeof(uint) + buffer.Length));
        serializer.WriteUint(deviceId);
        
        foreach (var b in buffer)
        {
            serializer.WriteByte(b);
        }

        return CallMethod(serializer);
    }
    
    public uint DeviceGet(uint deviceId)
    {
        var deserializer = new BinaryDeserializer(DeviceGetWithBuffer(deviceId));
        return deserializer.ReadUint();
    }
    
    public byte[] DeviceGetWithBuffer(uint deviceId)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DSTS);
        serializer.WriteUint(sizeof(uint));
        serializer.WriteUint(deviceId);

        return CallMethod(serializer);
    }
    
    public byte[] DeviceGetWithBuffer(uint deviceId, uint status)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DSTS);
        serializer.WriteUint(sizeof(uint) * 2);
        serializer.WriteUint(deviceId);
        serializer.WriteUint(status);

        return CallMethod(serializer);
    }
    
    private byte[] CallMethod(BinarySerializer serializer)
    {
        var outBuffer = new byte[32];
        CallDeviceIoControl((uint) AsusWmi.ASUS_WMI_CTRL_CODE, serializer.ToArray(), outBuffer);
        return outBuffer;
    }

    private void CallDeviceIoControl(uint dwIoControlCode, byte[] lpInBuffer, byte[] lpOutBuffer)
    {
        if (!_acpiHandleProvider.TryGet(out var handle))
        {
            Log.Error("Failed to get handle to ACPI device");
            return;
        }
        
        var inBuffer = Marshal.AllocHGlobal(lpInBuffer.Length);
        Marshal.Copy(lpInBuffer, 0, inBuffer, lpInBuffer.Length);
        
        var outBuffer = Marshal.AllocHGlobal(lpOutBuffer.Length);
        Marshal.Copy(lpOutBuffer, 0, outBuffer, lpOutBuffer.Length);
        
        Kernel32.DeviceIoControl(handle, 
            dwIoControlCode, 
            inBuffer, 
            (uint)lpInBuffer.Length, 
            outBuffer, 
            (uint)lpOutBuffer.Length, 
            out var lpBytesReturned, 
            IntPtr.Zero);
        
        Marshal.Copy(outBuffer, lpOutBuffer, 0, lpOutBuffer.Length);
        
        Marshal.FreeHGlobal(inBuffer);
        Marshal.FreeHGlobal(outBuffer);
    }
}