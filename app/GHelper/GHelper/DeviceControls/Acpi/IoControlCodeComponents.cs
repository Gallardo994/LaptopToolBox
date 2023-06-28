namespace GHelper.DeviceControls.Acpi;

internal struct IoControlCodeComponents
{
    public uint DeviceType { get; }
    public uint Function { get; }
    public IoControlCode.Method Method { get; }
    public IoControlCode.Access Access { get; }
    
    public IoControlCodeComponents(uint controlCode)
    {
        DeviceType = (controlCode >> 16) & 0xFFFF;
        Function = (controlCode >> 2) & 0xFFF;
        Method = (IoControlCode.Method)(controlCode & 0x3);
        Access = (IoControlCode.Access)((controlCode >> 14) & 0x3);
    }
}