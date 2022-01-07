using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Storage
{
    public enum RenameOption
    {
        GenerateUniqueName = 0,
        ReplaceExisting = 1,
        FailIfExists = 2
    }
}
