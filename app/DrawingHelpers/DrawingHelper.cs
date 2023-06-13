using System.Drawing;
using System.Drawing.Drawing2D;

namespace GHelper.DrawingHelpers;

public class DrawingHelper : IDrawingHelper
{
    public GraphicsPath RoundedRect(Rectangle bounds, int radius)
    {
        var diameter = radius * 2;
        var size = new Size(diameter, diameter);
        var arc = new Rectangle(bounds.Location, size);
        var path = new GraphicsPath();

        if (radius == 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        path.AddArc(arc, 180, 90);
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);
        path.CloseFigure();
        return path;
    }

    public void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius)
    {
        using var path = RoundedRect(bounds, cornerRadius);
        graphics.FillPath(brush, path);
    }
}