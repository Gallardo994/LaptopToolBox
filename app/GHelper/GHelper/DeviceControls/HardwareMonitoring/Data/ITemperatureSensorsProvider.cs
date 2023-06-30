using System.Collections.ObjectModel;

namespace GHelper.DeviceControls.HardwareMonitoring.Data;

public interface ITemperatureSensorsProvider
{
    public ObservableCollection<ITemperatureSensor> Sensors { get; set; }
}