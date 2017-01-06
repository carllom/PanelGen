using PanelGen.Cli;
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
        private float _zoom = 10; // 1mm = 10pixels

        private Pen gridPen = new Pen(Color.FromArgb(10, Color.White));
        private bool _showgrid;
        public bool ShowGrid
        {
            get { return _showgrid; }
            set
            {
                var changed = _showgrid != value; _showgrid = value; if (changed) Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.Clear(Color.Black);
            e.Graphics.DrawLine(Pens.Red, Origo.X - origoSize, Origo.Y, Origo.X + origoSize, Origo.Y);
            e.Graphics.DrawLine(Pens.Green, Origo.X, Origo.Y - origoSize, Origo.X, Origo.Y + origoSize);

            if (ShowGrid)
            {
                var gridsize = _zoom;
                if (gridsize > 9) // Do not display too tight grid
                {
                    var gridX = Origo.X % gridsize;
                    var gridY = Origo.Y % gridsize;
                    while (gridX < Width)
                    {
                        e.Graphics.DrawLine(gridPen, gridX, 0, gridX, Height);
                        gridX += gridsize;
                    }
                    while (gridY < Height)
                    {
                        e.Graphics.DrawLine(gridPen, 0, gridY, Width, gridY);
                        gridY += gridsize;
                    }
                }
            }

            var draw = new ScreenDraw(e.Graphics, Pens.LightCyan, _zoom, Origo);
            if (Model?.panel != null)
            {
                var p = Model.panel;
                e.Graphics.DrawRectangle(Pens.LightCoral,
                    ScreenX(p.pos.x),
                    ScreenY(p.pos.y + p.height),
                    p.width * _zoom,
                    p.height * _zoom);
                    
                foreach (var item in Model.panel.items)
                {
                    if (item is Dial)
                    {
                        var d = item as Dial;
                        d.DrawDial(draw, d.pos.x, d.pos.y);
                        e.Graphics.DrawEllipse(Pens.Purple,
                            ScreenX(d.pos.x - d.holeRadius),
                            ScreenY(d.pos.y + d.holeRadius),
                            d.holeRadius * 2 * _zoom,
                            d.holeRadius * 2 * _zoom);

                    }
                    else if (item is Text)
                    {
                        var t = item as Text;
                        t.Draw(draw);
                    }
                    else if (item is RectangularPocket)
                    {
                        var rp = item as RectangularPocket;
                        // Screen representation
                        e.Graphics.DrawRectangle(Pens.Purple,
                            ScreenX(rp.pos.x - rp.width / 2),
                            ScreenY(rp.pos.y + rp.height / 2),
                            rp.width * _zoom,
                            rp.height * _zoom);
                    }
                    else if (item is CircularPocket)
                    {
                        var cp = item as CircularPocket;
                        // Screen representation
                        e.Graphics.DrawEllipse(Pens.Purple,
                            ScreenX(cp.pos.x - cp.diameter / 2),
                            ScreenY(cp.pos.y + cp.diameter / 2),
                            cp.diameter * _zoom,
                            cp.diameter * _zoom);
                    }
                    // Draw them as well
                }
            }

            if (Model?.selected != null)
            {
                var sel = Model.selected;
                var ext = Model.selected.Extents;
                e.Graphics.DrawRectangle(Pens.SeaGreen,
                    ScreenX(sel.pos.x - ext.x / 2),
                    ScreenY(sel.pos.y + ext.y/ 2),
                    ext.x * _zoom,
                    ext.y * _zoom);
            }
        }

        public Vertex2 DrawPosition
        {
            get
            {
                return DrawPos(_lastMPos);
            }
        }

        private Vertex2 DrawPos(Point pos)
        {
            return new Vertex2((pos.X - Origo.X) / _zoom, (Origo.Y - pos.Y) / _zoom);
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
