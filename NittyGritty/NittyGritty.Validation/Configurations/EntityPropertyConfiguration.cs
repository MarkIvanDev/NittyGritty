using NittyGritty.Validation.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace NittyGritty.Validation.Configurations
{
    public class EntityPropertyConfiguration<TOwner, TProperty> : BasePropertyConfiguration<TOwner, TProperty, EntityPropertyConfiguration<TOwner, TProperty>>, IEntityPropertyContract
        where TOwner : class
        where TProperty : class, INotifyPropertyChanged
    {
        public EntityPropertyConfiguration(Func<TOwner, TProperty> propertyFunc) : base(propertyFunc)
        {
            ValidationManager = new ValidationManager<TProperty>();
        }

        public ValidationManager<TProperty> ValidationManager { get; }

        public override async Task ValidateAsync(TOwner owner)
        {
            await base.ValidateAsync(owner);

            await ValidationManager.Validate();
        }
    }
}
