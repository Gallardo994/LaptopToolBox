using GHelper.AppWindows;
using GHelper.Configs;
using GHelper.DeviceControls.CPU;
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
    private readonly ICpuGeneralInfoProvider _cpuGeneralInfoProvider;
    
    [Inject]
    public MainWindowInitializer(IConfig config, MainWindow mainWindow, IGpuGeneralInfoProvider gpuGeneralInfoProvider, ICpuGeneralInfoProvider cpuGeneralInfoProvider)
    {
        _config = config;
        _mainWindow = mainWindow;
        _gpuGeneralInfoProvider = gpuGeneralInfoProvider;
        _cpuGeneralInfoProvider = cpuGeneralInfoProvider;
    }
    
    public void Initialize()
    {
        Log.Debug("CPU: {cpuInfo}", _cpuGeneralInfoProvider.Cpu);
        Log.Debug("Best GPU: {gpuInfo}", _gpuGeneralInfoProvider.BestGpu);
        
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