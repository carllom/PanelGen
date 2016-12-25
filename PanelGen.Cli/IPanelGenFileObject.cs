using System.IO;

namespace PanelGen.Cli
{
    public interface IPanelGenFileObject
    {
        void Save(BinaryWriter bw);
        void Load(BinaryReader br);
    }
}
