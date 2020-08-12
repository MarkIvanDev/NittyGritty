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
    }
}
