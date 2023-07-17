using System;
using LaptopToolBox.Commands;

namespace LaptopToolBox.Updates.Commands;

public class UpdatesCommandLoop : BackgroundCommandLoop<IUpdatesCommand>, IUpdatesCommandLoop
{
    public void Enqueue(Action action)
    {
        Enqueue(new UpdatesActionCommand(action));
    }
}