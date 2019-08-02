using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace PanelGen.Cli
{
    public class PanelStock : PanelComponent
    {
        public float width = 80;
        public float height = 80;
        public float thickness = 1;

        public override Vertex3 Extents => new Vertex3(width, height, thickness);
        public override bool Inside(float x, float y)
        {
            var p = new Vertex2(x, y) - pos.Xy;
            return Math.Abs(x) <= width / 2 && Math.Abs(y) <= height / 2;
        }

        public ICollection<PanelStockItem> items = new List<PanelStockItem>();

        public override void Load(BinaryReader data)
        {
            base.Load(data);
            width = data.ReadSingle();
            height = data.ReadSingle();
            thickness = data.ReadSingle();
            var numItems = data.ReadInt32();
            items.Clear();
            for (int i = 0; i < numItems; i++)
            {
                var obj = ReadObject(data) as PanelStockItem;
                if (obj == null)
                    System.Diagnostics.Debug.WriteLine($"Got object that was not a panelstockitem");
                else
                    items.Add(obj);
            }
        }

        public override void Save(BinaryWriter data)
        {
            base.Save(data);
            data.Write(width);
            data.Write(height);
            data.Write(thickness);
            data.Write(items.Count);
            foreach (var item in items)
            {
                WriteObject(data, item);
            }
        }

        public override XmlElement AsXml(XmlDocument doc)
        {
            var panel = doc.CreateElement("Panel");
            panel.AppendChild(base.AsXml(doc));
            var dimensions = doc.CreateElement("Dimensions");
            dimensions.SetAttribute("width", width.ToString(CultureInfo.InvariantCulture));
            dimensions.SetAttribute("height", width.ToString(CultureInfo.InvariantCulture));
            dimensions.SetAttribute("thickness", width.ToString(CultureInfo.InvariantCulture));
            panel.AppendChild(dimensions);
            var itemRoot = doc.CreateElement("Items");
            foreach (var item in items)
            {
                itemRoot.AppendChild(item.AsXml(doc));
            }
            panel.AppendChild(itemRoot);
            return panel;
        }
    }

    public abstract class PanelStockItem : PanelComponent
    {
        public byte toolNumber;

        public abstract void GenerateCode(TextWriter writer, Tool tool);
        public virtual bool UsesTool(int toolNumber) { return toolNumber == this.toolNumber; }
        public abstract PanelStockItem Clone();

        public override void Load(BinaryReader data)
        {
            base.Load(data);
            toolNumber = data.ReadByte();
        }

        public override void Save(BinaryWriter data)
        {
            base.Save(data);
            data.Write(toolNumber);
        }

        public override XmlElement AsXml(XmlDocument doc)
        {
            var stock = doc.CreateElement("Stock");
            stock.AppendChild(base.AsXml(doc));
            stock.SetAttribute("tool", toolNumber.ToString(CultureInfo.InvariantCulture));
            return stock;
        }
    }
}
