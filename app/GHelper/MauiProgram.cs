using System.Reflection;
using CommunityToolkit.Maui;
using GHelper.Platforms.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Ninject;
using Ninject.Syntax;
using Serilog;
using Xe.AcrylicView;

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
            .UseAcrylicView()
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
        
        builder.ConfigureLifecycleEvents(events =>
        {
#if WINDOWS10_0_17763_0_OR_GREATER
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    window.TryMicaOrAcrylic();
                });
            });
#endif
        });

        return builder.Build();
    }
}