using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace GHelper.DeviceControls.Fans;

[JsonObject(MemberSerialization.OptIn)]
public class FanCurve : ObservableObject
{
    [JsonProperty("point_count")] public int PointCount { get; init; }
    [JsonProperty("points")] public ObservableCollection<FanCurvePoint> Points { get; init; }

    public FanCurve()
    {
        
    }

    public FanCurve(int pointCount)
    {
        PointCount = pointCount;
        Points = new ObservableCollection<FanCurvePoint>();
        
        for (var i = 0; i < PointCount; i++)
        {
            Points.Add(new FanCurvePoint());
        }

        for (var i = 0; i < PointCount; i++)
        {
            Points[i].Temperature = (byte) (i * 100 / PointCount);
            Points[i].Value = 70;
        }
    }

    public FanCurve(FanCurve other)
    {
        PointCount = other.PointCount;
        Points = new ObservableCollection<FanCurvePoint>();
        
        foreach (var point in other.Points)
        {
            Points.Add(new FanCurvePoint(point));
        }
    }
    
    public struct Enumerator
    {
        private readonly FanCurve _fanCurve;
        private int _index;

        public Enumerator(FanCurve fanCurve)
        {
            _fanCurve = fanCurve;
            _index = -1;
        }

        public FanCurvePoint Current => _fanCurve.Points[_index];

        public bool MoveNext()
        {
            _index++;
            return _index < _fanCurve.PointCount;
        }
    }
    
    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }
    
    public FanCurvePoint this[int index]
    {
        get => Points[index];
        set => Points[index] = value;
    }
    
    public byte[] ToByteArray()
    {
        var byteArray = new byte[PointCount * 2];
        
        for (var i = 0; i < PointCount; i++)
        {
            byteArray[i] = Points[i].Temperature;
            byteArray[i + PointCount] = Points[i].Value;
        }

        return byteArray;
    }
    
    public bool HasModificationsComparedTo(FanCurve other)
    {
        if (PointCount != other.PointCount)
        {
            return true;
        }

        for (var i = 0; i < PointCount; i++)
        {
            if (Points[i].Temperature != other.Points[i].Temperature || Points[i].Value != other.Points[i].Value)
            {
                return true;
            }
        }

        return false;
    }
}