using System;
using System.Drawing;
using System.Windows.Forms;
using Gcode;
using PanelGen.Cli;

namespace PanelGen.Display
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            panel1.Paint += Panel1_Paint;
            _dial = CreateDummyDial();
            RenderDial2(panel1.CreateGraphics(), _dial);
            var gceng = new GCodeEngraver();
            gceng.Init();
            _dial.DrawDial(gceng, 0, 0);
            var result = gceng.GCode();
        }

        private Dial CreateDummyDial()
        {
            return new Dial
            {
                x = 0,
                y = 0,
                holeRadius = 6,

                minValue = 0,
                maxValue = 10,
                step = 1,
                innerRadius = 7.5f,
                arcSpan = 300,
                markerLength = 3.5f,
                tickCount = 5,
                tickLength = 2,
                text = "KNOBHEAD"
            };
        }

        private readonly Dial _dial;

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            RenderDial2(e.Graphics, _dial);
        }

        private void RenderDial2(Graphics g, Dial d)
        {
            const int scale = 10;
            var xc = g.VisibleClipBounds.Width / 2;
            var yc = g.VisibleClipBounds.Height / 2;
            g.DrawLine(Pens.Aquamarine, 0, 0, xc, yc);

            var sdrw = new ScreenDraw(g, _tickPen, scale, new PointF((int)xc, (int)yc));
            d.DrawDial(sdrw, 0, 0);

            // Draw text
            var size = g.MeasureString(d.text, _cncFont);
            g.DrawLine(Pens.CadetBlue,
                xc - size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale,
                xc + size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale + size.Height);

            var font = new HersheyFont(@"W:\Projekt\gcam\PanelGen\hershey");
            var fWidth = font.MeasureWidth(d.text);
            font.DrawString(sdrw, d.text, -fWidth/2 + 1, d.innerRadius + d.markerLength + 1);

            //g.DrawString(d.text, _cncFont, _fontBrush, xc - size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale);
        }
#if OLDIMPL
        private void RenderDial(Graphics g, Dial d)
        {
            //g.VisibleClipBounds.Width;
            var xc = g.VisibleClipBounds.Width / 2;
            var yc = g.VisibleClipBounds.Height / 2;
            g.DrawLine(Pens.Aquamarine, 0, 0, xc, yc);

            var scale = 10;

            var startAng = (360 - d.arcSpan) * Math.PI / 360;
            var mCount = (d.maxValue - d.minValue) / d.step + 1;
            var markerArc = d.arcSpan / mCount * Math.PI / 180;
            var tickArc = markerArc / d.tickCount;
            for (int i = 0; i < mCount; i++)
            {
                var mArc = startAng + i * markerArc;
                RenderTick(mArc, xc, yc, d.innerRadius * scale, (d.innerRadius + d.markerLength) * scale, g, _markerPen);
                for (double j = tickArc; j < markerArc; j += tickArc)
                {
                    RenderTick(mArc + j, xc, yc, d.innerRadius * scale, (d.innerRadius + d.tickLength) * scale, g, _tickPen);
                }
            }
            // Max marker
            RenderTick(startAng + mCount * markerArc, xc, yc, d.innerRadius * scale, (d.innerRadius + d.markerLength) * scale, g, _markerPen);
            var size = g.MeasureString(d.text, _cncFont);
            g.DrawLine(Pens.CadetBlue,
                xc - size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale,
                xc + size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale + size.Height);
            g.DrawString(d.text, _cncFont, _fontBrush, xc - size.Width / 2, yc + (d.innerRadius + d.markerLength) * scale);
        }

        private void RenderTick(double angle, float xc, float yc, float innerRadius, float outerRadius, Graphics g, Pen p)
        {
            var xk = -(float)Math.Sin(angle);
            var yk = -(float)Math.Cos(angle);
            g.DrawLine(p, xc+xk*innerRadius, yc-yk*innerRadius, xc+xk*outerRadius, yc-yk*outerRadius);
        }
#endif
        private readonly Pen _markerPen = new Pen(Color.CadetBlue, 2.5f);
        private readonly Pen _tickPen = new Pen(Color.CadetBlue, 2f);
        private readonly Brush _fontBrush = Brushes.CadetBlue;
        private readonly Font _cncFont = new Font(new FontFamily("Segoe UI Light"), 32);

        private void panel1_Resize(object sender, EventArgs e)
        {
            panel1.Refresh();
        }
    }
}
