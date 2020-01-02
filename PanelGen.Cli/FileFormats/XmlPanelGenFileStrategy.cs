using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace PanelGen.Cli.FileFormats
{
    class XmlPanelGenFileStrategy : IPanelGenFileStrategy
    {
        public PanelGenProject Read(Stream s)
        {
            throw new NotImplementedException();
        }

        public void Write(Stream s, PanelGenProject pgp)
        {
            var doc = new XmlDocument();
            Write(doc, pgp);
            doc.Save(s);
        }

        private XmlElement Write(XmlDocument doc, PanelGenProject pgp)
        {
            var root = doc.CreateElement("PanelGenProject");
            root.SetAttribute("version", "100");
            doc.AppendChild(root);

            // Stock components
            root.AppendChild(Write(doc, pgp.Stock));

            // Tools
            var tools = doc.CreateElement("Tools");
            foreach (var tool in pgp.Tools)
            {
                tools.AppendChild(Write(doc, tool));
            }
            root.AppendChild(tools);

            return root;
        }

        private XmlElement Write(XmlDocument doc, PanelStock ps)
        {
            var panel = doc.CreateElement("Panel");
            panel.AppendChild(doc.CreateElement("Position", ps.pos));
            var dimensions = doc.CreateElement("Dimensions");
            dimensions.SetAttribute("width", ps.width);
            dimensions.SetAttribute("height", ps.height);
            dimensions.SetAttribute("thickness", ps.thickness);
            panel.AppendChild(dimensions);
            var itemRoot = doc.CreateElement("Items");
            foreach (var item in ps.items)
            {
                XmlElement e = null;
                switch (item)
                {
                    case Dial d:
                        e = Write(doc, d);
                        break;
                    case CircularPocket cp:
                        e = Write(doc, cp);
                        break;
                    case RectangularPocket rp:
                        e = Write(doc, rp);
                        break;
                    case Text t:
                        e = Write(doc, t);
                        break;
                    case PolyLine pl:
                        e = Write(doc, pl);
                        break;
                    default:
                        throw new Exception($"Save not supported for component type {item.GetType().Name}");
                }

                // Common attributes for all stockpanelitems
                e.SetAttribute("tool", item.toolNumber);
                e.AppendChild(doc.CreateElement("Position", item.pos));

                itemRoot.AppendChild(e);
            }
            panel.AppendChild(itemRoot);
            return panel;
        }

        #region PanelStockItems
        private XmlElement Write(XmlDocument doc, CircularPocket cp)
        {
            var pocket = doc.CreateElement("CircularPocket");
            pocket.SetAttribute("diameter", cp.diameter);
            pocket.SetAttribute("depth", cp.depth);
            var stepList = doc.CreateElement("Steps");
            foreach (var step in cp.steps)
            {
                XmlElement stepElem = doc.CreateElement("Step");
                stepElem.SetAttribute("diameter", step.diameter);
                stepElem.SetAttribute("depth", step.depth);
                stepList.AppendChild(stepElem);
            }
            pocket.AppendChild(stepList);
            return pocket;
        }

        private XmlElement Write(XmlDocument doc, Dial d)
        {
            var dial = doc.CreateElement("CircularPocket");
            var hole = doc.CreateElement("Hole");
            hole.SetAttribute("radius", d.holeRadius);
            hole.SetAttribute("depth", d.holeDepth);
            hole.SetAttribute("tool", d.holeToolNumber);
            dial.AppendChild(hole);
            dial.SetAttribute("innerRadius", d.innerRadius);
            dial.SetAttribute("arcSpan",d.arcSpan);
            var markers = doc.CreateElement("Markers");
            markers.SetAttribute("length", d.markerLength);
            markers.SetAttribute("minValue", d.minValue);
            markers.SetAttribute("maxValue", d.maxValue);
            markers.SetAttribute("step", d.step);
            markers.SetAttribute("labelOffset", d.markerLabelOffset);
            markers.SetAttribute("fontSize", d.MarkerFont.Size);
            dial.AppendChild(markers);
            var ticks = doc.CreateElement("Ticks");
            ticks.SetAttribute("length", d.tickLength);
            ticks.SetAttribute("count", d.tickCount);
            dial.AppendChild(ticks);
            var label = doc.CreateElement("Label");
            label.SetAttribute("fontSize", d.LabelFont.Size);
            label.AppendChild(doc.CreateTextNode(d.text));
            dial.AppendChild(label);
            return dial;
        }

        private XmlElement Write(XmlDocument doc, PolyLine pl)
        {
            var poly = doc.CreateElement("PolyLine");
            poly.SetAttribute("radius", pl.radius);
            var points = doc.CreateElement("Points");
            foreach (var point in pl.points)
            {
                points.AppendChild(doc.CreateElement("Point", point));
            }
            poly.AppendChild(points);
            return poly;
        }

        private XmlElement Write(XmlDocument doc, RectangularPocket rp)
        {
            var pocket = doc.CreateElement("RectangularPocket");
            pocket.SetAttribute("width", rp.width);
            pocket.SetAttribute("height", rp.height);
            pocket.SetAttribute("depth", rp.depth);
            return pocket;
        }

        private XmlElement Write(XmlDocument doc, Text txt)
        {
            var text = doc.CreateElement("Text");
            text.SetAttribute("fontSize", txt.font.Size);
            text.SetAttribute("anchor", (byte)txt.anchor);
            text.AppendChild(doc.CreateTextNode(txt.text));
            return text;
        }
        #endregion

        private XmlElement Write(XmlDocument doc, Tool t)
        {
            var tool = doc.CreateElement("Tool");
            tool.SetAttribute("number", t.number.ToString());
            tool.SetAttribute("diameter", t.diameter.ToString(CultureInfo.InvariantCulture));
            tool.SetAttribute("zStep", t.zStep.ToString(CultureInfo.InvariantCulture));
            return tool;
        }
    }

    public static class XmlProjectFileExtensions
    {
        //public static Vertex3 ReadVertex3(this BinaryReader br)
        //{
        //    Vertex3 v;
        //    v.x = br.ReadSingle();
        //    v.y = br.ReadSingle();
        //    v.z = br.ReadSingle();
        //    return v;
        //}
        //public static Vertex2 ReadVertex2(this BinaryReader br)
        //{
        //    Vertex2 v;
        //    v.x = br.ReadSingle();
        //    v.y = br.ReadSingle();
        //    return v;
        //}
        public static XmlElement CreateElement(this XmlDocument bw, string name, Vertex3 v)
        {
            var elem = bw.CreateElement(name);
            elem.SetAttribute("x", v.x.ToString(CultureInfo.InvariantCulture));
            elem.SetAttribute("y", v.y.ToString(CultureInfo.InvariantCulture));
            elem.SetAttribute("z", v.z.ToString(CultureInfo.InvariantCulture));
            return elem;
        }
        public static XmlElement CreateElement(this XmlDocument bw, string name, Vertex2 v)
        {
            var elem = bw.CreateElement(name);
            elem.SetAttribute("x", v.x);
            elem.SetAttribute("y", v.y);
            return elem;
        }

        public static void SetAttribute(this XmlElement elem, string name, float value)
        {
            elem.SetAttribute(name, value.ToString(CultureInfo.InvariantCulture));
        }

        public static void SetAttribute(this XmlElement elem, string name, int value)
        {
            elem.SetAttribute(name, value.ToString(CultureInfo.InvariantCulture));
        }
    }

}
