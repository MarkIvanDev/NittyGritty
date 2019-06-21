using System;
using System.Collections.Generic;
using System.Text;

namespace NittyGritty.ViewModels
{
    public interface IStateManager
    {
        void LoadState(object parameter, Dictionary<string, object> state);

        void SaveState(Dictionary<string, object> state);

        T RestoreStateItem<T>(Dictionary<string, object> state, string stateKey, T defaultValue = default(T));
    }
}
