using GHelper.Commands;
using GHelper.Core.Commands;
using GHelper.ProcessHelpers;
using Microsoft.Win32.TaskScheduler;
using Ninject;

namespace GHelper.Core;

public class Scheduler : IScheduler
{
    private const string TaskName = "GHelper";
    
    private readonly ICommandLoop _commandLoop;
    private readonly IAdministratorHelper _administratorHelper;
    
    [Inject]
    public Scheduler(ICommandLoop commandLoop, IAdministratorHelper administratorHelper)
    {
        _commandLoop = commandLoop;
        _administratorHelper = administratorHelper;
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
        _commandLoop.Add(new ScheduleCommand(TaskName, _administratorHelper));
    }

    public void UnSchedule()
    {
        _commandLoop.Add(new UnScheduleCommand(TaskName, _administratorHelper));
    }
}