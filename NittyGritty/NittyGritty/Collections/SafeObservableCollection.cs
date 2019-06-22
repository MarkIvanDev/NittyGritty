using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NittyGritty.Collections
{
    public class SafeObservableCollection<T> : ObservableCollection<T>
    {
        public SafeObservableCollection() : base()
        {
        }

        public SafeObservableCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        /// <summary>Occurs when a property value changes. 
        /// This is the same event as on the <see cref="ObservableCollection{T}"/> except that it is public. </summary>
        public new event PropertyChangedEventHandler PropertyChanged
        {
            add { base.PropertyChanged += value; }
            remove { base.PropertyChanged -= value; }
        }

        /// <summary>Adds multiple items to the collection. </summary>
        /// <param name="collection">The items to add. </param>
        /// <exception cref="ArgumentNullException">The value of 'collection' cannot be null. </exception>
        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
                Items.Add(item);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection.ToList()));
        }

        /// <summary>Removes multiple items from the collection. </summary>
        /// <param name="collection">The items to remove. </param>
        /// <exception cref="ArgumentNullException">The value of 'collection' cannot be null. </exception>
        public void RemoveRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection.ToList())
                Items.Remove(item);

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, collection.ToList()));
        }

        /// <summary>Resets the whole collection with a given list. </summary>
        /// <param name="collection">The collection. </param>
        /// <exception cref="ArgumentNullException">The value of 'collection' cannot be null. </exception>
        public void Reset(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            Items.Clear();
            foreach (var i in collection)
                Items.Add(i);

            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

    }
}
