using NAudio.CoreAudioApi;

namespace GHelper.DeviceControls.Microphone.Vendors.Asus;

public class AsusMicrophone : IMicrophoneProvider
{
    public void SetState(bool state)
    {
        using var enumerator = new MMDeviceEnumerator();
        
        var commDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
        commDevice.AudioEndpointVolume.Mute = !state;
    }
    
    public bool IsMicrophoneEnabled()
    {
        using var enumerator = new MMDeviceEnumerator();
        
        var commDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
        return !commDevice.AudioEndpointVolume.Mute;
    }
}