using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Validation.Configurations
{
    public interface IPropertyConfiguration
    {
        Type OwnerType { get; }

        Type PropertyType { get; }

        Collection<string> Triggers { get; }

        ObservableCollection<string> Errors { get; }

        Task ValidateAsync(object owner);
    }

    public interface IPropertyConfiguration<TOwner, TProperty> : IPropertyConfiguration
    {
        Func<TOwner, TProperty> PropertyFunc { get; }

        Collection<Validator<TProperty>> Validators { get; }

        Task ValidateAsync(TOwner owner);
    }
}
