using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Uwp.Activation.Operations
{
    public enum FileOperationStrategy
    {
        /// <summary>
        /// Use a single payload to contain all files
        /// </summary>
        Single = 0,

        /// <summary>
        /// Group files by file type
        /// </summary>
        Group = 1,

        /// <summary>
        /// A unique payload for each file
        /// </summary>
        Unqiue = 3
    }
}
