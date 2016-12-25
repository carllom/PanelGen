using System;
using System.Collections.Generic;

namespace PanelGen.Cli
{
    public class PanelStock : IDrawableItem
    {
        public float width = 80;
        public float height = 80;
        public float thickness = 1;
        public float x;
        public float y;

        public ICollection<PanelStockItem> items = new List<PanelStockItem>();

        public void Draw(IDraw drw)
        {
            drw.MoveTo(x, y);
            drw.LineTo(x, y + height);
            drw.LineTo(x + width, y + height);
            drw.LineTo(x + width, y);
            drw.LineTo(x, y);
        }
    }

    public class PanelStockItem
    {
        public float x;
        public float y;
    }
}
