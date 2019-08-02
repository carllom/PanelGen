using System.IO;
using System.Xml;

namespace PanelGen.Cli
{
    public interface IPanelGenFileObject
    {
        void Save(BinaryWriter bw);
        void Load(BinaryReader br);

        XmlElement AsXml(XmlDocument doc);
        void ReadXml(XmlElement elem);
    }
}
