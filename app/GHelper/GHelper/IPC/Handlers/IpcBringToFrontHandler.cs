using GHelper.AppWindows;
using GHelper.Helpers;
using GHelper.IPC.Messages;
using Ninject;
using Serilog;

namespace GHelper.IPC.Handlers;

public class IpcBringToFrontHandler
{
    private readonly MainWindow _mainWindow;
    
    [Inject]
    public IpcBringToFrontHandler(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }
    
    public void Handle(int processId, IpcBringToFront message)
    {
        Log.Information("Received {Message} from {ProcessId}", message, processId);

        _mainWindow.BringToFront();
    }
}