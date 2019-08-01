using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Uwp.Platform
{
    public class StreamFileProcessor : FileProcessor<Stream>
    {
        public StreamFileProcessor() : base("*")
        {
        }
        
        public override async Task<Stream> Process(IStorageFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            
            var stream = await file.OpenReadAsync();
            return stream.AsStream();
        }

    }
}
