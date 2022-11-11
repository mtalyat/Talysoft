using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Talysoft.IO
{
    public static class Save
    {
        public static uint Version { get; set; } = 0;

        /// <summary>
        /// Saves the given ISaveable object to the disc at the given file path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="filepath"></param>
        public static void SaveObject<T>(T t, string filepath) where T : ISaveable
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Create))
            {
                BinaryWriter writer = new BinaryWriter(fs);

                writer.Write(Version);

                t.Serialize(writer, Version);
            }
        }
        
        /// <summary>
        /// Loads the ISaveable object on the disc from the given file path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static T LoadObject<T>(string filepath) where T : ISaveable, new()
        {
            if(!File.Exists(filepath))
            {
                //file does not exist
                return default;
            }

            T t;

            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                BinaryReader reader = new BinaryReader(fs);

                uint version = reader.ReadUInt32();

                t = new T();
                t.Deserialize(reader, version);
            }

            return t;
        }
    }
}
