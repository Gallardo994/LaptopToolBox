using System;
using GHelper.AppWindows;
using Microsoft.UI.Dispatching;
using Ninject;

namespace GHelper.Commands;

public class STACommandLoop : ISTACommandLoop
{
    private readonly DispatcherQueue _dispatcherQueue;
    
    [Inject]
    public STACommandLoop(MainWindow mainWindow)
    {
        _dispatcherQueue = mainWindow.DispatcherQueue;
    }
    
    public void Enqueue(ISTACommand command)
    {
        if (_dispatcherQueue.HasThreadAccess)
        {
            command.Execute();
            return;
        }
        
        _dispatcherQueue.TryEnqueue(command.Execute);
    }
    
    public void Enqueue(Action action)
    {
        Enqueue(new ActionCommand(action));
    }
}