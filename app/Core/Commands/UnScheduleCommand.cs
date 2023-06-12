using GHelper.Commands;
using GHelper.ProcessHelpers;
using Microsoft.Win32.TaskScheduler;

namespace GHelper.Core.Commands;

public class UnScheduleCommand : ICommand
{
    private readonly string _taskName;
    private readonly IAdministratorHelper _administratorHelper;
    
    public UnScheduleCommand(string taskName, IAdministratorHelper administratorHelper)
    {
        _taskName = taskName;
        _administratorHelper = administratorHelper;
    }

    public void Execute()
    {
        using var taskService = new TaskService();
        try
        {
            taskService.RootFolder.DeleteTask(_taskName);
        }
        catch (Exception e)
        {
            if (_administratorHelper.IsUserAdministrator())
            {
                MessageBox.Show("Can't remove task. Try running Task Scheduler by hand and manually deleting GHelper task if it exists there.", "Scheduler Error", MessageBoxButtons.OK);
            }
            else
            {
                ProcessHelper.RunAsAdmin();
            }
        }
    }
}