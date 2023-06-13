using System.Drawing;
using System.Windows.Forms;
using GHelper.DrawingHelpers;
using Ninject;

namespace GHelper.Toasts;

public class ToastNativeWindow : NativeWindow, IToastNativeWindow
{
    private readonly IDrawingHelper _drawingHelper;
    
    private string _text;
    private Bitmap? _icon;

    [Inject]
    public ToastNativeWindow(IDrawingHelper drawingHelper)
    {
        _drawingHelper = drawingHelper;
        
        _text = string.Empty;
        _icon = null;
    }
    
    public void SetText(string text)
    {
        _text = text;
    }
    
    public void SetIcon(Bitmap icon)
    {
        _icon = icon;
    }
    
    private void PerformPaint(PaintEventArgs e)
    {
        /*
        Brush brush = new SolidBrush(Color.FromArgb(150, Color.Black));
        _drawingHelper.FillRoundedRectangle(e.Graphics, brush, this.Bound, 10);

        var format = new StringFormat();
        format.LineAlignment = StringAlignment.Center;
        format.Alignment = StringAlignment.Center;

        var shiftX = 0;

        if (_icon is not null)
        {
            e.Graphics.DrawImage(_icon, 18, 18, 64, 64);
            shiftX = 40;
        }

        e.Graphics.DrawString(_text,
            new Font("Segoe UI", 36f, FontStyle.Bold, GraphicsUnit.Pixel),
            new SolidBrush(Color.White),
            new PointF(this.Bound.Width / 2 + shiftX, this.Bound.Height / 2),
            format);
            */
    }
}