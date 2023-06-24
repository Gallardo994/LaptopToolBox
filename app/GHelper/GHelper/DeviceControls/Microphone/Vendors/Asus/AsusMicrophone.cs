using NAudio.CoreAudioApi;
using Serilog;

namespace GHelper.DeviceControls.Microphone.Vendors.Asus;

public class AsusMicrophone : IMicrophoneProvider
{
    public void SetState(bool state)
    {
        using var enumerator = new MMDeviceEnumerator();
        
        foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
        {
            device.AudioEndpointVolume.Mute = !state;
        }
        
        Log.Debug($"Microphone state set to {state}");
    }
    
    public bool IsMicrophoneEnabled()
    {
        using var enumerator = new MMDeviceEnumerator();
        
        foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
        {
            if (!device.AudioEndpointVolume.Mute)
            {
                return true;
            }
        }
        
        return false;
    }
}