using NAudio.CoreAudioApi;
using Serilog;

namespace LaptopToolBox.DeviceControls.Microphone.Vendors.Asus;

public class AsusMicrophone : IMicrophoneProvider
{
    public void SetState(bool state)
    {
        using var enumerator = new MMDeviceEnumerator();
        
        foreach (var device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
        {
            device.AudioEndpointVolume.Mute = !state;
            device.AudioEndpointVolume.MasterVolumeLevelScalar = state ? 1f : 0f;
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