using System.Collections.Generic;
using System.IO;

namespace PanelGen.Cli
{
    public class PanelStock : PanelComponent, IDrawableItem
    {
        public float width = 80;
        public float height = 80;
        public float thickness = 1;

        public ICollection<PanelStockItem> items = new List<PanelStockItem>();

        public void Draw(IDraw drw)
        {
            drw.MoveTo(x, y);
            drw.LineTo(x, y + height);
            drw.LineTo(x + width, y + height);
            drw.LineTo(x + width, y);
            drw.LineTo(x, y);
        }

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
    }

    public class PanelStockItem : PanelComponent
    {
    }
}
