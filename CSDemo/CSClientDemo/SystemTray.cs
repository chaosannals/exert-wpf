using System;
using System.Windows.Forms;

namespace CSClientDemo;

public class SystemTray
{
    public NotifyIcon Raw { get; init; }

    public event MouseEventHandler ClickLeftButton = null!;
    public event MouseEventHandler ClickRightButton = null!;
    public event MouseEventHandler DoubleClickLeftButton = null!;

    public SystemTray()
    {
        Raw = new NotifyIcon()
        {
            Icon = Properties.Resources.Token,
            Visible = true,
        };
        Raw.MouseClick += (o, e) =>
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    ClickLeftButton.Invoke(o, e);
                    break;
                case MouseButtons.Right:
                    ClickRightButton.Invoke(o, e);
                    break;
            }
        };
        Raw.MouseDoubleClick += (o, e) =>
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    DoubleClickLeftButton.Invoke(o, e);
                    break;
            }
        };
    }
}
