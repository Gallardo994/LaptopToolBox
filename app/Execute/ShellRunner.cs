using System.Diagnostics;

namespace GHelper.Execute;

public class ShellRunner : IShellRunner
{
    public void Execute(string command)
    {
        Process.Start(new ProcessStartInfo(command) { UseShellExecute = true });
    }
}