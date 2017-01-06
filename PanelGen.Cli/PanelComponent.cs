using System;
using System.IO;

namespace PanelGen.Cli
{
    public struct Vertex3
    {
        public Vertex3 (float x, float y, float z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float x;
        public float y;
        public float z;
    }

    public abstract class PanelComponent : IPanelGenFileObject
    {
        public Vertex3 pos;

        public virtual void Load(BinaryReader data)
        {
            pos.x = data.ReadSingle();
            pos.y = data.ReadSingle();
            pos.z = data.ReadSingle();
        }

        public virtual void Save(BinaryWriter data)
        {
            data.Write(pos.x);
            data.Write(pos.y);
            data.Write(pos.z);
        }

        private const byte TYPE_DIAL = 1;
        private const byte TYPE_CIRCPOCKET = 2;
        private const byte TYPE_RECTPOCKET = 3;

        public static PanelComponent ReadObject(BinaryReader data)
        {
            var type = data.ReadByte();
            PanelComponent obj;
            switch (type)
            {
                case TYPE_DIAL:
                    obj = new Dial();
                    break;
                case TYPE_CIRCPOCKET:
                    obj = new CircularPocket();
                    break;
                case TYPE_RECTPOCKET:
                    obj = new RectangularPocket();
                    break;
                default:
                    throw new Exception($"Got unknown Panel component type {type}");
            }
            obj.Load(data);
            return obj;
        }

        public static void WriteObject(BinaryWriter data, PanelComponent obj)
        {
            if (obj is Dial)
                data.Write(TYPE_DIAL);
            else if (obj is CircularPocket)
                data.Write(TYPE_CIRCPOCKET);
            else if (obj is RectangularPocket)
                data.Write(TYPE_RECTPOCKET);
            obj.Save(data);
        }

    }
}
