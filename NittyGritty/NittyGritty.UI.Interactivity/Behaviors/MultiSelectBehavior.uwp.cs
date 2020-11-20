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

namespace NittyGritty.UI.Interactivity.Behaviors
{
    public class MultiSelectBehavior<TCollection, TItem> : Behavior<ListViewBase> where TCollection : class, IList<TItem>, INotifyCollectionChanged
    {
        private bool selectionChangeInProgress;

        protected override void OnAttached()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            }
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectionChangeInProgress) return;
            selectionChangeInProgress = true;

            foreach (TItem item in e.RemovedItems)
            {
                if (SelectedItems != null && SelectedItems.Contains(item))
                {
                    SelectedItems.Remove(item);
                }
            }

            foreach (TItem item in e.AddedItems)
            {
                if (SelectedItems != null && !SelectedItems.Contains(item))
                {
                    SelectedItems.Add(item);
                }
            }

            selectionChangeInProgress = false;
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
            }
        }


        public TCollection SelectedItems
        {
            get { return (TCollection)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(TCollection), typeof(MultiSelectBehavior<TCollection, TItem>), new PropertyMetadata(null, OnSelectedItemsChanged));

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MultiSelectBehavior<TCollection, TItem> behavior)
            {
                behavior.UpdateSelectedItems(e.OldValue as TCollection, e.NewValue as TCollection);
            }
        }

        private void UpdateSelectedItems(TCollection oldCollection, TCollection newCollection)
        {
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= SelectedItems_CollectionChanged;
            }

            if (newCollection != null)
            {
                bool oldSelectionChangeInProgress = selectionChangeInProgress;
                selectionChangeInProgress = true;   // don't notify, as we're in control

                switch (AssociatedObject.SelectionMode)
                {
                    case ListViewSelectionMode.Single:
                        AssociatedObject.SelectedItem = newCollection.FirstOrDefault();
                        break;

                    case ListViewSelectionMode.Multiple:
                    case ListViewSelectionMode.Extended:
                        AssociatedObject.SelectedItems.Clear();
                        foreach (var item in newCollection)
                        {
                            AssociatedObject.SelectedItems.Add(item);
                        }
                        break;

                    case ListViewSelectionMode.None:
                    default:
                        break;
                }
                selectionChangeInProgress = oldSelectionChangeInProgress;

                newCollection.CollectionChanged += SelectedItems_CollectionChanged;
            }
        }

        private void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (AssociatedObject is null) return;
            switch (AssociatedObject.SelectionMode)
            {
                case ListViewSelectionMode.Single:
                    if (e.Action == NotifyCollectionChangedAction.Reset)
                    {
                        AssociatedObject.SelectedItem = null;
                    }
                    else
                    {
                        AssociatedObject.SelectedItem = e.NewItems != null && e.NewItems.Count > 0 ? e.NewItems[0] : null;
                    }
                    break;

                case ListViewSelectionMode.Multiple:
                case ListViewSelectionMode.Extended:
                    var listSelectedItems = AssociatedObject.SelectedItems;
                    if (e.Action == NotifyCollectionChangedAction.Reset)
                    {
                        listSelectedItems.Clear();
                    }
                    else
                    {
                        foreach (var item in e.OldItems ?? Enumerable.Empty<TItem>().ToList())
                        {
                            if (listSelectedItems.Contains(item))
                            {
                                listSelectedItems.Remove(item);
                            }
                        }

                        foreach (var item in e.NewItems ?? Enumerable.Empty<TItem>().ToList())
                        {
                            if (!listSelectedItems.Contains(item))
                            {
                                listSelectedItems.Add(item);
                            }
                        }
                    }
                    break;

                case ListViewSelectionMode.None:
                default:
                    break;
            }
        }
        
    }
}
