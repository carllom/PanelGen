using PanelGen.Cli;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public class ViewPanel : Panel
    {
        public PointF Origo = new PointF();
        private float origoSize = 10;

        public PanelGenApplication Model;
        private float _zoom = 1;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.Black);
            e.Graphics.DrawLine(Pens.Red, Origo.X - origoSize, Origo.Y, Origo.X + origoSize, Origo.Y);
            e.Graphics.DrawLine(Pens.Green, Origo.X, Origo.Y - origoSize, Origo.X, Origo.Y + origoSize);

            var draw = new ScreenDraw(e.Graphics, Pens.LightCyan, _zoom, Origo);
            if (Model.panel != null)
            {
                Model.panel.Draw(draw);
                foreach (var item in Model.panel.items)
                {
                    if (item is Dial)
                    {
                        var d = item as Dial;
                        d.DrawDial(draw, d.x, d.y);
                    }
                    // Draw them as well
                }
            }
        }

        private const float WheelScale = 1 / 120f;
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            _zoom += e.Delta * WheelScale;
            Invalidate();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Invalidate();
        }

        private Point _lastMPos;
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Drag
            if (e.Button == MouseButtons.Right)
            {
                var xd = e.Location.X - _lastMPos.X;
                var yd = e.Location.Y - _lastMPos.Y;
                Origo = new PointF(Origo.X + xd, Origo.Y + yd);
                Invalidate();
            }
            // Update to current mouse position
            _lastMPos = e.Location;
        }
    }
}
