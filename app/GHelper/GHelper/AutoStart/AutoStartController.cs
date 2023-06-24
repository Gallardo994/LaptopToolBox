using GHelper.Helpers;
using Microsoft.Win32.TaskScheduler;

namespace GHelper.AutoStart
{
    public class AutoStartController : IAutoStartController
    {
        private const string TaskName = "GHelper AutoStart";

        private void EnableAutoStart()
        {
            if (IsAutoStartEnabled())
            {
                return;
            }

            using var taskService = new TaskService();
            
            var taskDefinition = taskService.NewTask();
            taskDefinition.RegistrationInfo.Description = "GHelper AutoStart";
            taskDefinition.Principal.RunLevel = TaskRunLevel.Highest;
            taskDefinition.Triggers.Add(new LogonTrigger());
            taskDefinition.Actions.Add(new ExecAction(ApplicationHelper.CurrentExecutableName));
            
            taskService.RootFolder.RegisterTaskDefinition(TaskName, taskDefinition);
        }

        private void DisableAutoStart()
        {
            if (!IsAutoStartEnabled())
            {
                return;
            }

            using var taskService = new TaskService();
            taskService.RootFolder.DeleteTask(TaskName);
        }

        public bool IsAutoStartEnabled()
        {
            using var taskService = new TaskService();
            return taskService.FindTask(TaskName) != null;
        }

        public void SetAutoStart(bool isAutoStartEnabled)
        {
            if (isAutoStartEnabled)
            {
                EnableAutoStart();
            }
            else
            {
                DisableAutoStart();
            }
        }
    }
}