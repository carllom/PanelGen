using System.IO;
using System.Linq;
using System.Globalization;
using System;

namespace PanelGen.Cli
{
    public struct CncPoint
    {
        public readonly float X;
        public readonly float Y;

        public CncPoint(float x, float y)
        {
            X = x;
            Y = y;
        }
    }

    public class GCodeEngraver : IDraw
    {
        public float EngravingDepth { get; set; } = 0.3f;
        public float Surface { get; set; } = 0;
        public float TravelZ { get; set; } = 1;

        public float Feedrate { get; set; } = 100;  // Working(engraving) feedrate
        public float Travelspeed { get; set; } = 1500; // (Free) travel speed

        private StringWriter _writer = new StringWriter(CultureInfo.InvariantCulture);

        public void Init()
        {
            _writer?.Dispose();
            _writer = new StringWriter(CultureInfo.InvariantCulture);
            _writer.WriteLine("G17"); // Select XY plane
            _writer.WriteLine("G21"); // Units in mm
            _writer.WriteLine("M3 S1000"); // Spindle on speed 1000
        }

        public void Finish()
        {
            RaiseTool(); // Move to safe Z
            _writer.WriteLine("M5"); // Spindle off
            MoveTo(0, 0); // Move home
        }
        public void AddLine(CncPoint from, CncPoint to, bool raise=true)
        {
            Begin(from);
            _writer.WriteLine("G1 X{0:0.###} Y{1:0.###}", to.X, to.Y);
            if (raise)
                End();
        }

        public void AddPolyLine(CncPoint[] points, bool raise = true)
        {
            if (points == null || points.Length == 0)
                return;

            Begin(points[0]);
            foreach (var point in points.Skip(1))
            {
                _writer.WriteLine("G1 X{0:0.###} Y{1:0.###}", point.X, point.Y);
            }
            if (raise)
                End();
        }

        public void AddFatLine(Vertex2[] points, Tool t, float width)
        {
            // 1. Calc offsets from startpoint 
            var offset = (width - t.diameter) / 2f;
            // total width - 2*tool radius (either side), the rest divided by 2 to get +/- offset from center line

            var seg = new Segment2();

            for (int i = 0; i < points.Length - 1; i++)
            {
                seg.begin = points[i];
                seg.end = points[i + 1];
                FatSegment(seg, t, offset);
            }
        }

        private const float overlap = 0.2f;

        private void FatSegment(Segment2 seg, Tool t, float offset)
        {
            // 1 Start with center line (begin-end)
            MoveTo(seg.begin); //G0(seg.begin); Lower();
            LineTo(seg.end); //G1(seg.end);

            // 1.5 Prepare extrude values
            var n = seg.Normal.Normalize; // +extrude
            //var n2 = -n; // -extrude
            var toff = 0f;


            while (toff < offset)
            {

                toff += Math.Min(offset, t.diameter * (1 - overlap)); // extend extrude
                var xtr1 = seg.Offset(n * toff);
                var xtr2 = seg.Offset(-n * toff);

                // 2 Move to +extrude "end"
                LineTo(xtr1.end); //G1(xtr1.end);

                // 3 Draw +extrude(end - begin)
                LineTo(xtr1.begin); // G1(xtr1.begin);

                // 4 Endcap(begin) to -extrude "begin"
                LineTo(xtr2.begin); //G1(xtr2.begin); // Just linear endcap for now

                // 5 Draw -extrude(begin - end)
                LineTo(xtr2.end); //G1(xtr2.end);

                // 6 Endcap(end) to +extrude "end"
                LineTo(xtr1.end); //G1(xtr1.end);

            } // 7 if < width pick next extrude size and goto 2
        }


        private void Begin(CncPoint pos)
        {
            _writer.WriteLine("G0 X{0:0.###} Y{1:0.###}", pos.X, pos.Y); // move to start point
            _writer.WriteLine("G0 Z{0:0.###} F{1}", Surface - EngravingDepth, Feedrate); // move to engraving depth
        }

        private void End()
        {
            _writer.WriteLine("G0 Z{0:0.###} F{1}", Surface + TravelZ, Travelspeed); // move to travel height
        }

        public string GCode()
        {
            return _writer.ToString();
        }

        #region IDraw interface
        private CncPoint _currPos;
        private bool _raised;

        private void RaiseTool()
        {
            if (!_raised)
                _writer.WriteLine("G0 Z{0:0.###} F{1}", Surface + TravelZ, Travelspeed); // move to travel height
            _raised = true;
        }
        private void LowerTool()
        {
            if (_raised)
                _writer.WriteLine("G0 Z{0:0.###} F{1}", Surface - EngravingDepth, Feedrate); // move to engraving depth
            _raised = false;
        }

        public void Raise()
        {
            _writer.WriteLine("G0 Z{0:0.###} F{1}", Surface + TravelZ, Travelspeed); // move to travel height
        }

        public void MoveTo(float x, float y)
        {
            RaiseTool();
            _writer.WriteLine("G0 X{0:0.###} Y{1:0.###}", x, y); // move to start point
        }

        public void LineTo(float x, float y)
        {
            LowerTool();
            _writer.WriteLine("G1 X{0:0.###} Y{1:0.###}", x, y);
        }

        public void MoveTo(Vertex2 v) => MoveTo(v.x, v.y);
        public void LineTo(Vertex2 v) => LineTo(v.x, v.y);

        #endregion
    }
}
