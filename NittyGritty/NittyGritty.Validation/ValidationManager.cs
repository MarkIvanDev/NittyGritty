using NittyGritty.Utilities;
using NittyGritty.Validation.Configurations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NittyGritty.Validation
{
    public class ValidationManager<T> : ObservableObject where T : class, INotifyPropertyChanged
    {
        public ValidationManager(T context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public T Context { get; }

        public Dictionary<string, IPropertyConfiguration> Configurations { get; } = new Dictionary<string, IPropertyConfiguration>();

        #region 

        /// <summary>
        /// Start listening to property changes of the context
        /// </summary>
        public void Start()
        {
            Context.PropertyChanged += Context_PropertyChanged;

        }

        private async void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(Configurations.TryGetValue(e.PropertyName, out var configuration))
            {
                await configuration.ValidateAsync(Context);
            }

            var triggers = Configurations.Values.Where(c => c.Triggers.Contains(e.PropertyName));
            foreach (var trigger in triggers)
            {
                await trigger.ValidateAsync(Context);
            }
            RaisePropertyChanged(nameof(Configurations));
        }

        /// <summary>
        /// Stop listening to property changes of the context
        /// </summary>
        public void Stop()
        {
            Context.PropertyChanged -= Context_PropertyChanged;
        }

        /// <summary>
        /// Validate the context using the configurations made
        /// </summary>
        /// <returns></returns>
        public async Task Validate()
        {
            foreach (var config in Configurations)
            {
                await config.Value.ValidateAsync(Context);
            }
        }

        #endregion

        #region Configure Properties

        public BoolPropertyConfiguration<T> ConfigureProperty(Expression<Func<T, bool>> property)
        {
            var propertyName = ExpressionUtilities.GetPropertyName(property);
            if(TryGetConfiguration<BoolPropertyConfiguration<T>>(propertyName, out var config))
            {
                return config;
            }
            else
            {
                var c = new BoolPropertyConfiguration<T>(property.Compile());
                Configurations.Add(propertyName, c);
                return c;
            }
        }

        public CharPropertyConfiguration<T> ConfigureProperty(Expression<Func<T, char>> property)
        {
            var propertyName = ExpressionUtilities.GetPropertyName(property);
            if (TryGetConfiguration<CharPropertyConfiguration<T>>(propertyName, out var config))
            {
                return config;
            }
            else
            {
                var c = new CharPropertyConfiguration<T>(property.Compile());
                Configurations.Add(propertyName, c);
                return c;
            }
        }

        public DateTimePropertyConfiguration<T> ConfigureProperty(Expression<Func<T, DateTime>> property)
        {
            var propertyName = ExpressionUtilities.GetPropertyName(property);
            if (TryGetConfiguration<DateTimePropertyConfiguration<T>>(propertyName, out var config))
            {
                return config;
            }
            else
            {
                var c = new DateTimePropertyConfiguration<T>(property.Compile());
                Configurations.Add(propertyName, c);
                return c;
            }
        }

        public DateTimeOffsetPropertyConfiguration<T> ConfigureProperty(Expression<Func<T, DateTimeOffset>> property)
        {
            var propertyName = ExpressionUtilities.GetPropertyName(property);
            if (TryGetConfiguration<DateTimeOffsetPropertyConfiguration<T>>(propertyName, out var config))
            {
                return config;
            }
            else
            {
                var c = new DateTimeOffsetPropertyConfiguration<T>(property.Compile());
                Configurations.Add(propertyName, c);
                return c;
            }
        }

        public EntityPropertyConfiguration<T, TProperty> ConfigureEntityProperty<TProperty>(Expression<Func<T, TProperty>> property)
            where TProperty : class, INotifyPropertyChanged
        {

            throw new NotImplementedException();
        }

        public NumericPropertyConfiguration<T, TProperty> ConfigureProperty<TProperty>(Expression<Func<T, TProperty>> property)
            where TProperty : struct, IComparable, IComparable<TProperty>, IConvertible, IEquatable<TProperty>, IFormattable
        {
            var propertyName = ExpressionUtilities.GetPropertyName(property);
            if (TryGetConfiguration<NumericPropertyConfiguration<T, TProperty>>(propertyName, out var config))
            {
                return config;
            }
            else
            {
                var c = new NumericPropertyConfiguration<T, TProperty>(property.Compile());
                Configurations.Add(propertyName, c);
                return c;
            }
        }

        #region Numeric
        //public NumericPropertyConfiguration<T, byte> ConfigureProperty(Expression<Func<T, byte>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, byte>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, decimal> ConfigureProperty(Expression<Func<T, decimal>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, decimal>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, double> ConfigureProperty(Expression<Func<T, double>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, double>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, float> ConfigureProperty(Expression<Func<T, float>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, float>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, int> ConfigureProperty(Expression<Func<T, int>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, int>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, long> ConfigureProperty(Expression<Func<T, long>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, long>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, sbyte> ConfigureProperty(Expression<Func<T, sbyte>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, sbyte>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, short> ConfigureProperty(Expression<Func<T, short>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, short>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, uint> ConfigureProperty(Expression<Func<T, uint>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, uint>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, ulong> ConfigureProperty(Expression<Func<T, ulong>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, ulong>>(propertyName);
        //}

        //public NumericPropertyConfiguration<T, ushort> ConfigureProperty(Expression<Func<T, ushort>> property)
        //{
        //    var propertyName = ExpressionUtilities.GetPropertyName(property);
        //    return GetConfiguration<NumericPropertyConfiguration<T, ushort>>(propertyName);
        //}

        #endregion

        public StringPropertyConfiguration<T> ConfigureProperty(Expression<Func<T, string>> property)
        {
            var propertyName = ExpressionUtilities.GetPropertyName(property);
            if (TryGetConfiguration<StringPropertyConfiguration<T>>(propertyName, out var config))
            {
                return config;
            }
            else
            {
                var c = new StringPropertyConfiguration<T>(property.Compile());
                Configurations.Add(propertyName, c);
                return c;
            }
        }

        #endregion

        private bool TryGetConfiguration<TConfig>(string propertyName, out TConfig config)
            where TConfig : class, IPropertyConfiguration
        {
            if (Configurations.ContainsKey(propertyName))
            {
                config = Configurations[propertyName] as TConfig;
                return true;
            }
            else
            {
                config = null;
                return false;
            }
        }
    }
}
