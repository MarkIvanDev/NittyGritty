using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Storage
{
    public enum CreationCollisionOption
    {
        GenerateUniqueName = 0,
        ReplaceExisting = 1,
        FailIfExists = 2,
        OpenIfExists = 3
    }
}
