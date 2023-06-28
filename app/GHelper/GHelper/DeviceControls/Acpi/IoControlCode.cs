using System.Runtime.InteropServices;

namespace GHelper.DeviceControls.Acpi;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
internal struct IoControlCode
{
    public uint Numeric => code;
    private uint code;

    public IoControlCode(uint deviceType, uint function, Access access) : this(deviceType, function, Method.Buffered, access)
    {
    }

    public IoControlCode(uint deviceType, uint function, Method method, Access access) 
    {
        code = (deviceType << 16) | ((uint)access << 14) | (function << 2) | (uint)method;
    }

    public enum Method : uint
    {
        Buffered = 0,
        InDirect = 1,
        OutDirect = 2,
        Neither = 3
    }

    public enum Access : uint
    {
        Any = 0,
        Read = 1,
        Write = 2
    }
}