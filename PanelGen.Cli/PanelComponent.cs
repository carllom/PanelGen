using System;
using System.IO;

namespace PanelGen.Cli
{
    public abstract class PanelComponent : IPanelGenFileObject
    {
        public float x;
        public float y;

        public virtual void Load(BinaryReader data)
        {
            x = data.ReadSingle();
            y = data.ReadSingle();
        }

        public virtual void Save(BinaryWriter data)
        {
            data.Write(x);
            data.Write(y);
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
