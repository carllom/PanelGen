using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace PanelGen.Cli
{
    /// <summary>
    /// PanelGen project file
    /// Contains panel component data and tool parameters
    /// </summary>
    public class PanelGenProject : IPanelGenFileObject
    {
        public PanelStock Stock { get; set; } = new PanelStock();
        public List<Tool> Tools { get; set; } = new List<Tool>();

        private const string Marker = @"PanelGen";

        public void Load(BinaryReader br)
        {
            var marker = br.ReadString();
            if (marker != Marker)
                throw new Exception("Unknown file format");
            var rev = br.ReadInt16();
            if (rev == 0x100)
            {
                // Stock components
                Stock = new PanelStock();
                Stock.Load(br);

                // Tools
                var nTools = br.ReadByte();
                Tools.Clear();
                for (int i = 0; i < nTools; i++)
                {
                    var t = new Tool();
                    t.Load(br);
                    Tools.Add(t);
                }
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

            // Stock components
            Stock.Save(bw);

            // Tools
            bw.Write((byte)Tools.Count);
            foreach(var tool in Tools)
            {
                tool.Save(bw);
            }
        }

        public void ReadXml(XmlElement elem)
        {
            throw new NotImplementedException();
        }

        public XmlElement AsXml(XmlDocument doc)
        {
            var root = doc.CreateElement("PanelGenProject");
            root.SetAttribute("version", "100");
            doc.AppendChild(root);

            // Stock components
            var stockElem = Stock.AsXml(doc);
            root.AppendChild(stockElem);


            // Tools
            var tools = doc.CreateElement("Tools");
            foreach (var tool in Tools)
            {
                tools.AppendChild(tool.AsXml(doc));
            }
            root.AppendChild(tools);

            return root;
        }
    }
}
