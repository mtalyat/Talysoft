using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Talysoft.IO
{
    public interface ISaveable
    {
        void Serialize(BinaryWriter writer, uint version);
        void Deserialize(BinaryReader reader, uint version);
    }
}
