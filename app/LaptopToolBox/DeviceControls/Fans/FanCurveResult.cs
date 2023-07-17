namespace LaptopToolBox.DeviceControls.Fans;

public enum FanCurveResult
{
    OK,
    WrongPointCount,
    PointsNotIncreasing,
    BeyondMaximum,
    AllPointsZero,
    BiosRejected,
}