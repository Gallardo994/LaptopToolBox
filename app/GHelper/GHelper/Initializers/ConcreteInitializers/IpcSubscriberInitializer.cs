using GHelper.IPC;
using GHelper.IPC.Handlers;
using GHelper.IPC.Messages;
using GHelper.IPC.Subscribe;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

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