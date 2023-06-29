using System;
using System.Collections.Generic;
using System.Timers;
using GHelper.Commands;
using GHelper.DeviceControls.HardwareMonitoring.Constructors;
using OpenHardwareMonitor.Hardware;

namespace GHelper.DeviceControls.HardwareMonitoring;

public class HardwareMonitor : IHardwareMonitor
{
    private readonly IBackgroundCommandLoop _backgroundCommandLoop;
    private readonly ISTACommandLoop _staCommandLoop;
    
    private readonly Computer _computer;
    private Timer _timer;
    
    private readonly Dictionary<HardwareType, IConstructor> _constructors = new()
    {
        { HardwareType.CPU, new CpuConstructor() },
        { HardwareType.GpuNvidia, new GpuConstructor() },
        { HardwareType.GpuAti, new GpuConstructor() },
        { HardwareType.Mainboard, new MainboardConstructor() },
        { HardwareType.RAM, new RamConstructor() },
        { HardwareType.HDD, new HddConstructor() },
    };

    public IHardwareReport HardwareReport { get; private set; } = new HardwareReport();
    public event Action<IHardwareReport> HardwareReportUpdated;
    
    public HardwareMonitor(IBackgroundCommandLoop backgroundCommandLoop, ISTACommandLoop staCommandLoop)
    {
        _backgroundCommandLoop = backgroundCommandLoop;
        _staCommandLoop = staCommandLoop;
        
        _computer = new Computer
        {
            CPUEnabled = true,
            GPUEnabled = true,
            MainboardEnabled = true,
            RAMEnabled = true,
            HDDEnabled = true,
            FanControllerEnabled = true,
        };
    }
    
    public void StartMonitoring()
    {
        _computer.Open();
        
        _timer = new Timer(1000);
        _timer.Elapsed += RequestUpdateHardware;
        _timer.Start();
    }
    
    public void StopMonitoring()
    {
        _timer?.Dispose();
        _timer = null;
        
        _computer.Close();
    }
    
    private void RequestUpdateHardware(object sender, ElapsedEventArgs e)
    {
        var computer = _computer;
        
        _backgroundCommandLoop.Enqueue(() =>
        {
            foreach (var hardware in computer.Hardware)
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
                
                foreach (var hardware in computer.Hardware)
                {
                    if (!_constructors.TryGetValue(hardware.HardwareType, out var constructor))
                    {
                        continue;
                    }
                    
                    constructor.FillReport(HardwareReport, hardware);
                }
                
                HardwareReportUpdated?.Invoke(HardwareReport);
            });
        });
    }
}