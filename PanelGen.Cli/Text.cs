using System.IO;

namespace PanelGen.Cli
{
    public enum Alignment { Left, Center, Right}
    public class Text : PanelStockItem
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

            font.DrawString(drw, text, pos.x + align, pos.y);
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
            text = data.ReadString();
        }

        public override void Save(BinaryWriter data)
        {
            base.Save(data);
            data.Write(text);
        }
    }
}
