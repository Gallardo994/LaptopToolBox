using System;
using GHelper.Commands;

namespace GHelper.Updates.Commands;

public class UpdatesCommandLoop : BackgroundCommandLoop<IUpdatesCommand>, IUpdatesCommandLoop
{
    public void Enqueue(Action action)
    {
        Enqueue(new UpdatesActionCommand(action));
    }
}