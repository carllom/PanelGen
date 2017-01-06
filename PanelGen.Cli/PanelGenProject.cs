using System;
using System.IO;

namespace PanelGen.Cli
{
    public class PanelGenProject : IPanelGenFileObject
    {
        public PanelStock Stock { get; set; } = new PanelStock();

        private const string Marker = @"PanelGen";

        public void Load(BinaryReader br)
        {
            var marker = br.ReadString();
            if (marker != Marker)
                throw new Exception("Unknown file format");
            var rev = br.ReadInt16();
            if (rev == 0x100)
            {
                Stock = new PanelStock();
                Stock.Load(br);
            }
        }

        public void Save(BinaryWriter bw)
        {
            if (Stock == null)
            {
                return;
            }

            bw.Write(Marker); // Marker
            bw.Write((short)0x0100); // Version (major,minor)
            Stock.Save(bw);
        }
    }
}
