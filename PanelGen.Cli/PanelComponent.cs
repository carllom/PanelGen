using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace PanelGen.Cli
{
    public class ExtentsRenderer : IDraw
    {
        public Vertex3 min = new Vertex3(float.MaxValue, float.MaxValue, float.MaxValue);
        public Vertex3 max = new Vertex3(float.MinValue, float.MinValue, float.MinValue);
        public Vertex3 Extents => max - min;

        public bool Inside(float x, float y) =>
            (x >= min.x) && (x <= max.x) && (y >= min.y) && (y <= max.y);

        public void AddPos(float x, float y)
        {
            if (x < min.x)
                min.x = x;
            if (x > max.x)
                max.x = x;
            if (y < min.y)
                min.y = y;
            if (y > max.y)
                max.y = y;
        }

        public void LineTo(float x, float y)
        {
            AddPos(x, y);
        }

        public void MoveTo(float x, float y)
        {
            AddPos(x, y);
        }
    }

    public struct Vertex3
    {
        public Vertex3 (float x, float y, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float x;
        public float y;
        public float z;

        public float Length => (float)Math.Sqrt(x * x + y * y);
        public Vertex2 Xy => new Vertex2(x, y);

        public static Vertex3 operator -(Vertex3 a, Vertex3 b) => new Vertex3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vertex3 operator +(Vertex3 a, Vertex3 b) => new Vertex3(a.x + b.x, a.y + b.y, a.z + b.z);

        public Vertex3 Rot(bool rotate)
        {
            return rotate ? new Vertex3(y, -x) : this;
        }
    }

    public struct Vertex2
    {
        public Vertex2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float x;
        public float y;

        public float Length => (float)Math.Sqrt(x * x + y * y);
        public Vertex2 Normal => new Vertex2(-y, x);
        public Vertex2 Normalize => new Vertex2(x, y) / Length;
        public static Vertex2 operator -(Vertex2 a, Vertex2 b) => new Vertex2(a.x - b.x, a.y - b.y);
        public static Vertex2 operator +(Vertex2 a, Vertex2 b) => new Vertex2(a.x + b.x, a.y + b.y);
        public static Vertex2 operator -(Vertex2 v) => new Vertex2(-v.x, -v.y);
        public static Vertex2 operator *(Vertex2 v, float c) => new Vertex2(v.x * c, v.y * c);
        public static Vertex2 operator /(Vertex2 v, float c) => new Vertex2(v.x / c, v.y / c);
        public override string ToString()
        {
            return $"({x}:{y})";
        }

        public Vertex2 Rot(bool rotate)
        {
            return rotate ? new Vertex2(y, -x) : this;
        }
    }

    public struct Segment2
    {
        public Vertex2 begin;
        public Vertex2 end;

        public Segment2(Vertex2 begin, Vertex2 end)
        {
            this.begin = begin;
            this.end = end;
        }
        public Vertex2 Normal => new Vertex2(begin.y - end.y, end.x - begin.x);
        public override string ToString()
        {
            return $"[{begin}..{end}]";
        }

        public Segment2 Offset(Vertex2 v)
        {
            return new Segment2(begin + v, end + v);
        }
    }

    public abstract class PanelComponent : IPanelGenFileObject
    {
        public Vertex3 pos;

        public abstract Vertex3 Extents { get; }
        public abstract bool Inside(float x, float y);

        public virtual void Load(BinaryReader data)
        {
            pos.x = data.ReadSingle();
            pos.y = data.ReadSingle();
            pos.z = data.ReadSingle();
        }

        public virtual void Save(BinaryWriter data)
        {
            data.Write(pos.x);
            data.Write(pos.y);
            data.Write(pos.z);
        }

        public void ReadXml(XmlElement elem)
        {
            throw new NotImplementedException();
        }


        public virtual XmlElement AsXml(XmlDocument doc)
        {
            var elem = doc.CreateElement("Position");
            elem.SetAttribute("x", pos.x.ToString(CultureInfo.InvariantCulture));
            elem.SetAttribute("y", pos.y.ToString(CultureInfo.InvariantCulture));
            elem.SetAttribute("z", pos.z.ToString(CultureInfo.InvariantCulture));
            return elem;
        }

        private const byte TYPE_DIAL = 1;
        private const byte TYPE_CIRCPOCKET = 2;
        private const byte TYPE_RECTPOCKET = 3;
        private const byte TYPE_TEXT = 4;
        private const byte TYPE_POLYLINE = 5;

        public static PanelComponent ReadObject(BinaryReader data)
        {
            var type = data.ReadByte();
            PanelComponent obj;
            switch (type)
            {
                case TYPE_DIAL:
                    obj = new Dial();
                    break;
                case TYPE_CIRCPOCKET:
                    obj = new CircularPocket();
                    break;
                case TYPE_RECTPOCKET:
                    obj = new RectangularPocket();
                    break;
                case TYPE_TEXT:
                    obj = new Text("");
                    break;
                case TYPE_POLYLINE:
                    obj = new PolyLine();
                    break;
                default:
                    throw new Exception($"Got unknown Panel component type {type}");
            }
            obj.Load(data);
            return obj;
        }

        public static void WriteObject(BinaryWriter data, PanelComponent obj)
        {
            if (obj is Dial)
                data.Write(TYPE_DIAL);
            else if (obj is CircularPocket)
                data.Write(TYPE_CIRCPOCKET);
            else if (obj is RectangularPocket)
                data.Write(TYPE_RECTPOCKET);
            else if (obj is Text)
                data.Write(TYPE_TEXT);
            else if (obj is PolyLine)
                data.Write(TYPE_POLYLINE);
            obj.Save(data);
        }
    }
}
