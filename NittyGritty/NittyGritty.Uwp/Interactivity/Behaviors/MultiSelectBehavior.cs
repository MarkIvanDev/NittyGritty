using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NittyGritty.Uwp.Interactivity.Behaviors
{
    public class MultiSelectBehavior<T> : Behavior<ListViewBase>
    {
        public MultiSelectBehavior()
        {
            SelectedItems = new ObservableCollection<T>();
        }

        public ObservableCollection<T> SelectedItems
        {
            get { return (ObservableCollection<T>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems",
                typeof(ObservableCollection<T>),
                typeof(MultiSelectBehavior<T>),
                new PropertyMetadata(null, SelectedItemsChanged));

        private bool _selectionChangedInProgress;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
            if (SelectedItems != null) SelectedItems.CollectionChanged -= SelectedItemsChanged;
        }

        private static void SelectedItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            MultiSelectBehavior<T> msbSender = sender as MultiSelectBehavior<T>;
            msbSender.WireNewSelectedItems(args.OldValue as ObservableCollection<T>, args.NewValue as ObservableCollection<T>);
        }

        private void WireNewSelectedItems(ObservableCollection<T> oldValue, ObservableCollection<T> newValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= SelectedItemsChanged;
            }

            if (newValue != null)
            {
                var listViewBase = AssociatedObject;
                if (listViewBase != null)
                {
                    var listSelectedItems = listViewBase.SelectedItems;
                    bool bOldInProgress = _selectionChangedInProgress;
                    _selectionChangedInProgress = true;   // don't notify, as we're in control

                    listSelectedItems.Clear();
                    foreach (var v in newValue)
                    {
                        listSelectedItems.Add(v);
                    }

                    _selectionChangedInProgress = bOldInProgress;
                }
                // wire up notifications on the new collection
                newValue.CollectionChanged += SelectedItemsChanged;
            }

        }

        private void SelectedItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var listViewBase = AssociatedObject;

            var listSelectedItems = listViewBase.SelectedItems;
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                listSelectedItems.Clear();
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (var item in e.OldItems)
                    {
                        if (listSelectedItems.Contains(item))
                        {
                            listSelectedItems.Remove(item);
                        }
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (var item in e.NewItems)
                    {
                        if (!listSelectedItems.Contains(item))
                        {
                            listSelectedItems.Add(item);
                        }
                    }
                }
            }
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_selectionChangedInProgress) return;
            _selectionChangedInProgress = true;

            foreach (T item in e.RemovedItems)
            {
                if (SelectedItems.Contains(item))
                {
                    SelectedItems.Remove(item);
                }
            }

            foreach (T item in e.AddedItems)
            {
                if (!SelectedItems.Contains(item))
                {
                    SelectedItems.Add(item);
                }
            }
            _selectionChangedInProgress = false;

        }
    }
}
