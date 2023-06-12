using GHelper.Commands;
using GHelper.Core.Commands;
using Microsoft.Win32.TaskScheduler;
using Ninject;

namespace GHelper.Core;

public class Scheduler : IScheduler
{
    private const string TaskName = "GHelper";
    
    private readonly ICommandLoop _commandLoop;
    
    [Inject]
    public Scheduler(ICommandLoop commandLoop)
    {
        _commandLoop = commandLoop;
    }

    public bool IsScheduled()
    {
        var taskService = new TaskService();
        
        foreach (var task in taskService.RootFolder.Tasks)
        {
            if (task.Name == TaskName)
            {
                return true;
            }
        }
        
        return false;
    }

    public void ReScheduleAdmin()
    {
        if (!ProcessHelper.IsUserAdministrator() || !IsScheduled())
        {
            return;
        }
        
        UnSchedule();
        Schedule();
    }
    
    public void Schedule()
    {
        _commandLoop.Add(new ScheduleCommand(TaskName));
    }

    public void UnSchedule()
    {
        _commandLoop.Add(new UnScheduleCommand(TaskName));
    }
}