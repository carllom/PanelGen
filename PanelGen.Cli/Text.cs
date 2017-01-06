using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelGen.Cli
{
    public enum Alignment { Left, Center, Right}
    class Text : PanelStockItem
    {
        public string text;
        public HersheyFont font;
        public Alignment anchor;

        public void Draw(IDraw drw)
        {
            var w = 0f;
            if (anchor != Alignment.Right)
                w = -font.InnerWidth(text);
            if (anchor == Alignment.Center)
                w /= 2;

            font.DrawString(drw, text, x + w, x);
        }

        public override void Load(BinaryReader data)
        {
            base.Load(data);
        }

        public override void Save(BinaryWriter data)
        {
            base.Save(data);
        }
    }
}
