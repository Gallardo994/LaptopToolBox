namespace GHelper.DeviceControls.TouchPad;

public static class PnpTouchPadHandleExtensions
{
    public static bool IsNullOrEmpty(this PnpTouchPadHandle handle)
    {
        return handle == null || handle.DeviceId == null;
    }
}