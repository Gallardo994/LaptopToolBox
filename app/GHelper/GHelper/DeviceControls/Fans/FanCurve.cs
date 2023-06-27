using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace GHelper.DeviceControls.Fans;

[JsonObject(MemberSerialization.OptIn)]
public class FanCurve : ObservableObject, IEnumerable<FanCurvePoint>
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
    
    public FanCurve(byte[] byteArray)
    {
        PointCount = byteArray.Length / 2;
        Points = new ObservableCollection<FanCurvePoint>();
        
        for (var i = 0; i < PointCount; i++)
        {
            Points.Add(new FanCurvePoint());
        }

        for (var i = 0; i < PointCount; i++)
        {
            Points[i].Temperature = byteArray[i];
            Points[i].Value = byteArray[i + PointCount];
        }
    }
    
    public struct Enumerator : IEnumerator<FanCurvePoint>
    {
        private readonly FanCurve _fanCurve;
        private int _index;

        public Enumerator(FanCurve fanCurve)
        {
            _fanCurve = fanCurve;
            _index = -1;
        }

        public void Reset()
        {
            _index = -1;
        }

        object IEnumerator.Current => Current;

        public FanCurvePoint Current => _fanCurve.Points[_index];

        public bool MoveNext()
        {
            _index++;
            return _index < _fanCurve.PointCount;
        }

        public void Dispose()
        {
            Reset();
        }
    }
    
    public IEnumerator<FanCurvePoint> GetEnumerator()
    {
        return new Enumerator(this);
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
            if (Points[i].HasModificationsComparedTo(other.Points[i]))
            {
                return true;
            }
        }

        return false;
    }
}