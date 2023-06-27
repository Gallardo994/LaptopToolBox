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
        
        /*
        // Below FanCurvePointCount*40%, fan speed is 20%
        for (var i = 0; i < (int) (PointCount * 0.4); i++)
        {
            Points[i].Value = 20;
        }
        
        // Otherwise, increase linearly to 100%
        for (var i = (int) (PointCount * 0.4); i < PointCount; i++)
        {
            Points[i].Value = (byte) (i * 100 / PointCount);
        }
        */
        
        // Set all to 99 for testing
        for (var i = 0; i < PointCount; i++)
        {
            Points[i].Value = 99;
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
        var byteArray = new byte[PointCount];
        
        for (var i = 0; i < PointCount; i++)
        {
            byteArray[i] = Points[i].Value;
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
            if (Points[i].Value != other.Points[i].Value)
            {
                return true;
            }
        }

        return false;
    }
}