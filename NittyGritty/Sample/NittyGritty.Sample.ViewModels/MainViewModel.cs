using System;
using System.Collections.Generic;
using System.Linq;
using NittyGritty.Collections;
using NittyGritty.Commands;
using NittyGritty.ViewModels;

namespace NittyGritty.Sample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            
        }

        private string _addText = string.Empty;

        public string AddText
        {
            get { return _addText; }
            set
            {
                Set(ref _addText, value);
            }
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

        /// <summary>
        /// Gets the AddCommand.
        /// </summary>
        public RelayCommand AddCommand => new RelayCommand(
            () =>
            {
                if(!string.IsNullOrWhiteSpace(AddText))
                    DynamicCollection.Add(AddText);
            });

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
