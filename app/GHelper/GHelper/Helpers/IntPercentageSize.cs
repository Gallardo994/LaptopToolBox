namespace GHelper.Helpers;

public static class IntPercentageSize
{
    public static double ToIntPercentage(double value, int percentage)
    {
        return value * percentage / 100d;
    }
    
    public static double ToFloatPercentage(double value, float percentage)
    {
        return value * (int) percentage / 100d;
    }
    
    public static double ToFloatPercentage(double value, float percentage, float maxPercentage)
    {
        return value * (int) percentage / (int) maxPercentage;
    }
}