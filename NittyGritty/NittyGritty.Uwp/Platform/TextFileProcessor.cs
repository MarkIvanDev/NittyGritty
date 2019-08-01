using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace NittyGritty.Uwp.Platform
{
    public class TextFileProcessor : FileProcessor<string>
    {
        public TextFileProcessor() : base("*")
        {

        }

        public override async Task<string> Process(IStorageFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var text = await FileIO.ReadTextAsync(file);
            return text;
        }
    }
}
