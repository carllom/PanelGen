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

        public Text(string text)
        {
            this.text = text;

            font = new HersheyFont(@"C:\Projekt\PanelGen\tool\hershey");
            anchor = Alignment.Center;
        }

        public override Vertex3 Extents
        {
            get
            {
                ExtentsRenderer xr = new ExtentsRenderer();
                Draw(xr);
                return xr.Extents;
            }
        }

        public override bool Inside(float x, float y)
        {
            ExtentsRenderer xr = new ExtentsRenderer();
            Draw(xr);
            return xr.Inside(x, y);
        }


        public void Draw(IDraw drw)
        {
            var align = 0f;
            if (anchor != Alignment.Right)
                align = -font.InnerWidth(text);
            if (anchor == Alignment.Center)
                align /= 2;

            font.DrawString(drw, text, pos.x + align, pos.x);
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
