using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace NittyGritty.Collections
{
    public class AlphaNumericGroupCollection<T> : Collection<Group<string, T>>, INotifyCollectionChanged
    {
        private const string Characters = "&#ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly Dictionary<string, Group<string, T>> _groups;

        public AlphaNumericGroupCollection(IEnumerable<T> items)
        {
            _groups = new Dictionary<string, Group<string, T>>();
            var itemGroups = items
                .OrderBy(i => i.ToString())
                .GroupBy(i => GetFirstCharacter(i.ToString()))
                .ToDictionary(i => i.Key);

            foreach (var alpha in Characters)
            {
                var title = alpha.ToString();
                var group = itemGroups.ContainsKey(title)
                    ? new Group<string, T>(title, itemGroups[title].ToList())
                    : new Group<string, T>(title);

                _groups.Add(title, group);
                Add(group);
            }

            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void AddRange(IEnumerable<T> items)
        {
            var changedGroups = new List<Group<string, T>>();

            foreach (var item in items)
            {
                var group = AddItemToCorrectGroup(item, true);
                if (!changedGroups.Contains(group))
                {
                    changedGroups.Add(group);
                    RaiseGroupChanged(group);
                }
            }
        }

        public void Add(T item)
        {
            var group = AddItemToCorrectGroup(item, true);
            RaiseGroupChanged(group);
        }

        public void Remove(T item)
        {
            var group = RemoveItemFromCorrectGroup(item);
            RaiseGroupChanged(group);
        }

        private static string GetFirstCharacter(string name)
        {
            var firstCharacter = name.Length > 0 ? name.Substring(0, 1).ToUpper() : "&";
            if (!Characters.Contains(firstCharacter))
            {
                if(char.IsDigit(firstCharacter, 0))
                {
                    return "#";
                }

                switch (firstCharacter)
                {
                    case "é": return "E";
                    case "è": return "E";
                    case "ê": return "E";
                    case "à": return "A";
                    case "â": return "A";
                    case "ü": return "U";
                    case "ä": return "A";
                    case "ö": return "O";
                    case "î": return "I";
                    default: return "&";
                }
            }
            return firstCharacter;
        }

        private Group<string, T> AddItemToCorrectGroup(T item, bool searchPosition)
        {
            var name = item.ToString();
            var group = _groups[GetFirstCharacter(name)];

            if (searchPosition && group.Items.Count > 0)
            {
                var newTitle = item.ToString();
                var newIndex = 0;
                foreach (var i in group.Items)
                {
                    if (i.ToString().CompareTo(newTitle) > 0)
                        break;
                    newIndex++;
                }
                group.Items.Insert(newIndex, item);
            }
            else
            {
                group.Items.Add(item);
            }

            return group;
        }

        private Group<string, T> RemoveItemFromCorrectGroup(T item)
        {
            var name = item.ToString();
            var group = _groups[GetFirstCharacter(name)];

            group.Items.Remove(item);
            return group;
        }

        private void RaiseGroupChanged(Group<string, T> group)
        {
            var index = IndexOf(group);
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, group, group, index));
        }

    }
}
