using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

namespace NittyGritty.UI.Converters
{
    [ContentProperty(Name = nameof(Bindings))]
    public abstract class AsyncMultiConverter<T> : DependencyObject
    {
        public AsyncMultiConverter()
        {
            Bindings = new ObservableCollection<Binding>();
        }

        public ObservableCollection<Binding> Bindings
        {
            get { return (ObservableCollection<Binding>)GetValue(BindingsProperty); }
            set { SetValue(BindingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Bindings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingsProperty =
            DependencyProperty.Register("Bindings", typeof(ObservableCollection<Binding>), typeof(AsyncMultiConverter<T>), new PropertyMetadata(null, OnBindingsChanged));

        private static void OnBindingsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AsyncMultiConverter<T> converter)
            {
                if (e.OldValue is ObservableCollection<Binding> oldValue)
                {
                    oldValue.CollectionChanged -= converter.Bindings_CollectionChanged;
                    foreach (var item in oldValue)
                    {
                        item.ValueChanged -= converter.Binding_ValueChanged;
                    }
                }

                if (e.NewValue is ObservableCollection<Binding> newValue)
                {
                    newValue.CollectionChanged += converter.Bindings_CollectionChanged;
                    foreach (var item in newValue)
                    {
                        item.ValueChanged += converter.Binding_ValueChanged;
                    }
                }
            }
        }

        private async void Binding_ValueChanged(object sender, PropertyChangedEventArgs e)
        {
            Output = await Convert();
        }

        private void Bindings_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is Binding binding)
                    {
                        binding.ValueChanged -= Binding_ValueChanged;
                    }
                }
            }

            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is Binding binding)
                    {
                        binding.ValueChanged += Binding_ValueChanged;
                    }
                }
            }
        }

        public T Output
        {
            get { return (T)GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Output.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutputProperty =
            DependencyProperty.Register("Output", typeof(T), typeof(AsyncMultiConverter<T>), new PropertyMetadata(default(T)));

        public abstract Task<T> Convert();
    }
}
