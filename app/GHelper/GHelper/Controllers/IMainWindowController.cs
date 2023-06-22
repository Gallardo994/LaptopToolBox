namespace GHelper.Controllers;

public interface IMainWindowController
{
    public void SetState(bool state);
    public bool GetState();
    public void ToggleState();
}