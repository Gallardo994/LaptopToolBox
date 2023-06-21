namespace GHelper.DeviceControls.CPU.Vendors.AMD;

public enum RyzenAccessStatus : int
{
    BAD = 0x0,
    OK = 0x1,
    FAILED = 0xFF,
    UNKNOWN_CMD = 0xFE,
    CMD_REJECTED_PREREQ = 0xFD,
    CMD_REJECTED_BUSY = 0xFC
}