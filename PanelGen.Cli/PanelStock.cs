using System;
using System.Collections.Generic;
using System.IO;

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
    }

    public abstract class PanelStockItem : PanelComponent
    {
        public byte toolNumber;

        public abstract void GenerateCode(TextWriter writer, Tool tool);
        public virtual bool UsesTool(int toolNumber) { return toolNumber == this.toolNumber; }
        public abstract PanelStockItem Clone();
    }
}
