using System;
using System.Collections.Generic;
using System.Timers;
using GHelper.Commands;
using GHelper.DeviceControls.HardwareMonitoring.Constructors;
using GHelper.DeviceControls.HardwareMonitoring.PostProcessors;
using GHelper.Helpers;
using LibreHardwareMonitor.Hardware;

namespace GHelper.DeviceControls.HardwareMonitoring;

public class HardwareMonitor : IHardwareMonitor
{
    private readonly IBackgroundCommandLoop _backgroundCommandLoop;
    private readonly ISTACommandLoop _staCommandLoop;
    
    private Computer _computer;
    private SafeTimer _timer;
    
    private readonly Dictionary<HardwareType, IConstructor> _constructors = new()
    {
        { HardwareType.Cpu, new CpuConstructor() },
        { HardwareType.GpuNvidia, new GpuConstructor() },
        // { HardwareType.GpuAmd, new GpuConstructor() },
        // { HardwareType.Motherboard, new MotherboardConstructor() },
        { HardwareType.Memory, new MemoryConstructor() },
        // { HardwareType.Storage, new StorageConstructor() },
    };
    
    private readonly IPostProcessor[] _postProcessors = 
    {
        new PowerConsumersPostProcessor(),
        new SensorsPostProcessor(),
    };

    public IHardwareReport HardwareReport { get; private set; } = new HardwareReport();
    public event Action<IHardwareReport> HardwareReportUpdated;
    
    public HardwareMonitor(IBackgroundCommandLoop backgroundCommandLoop, ISTACommandLoop staCommandLoop)
    {
        _backgroundCommandLoop = backgroundCommandLoop;
        _staCommandLoop = staCommandLoop;
        
        _backgroundCommandLoop.Enqueue(() =>
        {
            _computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMotherboardEnabled = true,
                IsMemoryEnabled = true,
                IsStorageEnabled = true,
            };
        });
    }
    
    public void StartMonitoring()
    {
        _backgroundCommandLoop.Enqueue(() =>
        {
            _computer.Open();
        });

        _timer = new SafeTimer(1000);
        _timer.SafeElapsed += RequestUpdateHardware;
        _timer.Start();
    }
    
    public void StopMonitoring()
    {
        _timer?.Dispose();
        _timer = null;
        
        _backgroundCommandLoop.Enqueue(() =>
        {
            _computer.Close();
        });
    }
    
    private void RequestUpdateHardware(object sender, ElapsedEventArgs e)
    {
        _backgroundCommandLoop.Enqueue(() =>
        {
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
                
                foreach (var subHardware in hardware.SubHardware)
                {
                    subHardware.Update();
                }
            }
            
            _staCommandLoop.Enqueue(() =>
            {
                HardwareReport.Clear();
                
                foreach (var hardware in _computer.Hardware)
                {
                    if (!_constructors.TryGetValue(hardware.HardwareType, out var constructor))
                    {
                        continue;
                    }
                    
                    constructor.FillReport(HardwareReport, hardware);
                }
                
                foreach (var postProcessor in _postProcessors)
                {
                    postProcessor.PostProcess(HardwareReport, _computer);
                }
                
                HardwareReportUpdated?.Invoke(HardwareReport);
            });
        });
    }
}