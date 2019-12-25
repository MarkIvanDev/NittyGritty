using System;
using System.Collections.Generic;
using System.Linq;
using NittyGritty.Collections;
using NittyGritty.ViewModels;

namespace NittyGritty.Sample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            
        }

        private string _Text = string.Empty;

        public string Text
        {
            get { return _Text; }
            set
            {
                Set(ref _Text, value);
                if (DynamicCollection != null)
                {
                    DynamicCollection.Filter = c => c.IndexOf(value ?? string.Empty, StringComparison.OrdinalIgnoreCase) != -1;
                }
            }
        }

        private DynamicCollection<string> _dynamicCollection;

        public DynamicCollection<string> DynamicCollection
        {
            get { return _dynamicCollection; }
            set { Set(ref _dynamicCollection, value); }
        }

        public override void LoadState(object parameter, Dictionary<string, object> state)
        {
            DynamicCollection = new DynamicCollection<string>(Enumerable.Range(1, 10).Select(i => i.ToString()));
        }

        public override void SaveState(Dictionary<string, object> state)
        {
            
        }
    }
}
