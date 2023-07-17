using System;

namespace LaptopToolBox.DeviceControls.CPU.Vendors.AMD;

public interface IOls : IDisposable
{
    public int InitializeOls();
    public void DeinitializeOls();

    public uint GetDllStatus();
    public int Cpuid(uint index, ref uint eax, ref uint ebx, ref uint ecx, ref uint edx);
    
    public int WritePciConfigDwordEx(uint pciAddress, uint regAddress, byte value);
    public int WritePciConfigDwordEx(uint pciAddress, uint regAddress, uint value);
    public int ReadPciConfigDwordEx(uint pciAddress, uint regAddress, ref uint value);
    
    public int WritePciConfigDwordEx64(uint pciAddress, uint regAddress, uint value);
    public int WritePciConfigDwordEx64(uint pciAddress, uint regAddress, ulong value);
    public int ReadPciConfigDwordEx64(uint pciAddress, uint regAddress, ref ulong value);

    public uint GetDriverType();
    public uint GetDllVersion(ref byte major, ref byte minor, ref byte revision, ref byte release);
    public uint GetDriverVersion(ref byte major, ref byte minor, ref byte revision, ref byte release);


    public void WritePciConfigDword(uint pciAddress, byte regAddress, uint value);
    public uint ReadPciConfigDword(uint pciAddress, byte regAddress);

    public uint GetStatus();
}