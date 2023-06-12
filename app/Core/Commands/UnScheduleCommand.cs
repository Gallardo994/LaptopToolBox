using GHelper.Commands;
using Microsoft.Win32.TaskScheduler;

namespace GHelper.Core.Commands;

public class UnScheduleCommand : ICommand
{
    private readonly string _taskName;
    
    public UnScheduleCommand(string taskName)
    {
        _taskName = taskName;
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
            if (ProcessHelper.IsUserAdministrator())
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