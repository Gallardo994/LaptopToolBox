using LaptopToolBox.IPC;
using LaptopToolBox.IPC.Handlers;
using LaptopToolBox.IPC.Messages;
using LaptopToolBox.IPC.Subscribers;
using Ninject;

namespace LaptopToolBox.Initializers.ConcreteInitializers;

public class IpcSubscriberInitializer : IInitializer
{
    private readonly IIpcBackgroundSubscriber _ipcSubscriber;
    
    private readonly IpcBringToFrontHandler _ipcBringToFrontHandler;
    
    [Inject]
    public IpcSubscriberInitializer(IIpcBackgroundSubscriber ipcSubscriber,
        IpcBringToFrontHandler ipcBringToFrontHandler)
    {
        _ipcSubscriber = ipcSubscriber;
        
        _ipcBringToFrontHandler = ipcBringToFrontHandler;
    }
    
    public void Initialize()
    {
        _ipcSubscriber.Subscribe<IpcBringToFront>(_ipcBringToFrontHandler.Handle);
        
        _ipcSubscriber.StartListening();
    }
}