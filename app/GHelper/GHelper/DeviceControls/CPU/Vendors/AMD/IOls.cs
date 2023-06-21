using System;

namespace GHelper.DeviceControls.CPU.Vendors.AMD;

public interface IOls : IDisposable
{
    public Ols._InitializeOls InitializeOls { get; }
    public Ols._DeinitializeOls DeinitializeOls { get; }

    public Ols._GetDllStatus GetDllStatus { get; }
    public Ols._GetDriverType GetDriverType { get; }
    public Ols._GetDllVersion GetDllVersion { get; }
    public Ols._GetDriverVersion GetDriverVersion { get; }
    public Ols._Cpuid Cpuid { get; }
    public Ols._WritePciConfigDwordEx WritePciConfigDwordEx { get; }
    
    public Ols._ReadPciConfigDwordEx ReadPciConfigDwordEx { get; }
    public Ols._ReadPciConfigDwordEx64 ReadPciConfigDwordEx64 { get; }
    public Ols._WritePciConfigDwordEx64 WritePciConfigDwordEx64 { get; }
    public Ols._WritePciConfigDword WritePciConfigDword { get; }
    public Ols._ReadPciConfigDword ReadPciConfigDword { get; }

    public uint GetStatus();
}