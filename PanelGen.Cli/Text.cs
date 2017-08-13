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

            font = new HersheyFont(FontFace.RomanSimplex);
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

        public override PanelStockItem Clone()
        {
            var copy = new Text(text)
            {
                pos = pos,
                toolNumber = toolNumber,
                anchor = anchor,
                font = {Size = font.Size } // Font is created by constructor (for now)
            };
            return copy;
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
            font.Size = data.ReadSingle();
            anchor = (Alignment)data.ReadByte();
        }

        public override void Save(BinaryWriter data)
        {
            base.Save(data);
            data.Write(text);
            data.Write(font.Size);
            data.Write((byte)anchor);
        }
    }
}
