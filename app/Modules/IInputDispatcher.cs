namespace GHelper.Modules;

public interface IInputDispatcher
{
    public void RegisterKeys();
    public void InitBacklightTimer();
    public void Init();
}