using System;

namespace LaptopToolBox.IPC.Subscribers;

public interface IIpcBackgroundSubscriber
{
    public void StartListening();
    public void StopListening();
    
    public void Subscribe<T>(Action<int, T> callback) where T : class, IIpcMessage;
}