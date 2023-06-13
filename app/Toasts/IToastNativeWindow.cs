using System.Drawing;
using System.Windows.Forms;

namespace GHelper.Toasts;

public interface IToastNativeWindow
{
    public void CreateHandle(CreateParams createParams);
    public void DestroyHandle();
    
    public void SetText(string text);
    public void SetIcon(Bitmap iconBitmap);
}