using System;

namespace GHelper.Updates.Commands;

public interface IUpdatesCommandLoop
{
    public void Enqueue(IUpdatesCommand command);
    public void Enqueue(Action action);
}