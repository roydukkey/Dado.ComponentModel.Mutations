# System.ComponentModel.Mutations

Provides attributes that are used to help ensure data integrity for objects used as data sources.

MyGet Dev Package: http://www.myget.org/gallery/roydukkey


## Sample Usage

The following class shows that a property should be converted to lowercase and replaced of invalid characters.

```csharp
using System.Text.RegularExpressions;

public class User
{
	private string _userName;

	public string UserName {
		get {
			return _userName;
		}
		set {
			value = value.ToLower();

			_userName = Regex.Replace(value, @"[^a-z0-9._]", String.Empty);
		}
	}
}
```

Altering the example to use a mutation attributes will produce the following code.

```csharp
using System.ComponentModel.DataMutations;

public class User
{
	[
		ToLower,
		RegexReplace(@"[^a-z0-9._]")
	]
	public string UserName { get; set; }
}
```

Now, when `Mutator.Mutate(context);` is executed the value of `User.UserName` will be lowercased and have all invalid characters replaced.

[API Documentation](Documentation/README.md#documentation-index)


## Further Development

This project is still initial development. APIs have been submitted to [dotnet/corefx#7660](https://www.github.com/dotnet/corefx/issues/7660) for review and are likely to change.

Unit testing will be provided after a stable RTM has been published for .NET Core.


## License

System.ComponentModel.Mutations is licensed under the [MIT license](LICENSE).
