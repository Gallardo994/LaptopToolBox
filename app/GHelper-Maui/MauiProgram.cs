using System.Reflection;
using CommunityToolkit.Maui;
using GHelper.Injection;
using GHelper.Platforms.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Ninject;
using Serilog;

namespace GHelper;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var appDataDirectory = new DirectoryInfo(appDataPath);
        var logDirectory = appDataDirectory.CreateSubdirectory("GHelper");
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(Path.Combine(logDirectory.FullName, "log.txt"), rollingInterval: RollingInterval.Day, buffered: false)
            .WriteTo.Console()
            .MinimumLevel.Debug()
            .CreateLogger();
        
        AppDomain.CurrentDomain.FirstChanceException += (sender, args) =>
        {
            Log.Error(args.Exception, "Unhandled exception");
        };
        
        var kernel = new StandardKernel();

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseNinject(kernel)
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("SagoeFluentIcons.ttf", "SagoeFluentIcons");
                fonts.AddFont("SagoeUI.ttf", "SagoeUI");
                fonts.AddFont("SagoeUI-VF.tff", "SagoeUIVF");
                
            })
            .Logging.AddSerilog();

        builder.Services.AddLogging(logging => logging.AddSerilog(dispose: true));

        Services.ResolutionRoot = kernel;
        
        builder.ConfigureLifecycleEvents(events =>
        {
#if WINDOWS10_0_17763_0_OR_GREATER
 
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    // *** For Mica or Acrylic support ** //
                    window.TryMicaOrAcrylic();
                });
            });
        });
#endif
        
        kernel.Load(Assembly.GetExecutingAssembly());

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
    }
}