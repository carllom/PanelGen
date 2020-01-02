using System;
using System.IO;

namespace PanelGen.Cli.FileFormats
{
    class BinaryPanelGenFileStrategy : IPanelGenFileStrategy
    {
        protected const string Marker = @"PanelGen"; // File type marker

        // Object types
        protected const byte TYPE_DIAL = 1;
        protected const byte TYPE_CIRCPOCKET = 2;
        protected const byte TYPE_RECTPOCKET = 3;
        protected const byte TYPE_TEXT = 4;
        protected const byte TYPE_POLYLINE = 5;

        public PanelGenProject Read(Stream s)
        {
            using (var br = new BinaryReader(s))
            {
                return ReadProject(br);
            }
        }

        private static PanelGenProject ReadProject(BinaryReader br)
        {
            var marker = br.ReadString();
            if (marker != Marker)
                throw new Exception("Unknown file format");
            var pgp = new PanelGenProject();

            var rev = br.ReadInt16();
            if (rev == 0x100)
            {
                // Stock components
                pgp.Stock = ReadStock(br);

                // Tools
                var nTools = br.ReadByte();
                for (int i = 0; i < nTools; i++)
                {
                    pgp.Tools.Add(ReadTool(br));
                }
            }
            return pgp;
        }

        private static PanelStock ReadStock(BinaryReader br)
        {
            var ps = new PanelStock();
            ps.pos = br.ReadVertex3();
            ps.width = br.ReadSingle();
            ps.height = br.ReadSingle();
            ps.thickness = br.ReadSingle();
            var numItems = br.ReadInt32();
            for (int i = 0; i < numItems; i++)
            {
                ps.items.Add(ReadObject(br));
            }
            return ps;
        }

        #region PanelStockItems
        private static PanelStockItem ReadObject(BinaryReader br)
        {
            var type = br.ReadByte();
            switch (type)
            {
                case TYPE_DIAL:
                    return ReadDial(br);
                case TYPE_CIRCPOCKET:
                    return ReadCircularPocket(br);
                case TYPE_RECTPOCKET:
                    return ReadRectangularPocket(br);
                case TYPE_TEXT:
                    return ReadText(br);
                case TYPE_POLYLINE:
                    return ReadPolyLine(br);
                default:
                    throw new Exception($"Got unknown Panel component type {type}");
            }
        }

        private static Dial ReadDial(BinaryReader br)
        {
            var d = new Dial();
            d.pos = br.ReadVertex3();
            d.toolNumber = br.ReadByte();
            d.holeRadius = br.ReadSingle();
            d.holeDepth = br.ReadSingle();
            d.innerRadius = br.ReadSingle();
            d.arcSpan = br.ReadSingle();
            d.markerLength = br.ReadSingle();
            d.minValue = br.ReadInt32();
            d.maxValue = br.ReadInt32();
            d.step = br.ReadInt32();
            d.tickLength = br.ReadSingle();
            d.tickCount = br.ReadInt32();
            d.text = br.ReadString();
            d.markerLabelOffset = br.ReadSingle();
            d.MarkerFont.Size = br.ReadSingle();
            d.LabelFont.Size = br.ReadSingle();
            d.holeToolNumber = br.ReadByte();
            return d;
        }

        private static CircularPocket ReadCircularPocket(BinaryReader br)
        {
            var cp = new CircularPocket();
            cp.pos = br.ReadVertex3();
            cp.toolNumber = br.ReadByte();
            cp.diameter = br.ReadSingle();
            cp.depth = br.ReadSingle();

            var numSteps = br.ReadByte();
            for (int i = 0; i < numSteps; i++)
            {
                var dia = br.ReadSingle();
                var dep = br.ReadSingle();
                var s = new CircularPocket.Step(dia, dep);
                cp.steps.Add(s);
            }
            return cp;
        }

        private static RectangularPocket ReadRectangularPocket(BinaryReader br)
        {
            var rp = new RectangularPocket();
            rp.pos = br.ReadVertex3();
            rp.toolNumber = br.ReadByte();
            rp.width = br.ReadSingle();
            rp.height = br.ReadSingle();
            rp.depth = br.ReadSingle();
            return rp;
        }

        private static Text ReadText(BinaryReader br)
        {
            var t = new Text("");
            t.pos = br.ReadVertex3();
            t.toolNumber = br.ReadByte();
            t.text = br.ReadString();
            t.font.Size = br.ReadSingle();
            t.anchor = (Alignment)br.ReadByte();
            return t;
        }

        private static PolyLine ReadPolyLine(BinaryReader br)
        {
            var pl = new PolyLine();
            pl.pos = br.ReadVertex3();
            pl.toolNumber = br.ReadByte();
            pl.radius = br.ReadSingle();
            var numPoints = br.ReadByte();
            for (int i = 0; i < numPoints; i++)
            {
                pl.points.Add(br.ReadVertex2());
            }
            return pl;
        }
        #endregion

        private static Tool ReadTool(BinaryReader br)
        {
            var t = new Tool();
            t.number = br.ReadInt32();
            t.diameter = br.ReadSingle();
            t.zStep = br.ReadSingle();
            return t;
        }

        public void Write(Stream s, PanelGenProject pgp)
        {
            using (var bw = new BinaryWriter(s))
            {
                Write(bw, pgp);
            }
        }

        private static void Write(BinaryWriter bw, PanelGenProject pgp)
        {
            if (pgp.Stock == null)
            {
                return;
            }

            bw.Write(Marker); // Marker
            bw.Write((short)0x0100); // Version (major,minor)

            // Stock components
            Write(bw, pgp.Stock);

            // Tools
            bw.Write((byte)pgp.Tools.Count);
            foreach (var tool in pgp.Tools)
            {
                Write(bw, tool);
            }
        }
       
        private static void Write(BinaryWriter bw, PanelStock ps)
        {
            bw.Write(ps.pos);
            bw.Write(ps.width);
            bw.Write(ps.height);
            bw.Write(ps.thickness);
            bw.Write(ps.items.Count);
            foreach (var item in ps.items)
            {
                switch (item)
                {
                    case Dial d:
                        Write(bw, d);
                        break;
                    case CircularPocket cp:
                        Write(bw, cp);
                        break;
                    case RectangularPocket rp:
                        Write(bw, rp);
                        break;
                    case Text t:
                        Write(bw, t);
                        break;
                    case PolyLine pl:
                        Write(bw, pl);
                        break;
                    default:
                        throw new Exception($"Save not supported for component type {item.GetType().Name}");
                }
            }
        }

        private static void Write(BinaryWriter bw, Tool t)
        {
            bw.Write(t.number);
            bw.Write(t.diameter);
            bw.Write(t.zStep);
        }

        #region PanelStockItems
        private static void Write(BinaryWriter bw, CircularPocket cp)
        {
            bw.Write(TYPE_CIRCPOCKET);
            bw.Write(cp.pos);
            bw.Write(cp.toolNumber);
            bw.Write(cp.diameter);
            bw.Write(cp.depth);

            bw.Write((byte)cp.steps.Count);
            foreach (var step in cp.steps)
            {
                bw.Write(step.diameter);
                bw.Write(step.depth);
            }
        }

        private static void Write(BinaryWriter bw, Dial d)
        {
            bw.Write(TYPE_DIAL);
            bw.Write(d.pos);
            bw.Write(d.toolNumber);
            bw.Write(d.holeRadius);
            bw.Write(d.holeDepth);
            bw.Write(d.innerRadius);
            bw.Write(d.arcSpan);
            bw.Write(d.markerLength);
            bw.Write(d.minValue);
            bw.Write(d.maxValue);
            bw.Write(d.step);
            bw.Write(d.tickLength);
            bw.Write(d.tickCount);
            bw.Write(d.text);
            bw.Write(d.markerLabelOffset);
            bw.Write(d.MarkerFont.Size);
            bw.Write(d.LabelFont.Size);
            bw.Write(d.holeToolNumber);
        }

        private static void Write(BinaryWriter bw, PolyLine pl)
        {
            bw.Write(TYPE_POLYLINE);
            bw.Write(pl.pos);
            bw.Write(pl.toolNumber);
            bw.Write(pl.radius);
            bw.Write((byte)pl.points.Count);
            foreach (var point in pl.points)
            {
                bw.Write(point);
            }
        }

        private static void Write(BinaryWriter bw, RectangularPocket rp)
        {
            bw.Write(TYPE_RECTPOCKET);
            bw.Write(rp.pos);
            bw.Write(rp.toolNumber);
            bw.Write(rp.width);
            bw.Write(rp.height);
            bw.Write(rp.depth);
        }

        private static void Write(BinaryWriter bw, Text txt)
        {
            bw.Write(TYPE_TEXT);
            bw.Write(txt.pos);
            bw.Write(txt.toolNumber);
            bw.Write(txt.text);
            bw.Write(txt.font.Size);
            bw.Write((byte)txt.anchor);
        }
        #endregion
    }

    public static class BinaryProjectFileExtensions
    {
        public static Vertex3 ReadVertex3(this BinaryReader br)
        {
            Vertex3 v;
            v.x = br.ReadSingle();
            v.y = br.ReadSingle();
            v.z = br.ReadSingle();
            return v;
        }
        public static Vertex2 ReadVertex2(this BinaryReader br)
        {
            Vertex2 v;
            v.x = br.ReadSingle();
            v.y = br.ReadSingle();
            return v;
        }
        public static void Write(this BinaryWriter bw,  Vertex3 v)
        {
            bw.Write(v.x);
            bw.Write(v.y);
            bw.Write(v.z);
        }
        public static void Write(this BinaryWriter bw, Vertex2 v)
        {
            bw.Write(v.x);
            bw.Write(v.y);
        }
    }
}
