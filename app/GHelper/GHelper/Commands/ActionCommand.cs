using System;

namespace GHelper.Commands;

public class ActionCommand : ISTACommand, IBackgroundCommand
{
    private readonly Action _action;
    
    public ActionCommand(Action action)
    {
        _action = action;
    }
    
    void ISTACommand.Execute()
    {
        _action();
    }

    void IBackgroundCommand.Execute()
    {
        _action();
    }
}