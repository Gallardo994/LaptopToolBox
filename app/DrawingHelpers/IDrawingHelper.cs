namespace GHelper.DrawingHelpers;

public interface IDrawingHelper
{
    public void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle bounds, int cornerRadius);
}