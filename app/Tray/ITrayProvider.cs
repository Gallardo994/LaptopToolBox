using System;
using System.Drawing;
using System.Windows.Forms;

namespace GHelper.Tray;

public interface ITrayProvider : IDisposable
{
    public void SetVisible(bool visible);
    public void SetToolTip(string text);
    public void SetIcon(Icon? icon);
    public void SetContextMenuStrip(ContextMenuStrip? contextMenuStrip);
}