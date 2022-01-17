using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NittyGritty.Sample.Sub;
using NittyGritty.SourceGenerators.Attributes;
using NittyGritty.ViewModels;

namespace NittyGritty.Sample
{
    public partial class Class1 : ViewModelBase
    {
        [Notify]
        [AlsoNotify(Name = nameof(FullName))]
        public int _id, _age;

        [Notify(Getter = AccessLevel.Internal, Setter = AccessLevel.Internal)]
        public int _id5;

        [Notify(Name = "FullName")]
        public readonly string _name;

        [Notify]
        public Thing _thing;

        [Command]
        public async Task Run()
        {
            
        }

        [Command(Name = "RunWithString")]
        public void Run(string text)
        {

        }

        [Command]
        public async Task RunAsync()
        {

        }

        [Command(Name = "RunWithNumber")]
        public void Run(int number)
        {

        }

        [Command(Name = "RunAsyncWithNumber")]
        public async Task RunAsync(int number)
        {

        }

        [Command(Name = "RunWithNullableNumber")]
        public void Run(int? number)
        {

        }

        public override void LoadState(object parameter, Dictionary<string, object> state)
        {
            throw new NotImplementedException();
        }

        public override void SaveState(Dictionary<string, object> state)
        {
            throw new NotImplementedException();
        }
    }

    [ViewModelKey]
    public class HomeViewModel { }

    [ViewModelKey(Key = "NormalName")]
    public class WeirdNameViewModel { }

    [ViewModelKey]
    public class SettingsViewModel { }

    [DialogKey]
    public class LoginViewModel { }

    [DialogKey]
    public class AddUserViewModel { }

    [DialogKey]
    public class RemoveUserDialog { }

    [DialogKey(Key = "AnotherNormalName")]
    public class AnotherWeirdNameViewModel { }

}
