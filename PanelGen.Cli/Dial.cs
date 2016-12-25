using System;

namespace PanelGen.Cli
{
    public class Dial : PanelStockItem
    {
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

        // Labels and text
        public string text; // Dial label
        public HersheyFont Font { get; set; } = new HersheyFont(@"C:\Projekt\PanelGen\tool\hershey");
        public float markerLabelOffset = 1.5f;

        public void DrawDial(IDraw drw, float xc, float yc)
        {
            var startAng = (360 - arcSpan) * Math.PI / 360;
            var mCount = (maxValue - minValue) / step;
            var markerArc = arcSpan / mCount * Math.PI / 180;
            var tickArc = markerArc / (tickCount + 1);
            var outerRadius = innerRadius + markerLength;

            Font.Size = 1.5f;

            for (var i = 0; i < mCount; i++)
            {
                var mArc = startAng + i * markerArc;
                DrawTick(mArc, xc, yc, innerRadius, outerRadius, drw);
                DrawTickLabel((minValue + i*step).ToString(), mArc, xc, yc, outerRadius + markerLabelOffset, drw);
                for (var j = tickArc; j < markerArc; j += tickArc)
                {
                    DrawTick(mArc + j, xc, yc, innerRadius, innerRadius + tickLength, drw);
                }
            }
            // Max marker
            var maxAngle = startAng + mCount * markerArc;
            DrawTick(maxAngle, xc, yc, innerRadius, outerRadius, drw);
            DrawTickLabel(maxValue.ToString(), maxAngle, xc, yc, outerRadius + markerLabelOffset, drw);

            // Dial text
            Font.Size = 3f;
            var fWidth = Font.Width(text);
            Font.DrawString(drw, text, xc - fWidth / 2, yc + innerRadius + markerLength + 3f, false); //TODO: Fix offsets - letters are rendered offset to center
        }

        private static void DrawTick(double angle, float xc, float yc, float rInner, float rOuter, IDraw drw)
        {
            var xk = -(float)Math.Sin(angle);
            var yk = -(float)Math.Cos(angle);
            drw.MoveTo(xc + xk * rInner, yc - yk * rInner);
            drw.LineTo(xc + xk * rOuter, yc - yk * rOuter);
        }

        private void DrawTickLabel(string text, double angle, float xc, float yc, float dist, IDraw drw)
        {
            var w = Font.InnerWidth(text);
            var xk = -(float)Math.Sin(angle);
            var yk = -(float)Math.Cos(angle);
            if (xk < -1e-8)
                w = -w; // Left side, align to end
            else if (xk <= 0)
                w = -w / 2; // Top side, align to center
            else
                w = 0; // Right side align to start

            Font.DrawString(drw, text, xc + xk * dist + w, yc - yk * dist);
        }
    }
}
