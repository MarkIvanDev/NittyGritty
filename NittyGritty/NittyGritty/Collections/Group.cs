using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NittyGritty.Collections
{
    public class Group<T> : IGrouping<string, T>
    {
        private readonly IList<T> groupList;

        public Group(string key, IEnumerable<T> groupList)
        {
            Key = key;
            this.groupList = groupList.ToList();
            Count = this.groupList.Count;
        }

        public string Key { get; }

        public int Count { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return groupList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return groupList.GetEnumerator();
        }
    }
}
