using System.Security.Principal;
using GHelper.Commands;
using GHelper.ProcessHelpers;
using Microsoft.Win32.TaskScheduler;

namespace GHelper.Core.Commands;

public class ScheduleCommand : ICommand
{
    private readonly string _taskName;
    private readonly IAdministratorHelper _administratorHelper;
    
    public ScheduleCommand(string taskName, IAdministratorHelper administratorHelper)
    {
        _taskName = taskName;
        _administratorHelper = administratorHelper;
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
            if (_administratorHelper.IsUserAdministrator())
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