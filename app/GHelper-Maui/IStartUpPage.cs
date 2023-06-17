namespace GHelper;

public interface IStartUpPage
{
    public void Navigate<T>() where T : Page;
    public void Navigate(Type type);
}