using System;
using System.Reflection;
using System.Windows;
using Ninject;
using Serilog;

namespace GHelper;

public partial class App
{
    private void App_StartUp(object sender, StartupEventArgs startupEventArgs)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .MinimumLevel.Debug()
            .CreateLogger();

        try
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
        
            ProgramM.MainA(kernel, startupEventArgs.Args);
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Fatal error");
            throw;
        }
    }
}