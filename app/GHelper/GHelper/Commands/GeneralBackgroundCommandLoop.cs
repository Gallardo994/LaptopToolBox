using System;

namespace GHelper.Commands;

public class GeneralBackgroundCommandLoop : BackgroundCommandLoop<IBackgroundCommand>, IBackgroundCommandLoop
{
    public void Enqueue(Action action)
    {
        Enqueue(new ActionCommand(action));
    }
}