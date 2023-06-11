using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace GHelper
{
    public static class ProcessHelper
    {
        private static long lastAdmin;

        public static void CheckAlreadyRunning()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);

            if (processes.Length > 1)
            {
                foreach (Process process in processes)
                    if (process.Id != currentProcess.Id)
                        try
                        {
                            process.Kill();
                        }
                        catch (Exception ex)
                        {
                            Log.Debug(ex.ToString());
                            MessageBox.Show(Properties.Strings.AppAlreadyRunningText, Properties.Strings.AppAlreadyRunning, MessageBoxButtons.OK);
                            Application.Exit();
                            return;
                        }
            }
        }

        public static bool IsUserAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RunAsAdmin(string? param = null)
        {

            if (Math.Abs(DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastAdmin) < 2000) return;
            lastAdmin = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // Check if the current user is an administrator
            if (!IsUserAdministrator())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Application.ExecutablePath;
                startInfo.Arguments = param;
                startInfo.Verb = "runas";
                try
                {
                    Process.Start(startInfo);
                    Application.Exit();
                } catch (Exception ex)
                {
                    Log.Debug(ex.Message);
                }
            }
        }


        public static void KillByName(string name)
        {
            foreach (var process in Process.GetProcessesByName(name))
            {
                try
                {
                    process.Kill();
                    Log.Debug($"Stopped: {process.ProcessName}");
                }
                catch (Exception ex)
                {
                    Log.Debug($"Failed to stop: {process.ProcessName} {ex.Message}");
                }
            }
        }

        public static void KillByProcess(Process process)
        {
            try
            {
                process.Kill();
                Log.Debug($"Stopped: {process.ProcessName}");
            }
            catch (Exception ex)
            {
                Log.Debug($"Failed to stop: {process.ProcessName} {ex.Message}");
            }
        }

        public static void StopDisableService(string serviceName)
        {
            try
            {
                string script = $"Set-Service -Name \"{serviceName}\" -Status stopped -StartupType disabled";
                Log.Debug(script);
                RunCMD("powershell", script);
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
            }
        }

        public static void StartEnableService(string serviceName)
        {
            try
            {
                string script = $"Set-Service -Name \"{serviceName}\" -Status running -StartupType Automatic";
                Log.Debug(script);
                RunCMD("powershell", script);
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
            }
        }

        public static void RunCMD(string name, string args)
        {
            var cmd = new Process();
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.FileName = name;
            cmd.StartInfo.Arguments = args;
            cmd.Start();
            Log.Debug(cmd.StandardOutput.ReadToEnd());
            cmd.WaitForExit();
        }


    }
}
