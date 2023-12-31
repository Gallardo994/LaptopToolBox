﻿using System;
using System.Runtime.InteropServices;
using LaptopToolBox.Serialization;
using Serilog;
using Vanara.PInvoke;

namespace LaptopToolBox.DeviceControls.Acpi.Vendors.Asus;

public class AsusAcpi : IAcpi
{
    private readonly AsusAcpiHandleProvider _acpiHandleProvider;
    private readonly IoControlCode _acpiIoControlCode = new((uint) FileDeviceType.FILE_DEVICE_UNKNOWN, 0x903, IoControlCode.Method.Buffered, IoControlCode.Access.Any);

    public bool IsAvailable => _acpiHandleProvider.TryGet(out _);

    private const int DefaultBufferSize = 32;
    
    public AsusAcpi()
    {
        _acpiHandleProvider = new AsusAcpiHandleProvider();

        Initialize();
    }

    public bool TryDeviceSet(uint deviceId, uint status, out uint result)
    {
        var deserializer = new BinaryDeserializer(DeviceSetWithBuffer(deviceId, status, sizeof(uint)));

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
    
    public bool TryDeviceSet(uint deviceId, byte[] buffer, out uint result)
    {
        var deserializer = new BinaryDeserializer(DeviceSetWithBuffer(deviceId, buffer, sizeof(uint)));

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
    
    public bool TryDeviceGet(uint deviceId, out uint status)
    {
        var deserializer = new BinaryDeserializer(DeviceGetWithBuffer(deviceId, sizeof(uint)));

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
    
    public byte[] DeviceGetBuffer(uint deviceId, uint status)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DSTS);
        serializer.WriteSizeOf<uint>(count: 2);
        serializer.WriteUint(deviceId);
        serializer.WriteUint(status);

        return CallDeviceIoControl(serializer, DefaultBufferSize);
    }
    
    private void Initialize()
    {
        var serializer = new BinarySerializer();
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_INIT);
        
        CallDeviceIoControl(serializer, sizeof(uint));
    }
    
    private byte[] DeviceSetWithBuffer(uint deviceId, uint status, int bufferSize)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DEVS);
        serializer.WriteSizeOf<uint>(count: 2);
        serializer.WriteUint(deviceId);
        serializer.WriteUint(status);

        return CallDeviceIoControl(serializer, bufferSize);
    }

    private byte[] DeviceSetWithBuffer(uint deviceId, byte[] buffer, int bufferSize)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DEVS);
        serializer.WriteSizeOf<uint>(extraBytes: buffer.Length);
        serializer.WriteUint(deviceId);
        serializer.WriteBytes(buffer);

        return CallDeviceIoControl(serializer, bufferSize);
    }

    private byte[] DeviceGetWithBuffer(uint deviceId, int bufferSize)
    {
        var serializer = new BinarySerializer();
        
        serializer.WriteUint((uint) AsusWmi.ASUS_WMI_METHODID_DSTS);
        serializer.WriteSizeOf<uint>();
        serializer.WriteUint(deviceId);

        return CallDeviceIoControl(serializer, bufferSize);
    }

    private byte[] CallDeviceIoControl(BinarySerializer serializer, int bufferSize)
    {
        if (!_acpiHandleProvider.TryGet(out var handle))
        {
            Log.Error("Failed to get handle to ACPI device");
            return Array.Empty<byte>();
        }
        
        var inBuffer = Marshal.AllocHGlobal(serializer.Position);
        Marshal.Copy(serializer.Buffer, 0, inBuffer, serializer.Position);
        
        var outBuffer = Marshal.AllocHGlobal(bufferSize);
        
        Kernel32.DeviceIoControl(handle, 
            _acpiIoControlCode.Numeric, 
            inBuffer, 
            (uint)serializer.Position, 
            outBuffer,
            (uint) bufferSize,
            out var lpBytesReturned,
            IntPtr.Zero);
        
        var lpOutBuffer = new byte[lpBytesReturned];
        Marshal.Copy(outBuffer, lpOutBuffer, 0, (int) lpBytesReturned);
        
        Marshal.FreeHGlobal(inBuffer);
        Marshal.FreeHGlobal(outBuffer);
        
        return lpOutBuffer;
    }
}