using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talysoft.IO;

namespace Talysoft
{
    public struct Coordinate2D : ISaveable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Serialize(BinaryWriter writer, uint version)
        {
            writer.Write(X);
            writer.Write(Y);
        }

        public void Deserialize(BinaryReader reader, uint version)
        {
            X = reader.ReadInt32();
            Y = reader.ReadInt32();
        }
    }
}
