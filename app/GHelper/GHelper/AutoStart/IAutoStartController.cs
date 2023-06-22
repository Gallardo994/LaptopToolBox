namespace GHelper.AutoStart;

public interface IAutoStartController
{
    public bool IsAutoStartEnabled();
    public void SetAutoStart(bool isAutoStartEnabled);
}