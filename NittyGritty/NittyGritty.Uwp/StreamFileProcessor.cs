using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Uwp
{
    public class StreamFileProcessor : FileProcessor<Stream>
    {
        public StreamFileProcessor(string fileType) : base(fileType)
        {

        }

        public override async Task<Stream> Process(IStorageFile file)
        {
            var stream = await file.OpenReadAsync();
            return stream.AsStream();
        }
    }
}
