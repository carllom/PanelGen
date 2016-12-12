using System.IO;
using System.Linq;
using PanelGen.Cli;
using System.Globalization;

namespace Gcode
{
    public struct Point
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public Point(float x, float y, float z=0)
        {
            X = x;
            Y = y;
            Z = z;
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
            Raise(); // Move to safe Z
            _writer.WriteLine("M5"); // Spindle off
            MoveTo(0, 0); // Move home
        }
        public void AddLine(Point from, Point to, bool raise=true)
        {
            Begin(from);
            _writer.WriteLine("G1 X{0:0.###} Y{1:0.###}", to.X, to.Y);
            if (raise)
                End();
        }

        public void AddPolyLine(Point[] points, bool raise = true)
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

        private void Begin(Point pos)
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
        private Point _currPos;
        private bool _raised;

        private void Raise()
        {
            if (!_raised)
                _writer.WriteLine("G0 Z{0:0.###} F{1}", Surface + TravelZ, Travelspeed); // move to travel height
            _raised = true;
        }
        private void Lower()
        {
            if (_raised)
                _writer.WriteLine("G0 Z{0:0.###} F{1}", Surface - EngravingDepth, Feedrate); // move to engraving depth
            _raised = false;
        }

        public void MoveTo(float x, float y)
        {
            Raise();
            _writer.WriteLine("G0 X{0:0.###} Y{1:0.###}", -x, y); // move to start point
        }

        public void LineTo(float x, float y)
        {
            Lower();
            _writer.WriteLine("G1 X{0:0.###} Y{1:0.###}", -x, y);
        }

        public void ArcTo(float x, float y)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
