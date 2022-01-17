# Nitty Gritty Source Generators
This package includes the following source generators:

### Property Generator
Generate *observable* properties by annotating your fields inside a class with `[Notify]`. The class should have `ObservableObject` as its base class. You can also annotate your field with `[AlsoNotify]` to also raise the property changed event for other properties whenever the generated property is set.


You can customize the generated property by using the following properties of `NotifyAttribute`:  
- **Name** (Change the name of the generated property)
- **Getter** (Change the accessibility level of the property's getter)
- **Setter** (Change the accessibility level of the property's setter)

Example:

```csharp
using NittyGritty;
using NittyGritty.SourceGenerators.Attributes;

public partial class Student : ObservableObject
{
    [Notify]
    [AlsoNotify(Name = "FullName")]
    public string _firstName;

    [Notify(Name = "LastName")]
    [AlsoNotify(Name = "FullName")]
    public string _surname;

    public string  FullName => $"{FirstName} {LastName}";
}
```

This will generate:

```csharp
public partial class Student
{
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            Set(ref _firstName, value);
            RaisePropertyChanged("FullName");
        }
    }

    public string LastName
    {
        get { return _surname; }
        set
        {
            Set(ref _surname, value);
            RaisePropertyChanged("FullName");
        }
    }
}
```

---

### Command Generator
Generate commands by annotating your methods with `[Command]`. The generator will rely on the method signature to generate the command:

- Return type is `void` => `RelayCommand`
- Return type is `Task` => `AsyncRelayCommand`
- Other return types => not supported
- No parameters => Non-generic Command
- One parameter => Generic Command with the parameter type as the type argument
- Two parameters or more => not supported

You can customize the generated command by using the following property of `CommandAttribute`:  
- **Name** (Change the name of the generated command)

Example:

```csharp
using System.Threading.Tasks;
using NittyGritty.ViewModels;
using NittyGritty.SourceGenerators.Attributes;

public partial class HomeViewModel : ViewModelBase
{
    [Command]
    public void Load()
    {
        // ...
    }

    [Command]
    public async Task LoadAsync()
    {
        // ...
    }
}
```

This will generate:

```csharp
using NittyGritty.Commands;

public partial class HomeViewModel
{
    
    private RelayCommand _Load;
    public RelayCommand LoadCommand => _Load ?? (_Load = new RelayCommand(
    	() =>
    	{
    		Load();
    	}
    	));

    
    private AsyncRelayCommand _LoadAsync;
    public AsyncRelayCommand LoadAsyncCommand => _LoadAsync ?? (_LoadAsync = new AsyncRelayCommand(
    	async () =>
    	{
    		await LoadAsync();
    	}
    	));

}
```

---

### ViewModelKeys Generator
Generate a `ViewModelKeys` class for all of your `[ViewModelKey]` annotated view models to use for `NavigationService` and more.

You can customize the generated view model key by using the following property of `ViewModelKeyAttribute`:  
- **Key** (Change the key of the generated view model key)

You can also customize the namespace of the generated `ViewModelKeys` class by using the MSBuild property `NG_ViewModelKeysNamespace` in your .csproj file.
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <NG_ViewModelKeysNamespace>NittyGritty.Sample</NG_ViewModelKeysNamespace>
  </PropertyGroup>
</Project>
```

Example:

```csharp
using NittyGritty.ViewModels;
using NittyGritty.SourceGenerators.Attributes;

[ViewModelKey]
public class HomeViewModel : ViewModelBase { /* ... */ }

[ViewModelKey(Key = "NormalName")]
public class WeirdNameViewModel : ViewModelBase { /* ... */ }
```

This will generate:

```csharp
namespace NittyGritty.Generated
{
	public static class ViewModelKeys
	{
		public static string Home { get; } = nameof(Home);
		public static string NormalName { get; } = nameof(NormalName);
	}
}
```

---

### DialogKeys Generator
Generate a `DialogKeys` class for all of your `[DialogKey]` annotated view models to use for `DialogService` and more.

You can customize the generated dialog key by using the following property of `DialogKeyAttribute`:  
- **Key** (Change the key of the generated dialog key)

You can also customize the namespace of the generated `DialogKeys` class by using the MSBuild property `NG_DialogKeysNamespace` in your .csproj file.
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <NG_DialogKeysNamespace>NittyGritty.Sample</NG_DialogKeysNamespace>
  </PropertyGroup>
</Project>
```

Example:

```csharp
using NittyGritty;
using NittyGritty.SourceGenerators.Attributes;

[DialogKey]
public class ConfirmDeleteViewModel : ObservableObject { /* ... */ }

[DialogKey(Key = "NormalName")]
public class WeirdNameViewModel : ObservableObject { /* ... */ }
```

This will generate:

```csharp
namespace NittyGritty.Generated
{
	public static class DialogKeys
	{
		public static string ConfirmDelete { get; } = nameof(ConfirmDelete);
		public static string NormalName { get; } = nameof(NormalName);
	}
}
```

---

##### NOTES
Don't forget to make your classes partial when using the `[Notify]` and `[Command]` attributes. If the generated source code is not being updated even after doing a clean and rebuild, try restarting Visual Studio.