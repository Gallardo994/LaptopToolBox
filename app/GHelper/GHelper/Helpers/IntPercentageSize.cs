namespace GHelper.Helpers;

public static class IntPercentageSize
{
    public static double ToPercentage(double value, int percentage)
    {
        return value * percentage / 100d;
    }
}