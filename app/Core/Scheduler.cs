using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Win32.TaskScheduler;

namespace GHelper.Core;

public class Scheduler : IScheduler
{
    private const string TaskName = "GHelper";

    public bool IsScheduled()
    {
        var taskService = new TaskService();
        return taskService.RootFolder.AllTasks.Any(t => t.Name == TaskName);
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
        var strExeFilePath = Application.ExecutablePath;

        var userId = WindowsIdentity.GetCurrent().Name;

        using var td = TaskService.Instance.NewTask();
        td.RegistrationInfo.Description = "G-Helper Auto Start";
        td.Triggers.Add(new LogonTrigger { UserId = userId });
        td.Actions.Add(strExeFilePath);

        if (ProcessHelper.IsUserAdministrator())
        {
            td.Principal.RunLevel = TaskRunLevel.Highest;
        }

        td.Settings.StopIfGoingOnBatteries = false;
        td.Settings.DisallowStartIfOnBatteries = false;
        td.Settings.ExecutionTimeLimit = TimeSpan.Zero;

        Debug.WriteLine(strExeFilePath);
        Debug.WriteLine(userId);

        try
        {
            TaskService.Instance.RootFolder.RegisterTaskDefinition(TaskName, td);
        }
        catch (Exception e)
        {
            if (ProcessHelper.IsUserAdministrator())
                MessageBox.Show("Can't create a start up task. Try running Task Scheduler by hand and manually deleting GHelper task if it exists there.", "Scheduler Error", MessageBoxButtons.OK);
            else
                ProcessHelper.RunAsAdmin();
        }
    }

    public void UnSchedule()
    {
        using var taskService = new TaskService();
        try
        {
            taskService.RootFolder.DeleteTask(TaskName);
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