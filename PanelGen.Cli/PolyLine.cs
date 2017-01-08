using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelGen.Cli
{
    public class PolyLine : PanelStockItem
    {
        public override Vertex3 Extents
        {
            get
            {
                var xr = new ExtentsRenderer();
                Draw(xr);
                return xr.Extents;
            }
        }

        public override bool Inside(float x, float y)
        {
            var p = new Vertex2(x, y);
            p = p - pos.Xy;
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (DistToSegment(p, points.ElementAt(i), points.ElementAt(i + 1)) < 1.5)
                {
                    return true;
                }
            }
            return false;
        }

        public float radius;
        public ICollection<Vertex2> points = new List<Vertex2>();

        public void Draw(IDraw drw)
        {
            var p = points.ElementAt(0);
            drw.MoveTo(p.x + pos.x, p.y + pos.y);
            for (int i = 1; i < points.Count; i++)
            {
                p = points.ElementAt(i);
                drw.LineTo(p.x + pos.x, p.y + pos.y);
            }
        }

        public override void GenerateCode(TextWriter writer, Tool tool)
        {
            var engr = new GCodeEngraver();
            Draw(engr);
            writer.WriteLine(engr.GCode());
        }

        public override void Load(BinaryReader data)
        {
            base.Load(data);
            radius = data.ReadSingle();
            var numPoints = data.ReadByte();
            for (int i = 0; i < numPoints; i++)
            {
                var p = new Vertex2();
                p.x = data.ReadSingle();
                p.y = data.ReadSingle();
                points.Add(p);
            }
        }

        public override void Save(BinaryWriter data)
        {
            base.Save(data);
            data.Write(radius);
            data.Write((byte)points.Count);
            foreach(var point in points)
            {
                data.Write(point.x);
                data.Write(point.y);
            }
        }

        private float Sqr(float x) => x * x;
        private float Dist2(Vertex2 v, Vertex2 w) => Sqr(v.x - w.x) + Sqr(v.y - w.y);
        private float DistToSegment(Vertex2 p, Vertex2 v, Vertex2 w)
        {
            var l2 = Dist2(v, w);
            if (l2 == 0)
                return Dist2(p, v);
            var t = ((p.x - v.x) * (w.x - v.x) + (p.y - v.y) * (w.y - v.y)) / l2;
            t = Math.Max(0, Math.Min(1, t));
            return (float)Math.Sqrt(Dist2(p,
                new Vertex2(v.x + t * (w.x - v.x), v.y + t * (w.y - v.y))));
        }
    }
}
