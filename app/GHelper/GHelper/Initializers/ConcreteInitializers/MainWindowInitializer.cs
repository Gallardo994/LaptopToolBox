using GHelper.AppWindows;
using GHelper.Configs;
using GHelper.DeviceControls.GPUs;
using GHelper.Helpers;
using Ninject;
using Serilog;

namespace GHelper.Initializers.ConcreteInitializers;

public class MainWindowInitializer : IInitializer
{
    private readonly IConfig _config;
    private readonly MainWindow _mainWindow;
    private readonly IGpuGeneralInfoProvider _gpuGeneralInfoProvider;
    
    [Inject]
    public MainWindowInitializer(IConfig config, MainWindow mainWindow, IGpuGeneralInfoProvider gpuGeneralInfoProvider)
    {
        _config = config;
        _mainWindow = mainWindow;
        _gpuGeneralInfoProvider = gpuGeneralInfoProvider;
    }
    
    public void Initialize()
    {
        foreach (var gpuInfo in _gpuGeneralInfoProvider.GpuGeneralInfoCollection)
        {
            Log.Debug("GPU: {gpuInfo}", gpuInfo);
        }
        
        if (!_config.StartMinimized)
        {
            _mainWindow.Activate();
        }
        
        _mainWindow.Closed += (sender, windowArgs) =>
        {
            windowArgs.Handled = true;
            _mainWindow.Hide();
        };
    }
}