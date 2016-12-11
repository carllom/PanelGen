using System;

namespace PanelGen.Cli
{
    public class Dial
    {
        public float x;
        public float y;

        public float holeRadius; // radius of hole for pot axis
        public float innerRadius; // inner radius for marker/tick lines

        // Marker lines are labeled lines
        public float arcSpan; // Dial segment arc angle
        public float markerLength; // Length of marker line
        public int minValue; // Min marker value
        public int maxValue; // Max marker value
        public int step; // Value step between markers

        // Tick lines are lines between markers
        public float tickLength; // Length of tick line
        public int tickCount; // Number of tick lines between each marker

        public string text;

        public void DrawDial(IDraw drw, float xc, float yc)
        {
            var startAng = (360 - arcSpan) * Math.PI / 360;
            var mCount = (maxValue - minValue) / step + 1;
            var markerArc = arcSpan / mCount * Math.PI / 180;
            var tickArc = markerArc / tickCount;

            for (var i = 0; i < mCount; i++)
            {
                var mArc = startAng + i * markerArc;
                DrawTick(mArc, xc, yc, innerRadius, innerRadius + markerLength, drw);
                for (var j = tickArc; j < markerArc; j += tickArc)
                {
                    DrawTick(mArc + j, xc, yc, innerRadius, innerRadius + tickLength, drw);
                }
            }
            // Max marker
            DrawTick(startAng + mCount*markerArc, xc, yc, innerRadius, innerRadius + markerLength, drw);

            // Dial text
            //var size = g.MeasureString(d.text, _cncFont);
            //g.DrawLine(Pens.CadetBlue,
            //    xc - size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale,
            //    xc + size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale + size.Height);
            //g.DrawString(d.text, _cncFont, _fontBrush, xc - size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale);
        }

        private static void DrawTick(double angle, float xc, float yc, float rInner, float rOuter, IDraw drw)
        {
            var xk = -(float)Math.Sin(angle);
            var yk = -(float)Math.Cos(angle);
            drw.MoveTo(xc + xk * rInner, yc - yk * rInner);
            drw.LineTo(xc + xk * rOuter, yc - yk * rOuter);
        }

    }
}
