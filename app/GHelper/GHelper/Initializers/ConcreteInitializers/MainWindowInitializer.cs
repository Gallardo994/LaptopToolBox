using System.Drawing;
using GHelper.AppWindows;
using GHelper.Configs;
using H.NotifyIcon;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

public class MainWindowInitializer : IInitializer
{
    private readonly IConfig _config;
    private readonly MainWindow _mainWindow;
    
    [Inject]
    public MainWindowInitializer(IConfig config, MainWindow mainWindow)
    {
        _config = config;
        _mainWindow = mainWindow;
    }
    
    public void Initialize()
    {
        _mainWindow.Activate();
        
        _mainWindow.Closed += (sender, windowArgs) =>
        {
            windowArgs.Handled = true;
            _mainWindow.Hide();
        };

        if (_config.StartMinimized)
        {
            _mainWindow.Hide();
        }
    }
}