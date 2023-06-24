namespace GHelper.Toasts;

public interface IToastController
{
    public void ShowToast(string iconGlyph, string title, string message);
}