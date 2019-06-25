using NittyGritty.Validation.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace NittyGritty.Validation
{
    public class ValidationManager<T> where T : class, INotifyPropertyChanged
    {
        private readonly T context;
        private Dictionary<string, IPropertyConfiguration> _configurations = new Dictionary<string, IPropertyConfiguration>();

        public ValidationManager(T context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region 

        /// <summary>
        /// Start listening to property changes of the context
        /// </summary>
        public void Start()
        {
            context.PropertyChanged += Context_PropertyChanged;
        }

        private void Context_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Stop listening to property changes of the context
        /// </summary>
        public void Stop()
        {
            context.PropertyChanged -= Context_PropertyChanged;
        }

        /// <summary>
        /// Validate the context using the configurations made
        /// </summary>
        /// <returns></returns>
        public async Task Validate()
        {

        }

        #endregion

        #region Configure Properties

        public BoolPropertyConfiguration ConfigureProperty(Func<T, bool> property)
        {
            
            throw new NotImplementedException();
        }

        public BytePropertyConfiguration ConfigureProperty(Func<T, byte> property)
        {

            throw new NotImplementedException();
        }

        public CharPropertyConfiguration ConfigureProperty(Func<T, char> property)
        {

            throw new NotImplementedException();
        }

        public DecimalPropertyConfiguration ConfigureProperty(Func<T, decimal> property)
        {

            throw new NotImplementedException();
        }

        public DoublePropertyConfiguration ConfigureProperty(Func<T, double> property)
        {

            throw new NotImplementedException();
        }

        public EntityPropertyConfiguration<TProperty> ConfigureProperty<TProperty>(Func<T, TProperty> property) where TProperty : class
        {

            throw new NotImplementedException();
        }

        public FloatPropertyConfiguration ConfigureProperty(Func<T, float> property)
        {

            throw new NotImplementedException();
        }

        public IntPropertyConfiguration ConfigureProperty(Func<T, int> property)
        {

            throw new NotImplementedException();
        }

        public LongPropertyConfiguration ConfigureProperty(Func<T, long> property)
        {

            throw new NotImplementedException();
        }

        public SBytePropertyConfiguration ConfigureProperty(Func<T, sbyte> property)
        {

            throw new NotImplementedException();
        }

        public ShortPropertyConfiguration ConfigureProperty(Func<T, short> property)
        {

            throw new NotImplementedException();
        }

        public StringPropertyConfiguration ConfigureProperty(Func<T, string> property)
        {

            throw new NotImplementedException();
        }

        public UIntPropertyConfiguration ConfigureProperty(Func<T, uint> property)
        {

            throw new NotImplementedException();
        }

        public ULongPropertyConfiguration ConfigureProperty(Func<T, ulong> property)
        {

            throw new NotImplementedException();
        }

        public UShortPropertyConfiguration ConfigureProperty(Func<T, ushort> property)
        {

            throw new NotImplementedException();
        }

        #endregion
    }
}
