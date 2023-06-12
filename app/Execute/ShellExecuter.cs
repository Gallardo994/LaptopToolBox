using System.Diagnostics;

namespace GHelper.Execute;

public class ShellExecuter : IShellExecuter
{
    public void Execute(string command)
    {
        Process.Start(new ProcessStartInfo(command) { UseShellExecute = true });
    }
}