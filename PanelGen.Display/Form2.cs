using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using PanelGen.Cli;

namespace PanelGen.Display
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            panel1.Paint += Panel1_Paint;
            var q = float.Parse("0.5", CultureInfo.InvariantCulture);
            var atf = new Type1Font(@"C:\Projekt\PanelGen\tool\psfonts\Eng_UniversLine.pfa");
            atf.Parse();
            ReadFontFile(@"C:\Projekt\PanelGen\PanelGen.Display\font1.txt");
        }

        private Pen _textErr = new Pen(Color.OrangeRed, 1f);
        private Pen _textPen = new Pen(Color.CadetBlue, 2f);
        private Pen _debugPen = Pens.Orange;

        private const float ls = 1; // Letter width (mm)
        private const float cspc = 0.2f; // Letter spacing (mm)

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            var scale = 30; // Pixels/mm
            var g = e.Graphics;
            var cx = 0f; // Cursor x
            var cy = g.VisibleClipBounds.Height/2; // Cursor y
            var h = ls * scale; // Letter height
            var w = ls * scale; // Letter width
            var w2 = w/2;
            var h2 = h/2;

            g.Clear(BackColor);
            foreach (var letter in textBox1.Text)
            {
                var xl = cx;
                var xr = xl + w;
                var yb = cy;
                var yt = yb - h;
                var x2 = xl + w2;
                var y2 = yb - h2;

                if (charSet.ContainsKey(letter))
                {
                    foreach (var seg in charSet[letter])
                    {

                        if (seg is LineSegment)
                        {
                            var line = seg as LineSegment;
                            var p0 = new PointF(xl + line.x0 * w, yb - line.y0 * h);
                            var p1 = new PointF(xl + line.x1 * w, yb - line.y1 * h);
                            g.DrawLine(_textPen, p0, p1);
                        }
                        else if (seg is ArcSegment)
                        {
                            var arc = seg as ArcSegment;
                            //var corda = new PointF(arc.x1 - arc.x0, arc.y1 - arc.y0); // Corda vector
                            var diam = arc.diam; //(float)Math.Sqrt(corda.X*corda.X + corda.Y*corda.Y); // Circle diameter
                            var rad = arc.rad; // diam/2; // Circle radius
                            var v0 = new PointF(arc.x0 - arc.cx, arc.y0 - arc.cy); // normalized start point (relative center)
                            
                            //var a0c = (float)(Math.Acos(v0.X/rad)*180/Math.PI);
                            //var a0s = (float)(Math.Asin(v0.Y/rad)*180/Math.PI);
                            //var a0 = a0c * (a0s > 0 ? -1 : 1); // Start angle
                            var a0 = (float) (Math.Atan2(v0.Y, v0.X)*180/Math.PI);
                            if (a0 < 0)
                                a0 += 360;

                            var v1 = new PointF(arc.x1 - arc.cx, arc.y1 - arc.cy); // normalized end point (relative center)
                            //var a1c = (float)(Math.Acos(v1.X / rad) * 180 / Math.PI);
                            //var a1s = (float)(Math.Asin(v1.Y / rad) * 180 / Math.PI);
                            //var a1 = a1c * (a1s > 0 ? -1 : 1); // End angle
                            var a1 = (float) (Math.Atan2(v1.Y, v1.X)*180/Math.PI);
                            if (a1 < 0)
                                a1 += 360;

                            var a = a0 - a1;
                            if (a0 < a1)
                                a += 360;
                            if (a.Equals(0))
                                a = 360;

                            g.DrawArc(_textPen, xl + (arc.cx - rad)*w, yb - (arc.cy + rad)*h, diam*h, diam*h, 360-a0, a);
                        }
                    }
                }
                else // Character not found
                {
                    g.DrawRectangle(_textErr, cx, cy - 2*h, w, 2*h);
                    g.DrawLine(_textErr, cx, cy, cx + w, cy - 2*h);
                    g.DrawLine(_textErr, cx, cy - 2*h, cx + w, cy);
                }

                //switch (letter)
                //{
                //    case 'A':
                //    case 'a':
                //        g.DrawLine(_textPen, xl, yb, x2, yt);
                //        g.DrawLine(_textPen, x2, yt, xr, yb);
                //        g.DrawLine(_textPen, xl + w2/2, y2, xr - w2/2, y2);
                //        break;
                //    case 'B':
                //    case 'b':

                //        break;
                //    default: // Draw box
                //        g.DrawRectangle(_textErr, cx, cy - h, w, h);
                //        g.DrawLine(_textErr, cx, cy, cx + w, cy - h);
                //        g.DrawLine(_textErr, cx, cy - h, cx + w, cy);
                //        break;
                //}
                cx += w + cspc*scale;
            }
        }

        private void CalcArcCoords(Graphics g, PointF p0, PointF p1)
        {
            var r = (float)Math.Sqrt((p1.X - p0.X)*(p1.X - p0.X) + (p1.Y - p0.Y)*(p1.Y - p0.Y)); // Radius
            var c = new PointF((p0.X + p1.X)/2, (p0.Y + p1.Y)/2);
            var tl = new PointF(c.X - r, c.Y - r);
            var sArc = Math.Asin(p0.X - c.X);
            var eArc = Math.Asin(p1.X - c.X);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            panel1.Refresh();
        }

        private Dictionary<char, IEnumerable<Segment>> charSet;

        private class Segment
        {
            public float x0;
            public float y0;
            public float x1;
            public float y1;
        }

        private class LineSegment : Segment { }

        private class ArcSegment : Segment
        {
            public float cx;
            public float cy;

            public float diam => rad*2;
            public float rad => (float)Math.Sqrt((x0 - cx)*(x0 - cx) + (y0 - cy)*(y0 - cy));
        }

        private void ReadFontFile(string path)
        {
            using (var rdr = new StreamReader(path))
            {
                charSet = new Dictionary<char, IEnumerable<Segment>>();
                char? currCode = null;
                List<Segment> currSegments = new List<Segment>();
                int lineNbr = -1;
                while (!rdr.EndOfStream)
                {
                    var line = (rdr.ReadLine());
                    lineNbr++;
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var tok = line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

                    if (tok[0].StartsWith("#")) // Comment '#<comment>'
                        continue;

                    if (tok[0].StartsWith("=")) // Define character '=<charcode>'
                    {
                        // new character - save old
                        if (currCode.HasValue)
                        {
                            charSet.Add(currCode.Value, currSegments);
                        }

                        currCode = tok[0][1]; // Second char is charcode
                        currSegments = new List<Segment>();
                        continue;
                    }

                    
                    
                    if (tok[0] == "L") // Line segment 'L <x0> <y0> <x1> <y1>'
                    {
                        if (tok.Length != 5) // Segment type + 4 coordinate values
                        {
                            System.Diagnostics.Debug.WriteLine($"ERROR: Wrong number of parameters in line {lineNbr}: {line}");
                        }

                        var s = new LineSegment
                        {
                            x0 = float.Parse(tok[1], CultureInfo.InvariantCulture),
                            y0 = float.Parse(tok[2], CultureInfo.InvariantCulture),
                            x1 = float.Parse(tok[3], CultureInfo.InvariantCulture),
                            y1 = float.Parse(tok[4], CultureInfo.InvariantCulture)
                        };
                        currSegments.Add(s);
                    }
                    else if (tok[0] == "A") // Arc segment 'A <x0> <y0> <x1> <y1> [<xc> <yc>]'
                    {
                        if (tok.Length != 5 || tok.Length != 7) // Segment type + 4/6 coordinate values
                        {
                            System.Diagnostics.Debug.WriteLine($"ERROR: Wrong number of parameters in line {lineNbr}: {line}");
                        }

                        var s = new ArcSegment
                        {
                            x0 = float.Parse(tok[1], CultureInfo.InvariantCulture),
                            y0 = float.Parse(tok[2], CultureInfo.InvariantCulture),
                            x1 = float.Parse(tok[3], CultureInfo.InvariantCulture),
                            y1 = float.Parse(tok[4], CultureInfo.InvariantCulture)
                        };
                        if (tok.Length == 7) // We have center definition
                        {
                            s.cx = float.Parse(tok[5], CultureInfo.InvariantCulture);
                            s.cy = float.Parse(tok[6], CultureInfo.InvariantCulture);
                        }
                        else // No center - assume semicircle
                        {
                            s.cx = (s.x0 + s.x1)/2;
                            s.cy = (s.y0 + s.y1)/2;
                        }
                        currSegments.Add(s);
                    }
                }
                // Save last character definition
                if (currCode.HasValue)
                {
                    charSet.Add(currCode.Value, currSegments);
                }

            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            ReadFontFile(@"W:\Projekt\gcam\PanelGen\PanelGen.Display\font1.txt");
            panel1.Refresh();
        }
    }
}
