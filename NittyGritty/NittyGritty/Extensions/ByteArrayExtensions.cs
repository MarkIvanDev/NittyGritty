using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NittyGritty.Extensions
{
    public static class ByteArrayExtensions
    {
        public static MemoryStream ToMemoryStream(this byte[] byteArray)
        {
            if (byteArray is null)
            {
                return null;
            }

            var memoryStream = new MemoryStream();
            memoryStream.Write(byteArray, 0, byteArray.Length);
            return memoryStream;
        }

        public static byte[] ToByteArray(this Stream stream)
        {
            if (stream != null)
            {
                using (var memory = new MemoryStream())
                {
                    stream.Position = 0;
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
            return null;
        }
    }
}
