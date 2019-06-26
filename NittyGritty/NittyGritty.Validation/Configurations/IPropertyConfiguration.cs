using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Validation.Configurations
{
    public interface IPropertyConfiguration
    {
        Type PropertyType { get; }
    }
}
