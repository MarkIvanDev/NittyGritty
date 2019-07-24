using NittyGritty.Models;
using System;
using System.Collections.Generic;

namespace NittyGritty.Uwp.Operations
{
    internal class PathConfiguration
    {
        public PathConfiguration(string path, Type view, Predicate<QueryString> createsNewView)
        {
            Path = path;
            View = view;
            CreatesNewView = createsNewView ?? (createsNewView = (q) => false);
        }

        public PathConfiguration(string path, Type view) : this(path, view, (uri) => false)
        {

        }

        public string Path { get; }

        public Type View { get; }

        public Predicate<QueryString> CreatesNewView { get; }
    }
}
