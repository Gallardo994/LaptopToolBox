using LaptopToolBox.Helpers;
using LaptopToolBox.AppWindows;
using LaptopToolBox.IPC.Messages;
using Ninject;
using Serilog;

namespace LaptopToolBox.IPC.Handlers;

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