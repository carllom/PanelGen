﻿using PanelGen.Cli;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PanelGen.Display
{
    public class ViewPanel : Panel
    {
        public PointF Origo = new PointF(float.NaN, float.NaN);
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
            if (Model?.panel != null)
            {
                var p = Model.panel;
                e.Graphics.DrawRectangle(Pens.LightCoral,
                    ScreenX(p.x),
                    ScreenY(p.y + p.height),
                    p.width * _zoom,
                    p.height * _zoom);
                    
                foreach (var item in Model.panel.items)
                {
                    if (item is Dial)
                    {
                        var d = item as Dial;
                        d.DrawDial(draw, d.x, d.y);
                        e.Graphics.DrawEllipse(Pens.Purple,
                            ScreenX(d.x - d.holeRadius),
                            ScreenY(d.y + d.holeRadius),
                            d.holeRadius * 2 * _zoom,
                            d.holeRadius * 2 * _zoom);

                    }
                    else if (item is RectangularPocket)
                    {
                        var rp = item as RectangularPocket;
                        // Screen representation
                        e.Graphics.DrawRectangle(Pens.Purple,
                            ScreenX(rp.x - rp.width / 2),
                            ScreenY(rp.y + rp.height / 2),
                            rp.width * _zoom,
                            rp.height * _zoom);
                    }
                    else if (item is CircularPocket)
                    {
                        var cp = item as CircularPocket;
                        // Screen representation
                        e.Graphics.DrawEllipse(Pens.Purple,
                            ScreenX(cp.x - cp.diameter / 2),
                            ScreenY(cp.y + cp.diameter / 2),
                            cp.diameter * _zoom,
                            cp.diameter * _zoom);
                    }
                    // Draw them as well
                }
            }
        }

        private float ScreenX(float x)
        {
            return Origo.X + x * _zoom;
        }

        private float ScreenY(float y)
        {
            return Origo.Y - y * _zoom;
        }


        private const float WheelScale = 1 / 120f;
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            _zoom += e.Delta * WheelScale;
            if (_zoom < 1)
            {
                _zoom = 1;
            }
            Invalidate();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            if (float.IsNaN(Origo.X)|| float.IsNaN(Origo.Y))
            {
                Origo = new PointF(Width / 2, Height / 2);
            }
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
