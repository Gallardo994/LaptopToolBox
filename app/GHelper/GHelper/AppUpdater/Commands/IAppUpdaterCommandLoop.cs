using System;

namespace GHelper.AppUpdater.Commands;

public interface IAppUpdaterCommandLoop
{
    public void Enqueue(IAppUpdaterCommand command);
    public void Enqueue(Action action);
}