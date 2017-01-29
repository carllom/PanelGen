using System;
using System.IO;

namespace PanelGen.Cli
{
    public class Tool : IPanelGenFileObject
    {
        public int number;
        public float diameter; // Tool diameter
        public float zStep; // Max z-step when doing multipass holes
        public float radius => diameter/2;

        public override string ToString()
        {
            return $"#{number} [{diameter};{zStep}]";
        }

        public void Save(BinaryWriter bw)
        {
            bw.Write(number);
            bw.Write(diameter);
            bw.Write(zStep);
        }

        public void Load(BinaryReader br)
        {
            number = br.ReadInt32();
            diameter = br.ReadSingle();
            zStep = br.ReadSingle();
        }
    }
}
