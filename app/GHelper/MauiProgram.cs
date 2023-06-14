using System.Reflection;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Ninject;
using Ninject.Syntax;
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
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Logging.AddSerilog();
        
        var kernel = new StandardKernel();
        kernel.Load(Assembly.GetExecutingAssembly());
        builder.Services.AddSingleton<IResolutionRoot>(kernel);

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
    }
}