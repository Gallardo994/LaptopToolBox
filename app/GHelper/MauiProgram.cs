using System.Reflection;
using CommunityToolkit.Maui;
using GHelper.Injection;
using Microsoft.Extensions.Logging;
using Ninject;
using Serilog;

namespace GHelper;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .MinimumLevel.Debug()
            .CreateLogger();
        
        var kernel = new StandardKernel();
        kernel.Load(Assembly.GetExecutingAssembly());
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseNinject(kernel)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Sagoe-Fluent-Icons.ttf", "SagoeFluentIcons");
                fonts.AddFont("Sagoe-UI.ttf", "SagoeUI");
                
            })
            .Logging.AddSerilog();

        Services.ResolutionRoot = kernel;

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
    }
}