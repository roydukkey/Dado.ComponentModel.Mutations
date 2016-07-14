# RegexReplaceAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutate a string replacing all strings that match a regular expression pattern with a specified replacement string.

**Namespace:** System.ComponentModel.DataMutations<br />
**Implements:** System.ComponentModel.DataMutations.MutationAttribute (in System.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class RegexReplaceAttribute : MutationAttribute
```

#### Remarks

If *Replacement* is **null**, each match of the specified *Patterns* are removed.

The *Replacement* property specifies the string that is to replace each match in the current value. *Replacement* can consist of any combination of literal text and substitutions. For example, the replacement pattern, `*${test}b`, inserts the string, "a\*", followed by the substring that is matched by the test capturing group, if any, followed by the string, "b". An asterisk (\*) is not recognized as a metacharacter within a replacement pattern.


### Constructors

| Name | Description |
| ---- | ----------- |
| [RegexReplaceAttribute(String, String[])](#RegexReplaceAttributeStringStringArray) | Initializes a new instance of the RegexReplaceAttribute class. |


### Properties

| Name | Description |
| ---- | ----------- |
| [Options](#Options) | Gets or sets a bitwise combination of the enumeration values that modify the regular expression. |
| [Patterns](#Patterns) | Gets the regular expression pattern to match in a string. |
| [Priority](MutationAttribute.md#Priority) | Gets or sets a value indicating the priority that determines the order in which MutationAttributes are evaluated. |
| [Replacement](#Replacement) | Gets or sets the replacement pattern that will be used to replace each match of the specified Patterns. |
| [RequiresContext](MutationAttribute.md#RequiresContext) | A flag indicating the attribute requires a non-null MutationContext&lt;T&gt; to perform validation. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this RegexReplaceAttribute. |


<a name='RegexReplaceAttributeStringStringArray'></a>
## RegexReplaceAttribute(String, String[])

Initializes a new instance of the *RegexReplaceAttribute* class.

#### Syntax

```csharp
public RegexReplaceAttribute(
	string pattern,
	params string[] additional
)
```

#### Parameters

<dl>
	<dt>pattern</dt>
	<dd>Type: System.String<br />The regular expression pattern to match.</dd>
	<dt>additional</dt>
	<dd>Type: System.String[]<br />Additional regular expression pattern to match.</dd>
</dl>


<a name='Options'></a>
## Options

Gets or sets a bitwise combination of the enumeration values that modify the regular expression.

#### Syntax

```csharp
public RegexOptions Options { get; set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Text.RegularExpressions.RegexOptions</dd>
</dl>


<a name='Patterns'></a>
## Patterns

Gets the regular expression pattern to match in a string.

#### Syntax

```csharp
public IEnumerable<string> Patterns { get; private set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Collections.Generic.IEnumerable&lt;System.String&gt;</dd>
</dl>


<a name='Replacement'></a>
## Replacement

Gets or sets the replacement pattern that will be used to replace each match of the specified *Patterns*.

#### Syntax

```csharp
public string Replacement { get; set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.String</dd>
</dl>


<a name='MutateValueObjectIMutationContext'></a>
## MutateValue(Object, IMutationContext)

Implements the mutation logic for this *RegexReplaceAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
