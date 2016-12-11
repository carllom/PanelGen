using System;
using System.Drawing;
using PanelGen.Cli;

namespace PanelGen.Display
{
    internal class ScreenDraw : IDraw
    {
        private PointF _screenOrigo;
        private PointF _currPos; // Current pos (source coordinates - not screen coordinates)
        private readonly Pen _p;
        private readonly Graphics _g;
        private readonly float _scale;
        public ScreenDraw(Graphics g, Pen p, float scale, PointF screenOrigo)
        {
            _g = g;
            _p = p;
            _scale = scale;
            _screenOrigo = screenOrigo;
        }

        public void MoveTo(float x, float y)
        {
            _currPos = new PointF(x, y);
        }

        public void LineTo(float x, float y)
        {
            var toPos = new PointF(x, y);
            _g.DrawLine(_p, ToScreen(_currPos), ToScreen(toPos));
            _currPos = toPos;
        }

        public void ArcTo(float x, float y)
        {
            throw new NotImplementedException();
        }

        private PointF ToScreen(PointF p)
        {
            return ToScreen(p.X, p.Y);
        }

        private PointF ToScreen(float x, float y)
        {
            return new PointF(x*_scale + _screenOrigo.X, y*_scale + _screenOrigo.Y);
        }
    }
}
