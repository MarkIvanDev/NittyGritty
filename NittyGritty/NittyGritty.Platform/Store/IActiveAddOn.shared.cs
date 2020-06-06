using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.Platform.Store
{
    public interface IActiveAddOn
    {
        bool IsActive { get; set; }
    }
}
