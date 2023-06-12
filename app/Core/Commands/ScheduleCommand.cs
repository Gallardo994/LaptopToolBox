using System.Security.Principal;
using GHelper.Commands;
using Microsoft.Win32.TaskScheduler;

namespace GHelper.Core.Commands;

public class ScheduleCommand : ICommand
{
    private readonly string _taskName;
    
    public ScheduleCommand(string taskName)
    {
        _taskName = taskName;
    }
    
    public void Execute()
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

        try
        {
            TaskService.Instance.RootFolder.RegisterTaskDefinition(_taskName, td);
        }
        catch (Exception e)
        {
            if (ProcessHelper.IsUserAdministrator())
            {
                MessageBox.Show("Can't create a start up task. Try running Task Scheduler by hand and manually deleting GHelper task if it exists there.", "Scheduler Error", MessageBoxButtons.OK);
            }
            else
            {
                ProcessHelper.RunAsAdmin();
            }
        }
    }
}