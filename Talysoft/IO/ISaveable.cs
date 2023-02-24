using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Talysoft.IO
{
    /// <summary>
    /// Provides an interface for saving and loading binary data.
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// Serializes this object to the BinaryWriter.
        /// </summary>
        /// <param name="writer">The BinaryWriter to write the data to.</param>
        /// <param name="version">The version of the save data.</param>
        void Serialize(BinaryWriter writer, uint version);

        /// <summary>
        /// Deserializes the given data in the BinaryReader into this object.
        /// </summary>
        /// <param name="reader">The BinaryReader that is reading from the data to be used.</param>
        /// <param name="version">The version of the save data.</param>
        void Deserialize(BinaryReader reader, uint version);
    }
}
