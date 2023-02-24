using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Talysoft.IO
{
    public static class Compression
    {
        /// <summary>
        /// Compresses a given byte array.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>The bytes, compressed.</returns>
        //https://stackoverflow.com/questions/39191950/how-to-compress-a-byte-array-without-stream-or-system-io
        public static byte[] Compress(byte[] data)
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(data, 0, data.Length);
            }
            return output.ToArray();
        }

        /// <summary>
        /// Decompresses a given compressed byte array.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>The bytes, decompressed.</returns>
        //https://stackoverflow.com/questions/39191950/how-to-compress-a-byte-array-without-stream-or-system-io
        public static byte[] Decompress(byte[] data)
        {
            MemoryStream input = new MemoryStream(data);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            return output.ToArray();
        }
    }
}
