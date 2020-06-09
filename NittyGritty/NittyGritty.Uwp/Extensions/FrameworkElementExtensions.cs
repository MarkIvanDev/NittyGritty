using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace NittyGritty.Uwp.Extensions
{
    public static class FrameworkElementExtensions
    {


        public static double GetActualHeight(FrameworkElement obj)
        {
            return (double)obj.GetValue(ActualHeightProperty);
        }

        public static void SetActualHeight(FrameworkElement obj, double value)
        {
            obj.SetValue(ActualHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for ActualHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActualHeightProperty =
            DependencyProperty.RegisterAttached("ActualHeight", typeof(double), typeof(FrameworkElementExtensions), new PropertyMetadata(double.NaN));



        public static double GetActualWidth(FrameworkElement obj)
        {
            return (double)obj.GetValue(ActualWidthProperty);
        }

        public static void SetActualWidth(FrameworkElement obj, double value)
        {
            obj.SetValue(ActualWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for ActualWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActualWidthProperty =
            DependencyProperty.RegisterAttached("ActualWidth", typeof(double), typeof(FrameworkElementExtensions), new PropertyMetadata(double.NaN));



        public static bool GetEnableActualSizeBinding(FrameworkElement obj)
        {
            return (bool)obj.GetValue(EnableActualSizeBindingProperty);
        }

        public static void SetEnableActualSizeBinding(FrameworkElement obj, bool value)
        {
            obj.SetValue(EnableActualSizeBindingProperty, value);
        }

        // Using a DependencyProperty as the backing store for EnableActualSizeBinding.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableActualSizeBindingProperty =
            DependencyProperty.RegisterAttached("EnableActualSizeBinding", typeof(bool), typeof(FrameworkElementExtensions), new PropertyMetadata(false, OnEnableActualSizeBindingChanged));

        private static void OnEnableActualSizeBindingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            if (element is null)
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                UpdateActualSize(element);
                element.SizeChanged += OnFrameworkElementSizeChanged;
            }
            else
            {
                element.SizeChanged -= OnFrameworkElementSizeChanged;
            }
        }

        private static void OnFrameworkElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                UpdateActualSize(element);
            }
        }

        private static void UpdateActualSize(FrameworkElement element)
        {
            var currentHeight = GetActualHeight(element);
            if (currentHeight != element.ActualHeight)
            {
                SetActualHeight(element, element.ActualHeight);
            }

            var currentWidth = GetActualWidth(element);
            if (currentWidth != element.ActualWidth)
            {
                SetActualWidth(element, element.ActualWidth);
            }
        }
    }
}
