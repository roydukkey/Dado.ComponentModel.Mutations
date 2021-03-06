# TrimAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutate a specified string in which all leading and/or trailing occurrences of a set of specified characters are removed.

**Namespace:** Dado.ComponentModel.DataMutations<br />
**Implements:** Dado.ComponentModel.DataMutations.MutationAttribute (in Dado.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class TrimAttribute : MutationAttribute
```

#### Remarks

The Trim attribute removes from the specified string all leading and/or trailing characters that are specified in *Characters*. Each leading and trailing trim operation stops when a character that is not in *Characters* is encountered. For example, if the string is "123abc456xyz789" and *Characters* contains the digits from "1" through "9", the resulting string is "abc456xyz".

If the specified string equals *Empty* or all the characters in the string consist of characters in the *Characters* array, the resulting string is *Empty*.

If *Characters* is **null** or an empty array, mutation removes any leading or trailing characters that result in *Char.IsWhiteSpace(char)* returning **true** when the character is passed to the method.

### Constructors

| Name | Description |
| ---- | ----------- |
| [TrimAttribute(Char[])](#TrimAttributeCharArray) | Initializes a new instance of the TrimAttribute class. |


### Properties

| Name | Description |
| ---- | ----------- |
| [Characters](#Characters) | Gets an array of Unicode characters to remove. |
| [Direction](#Direction) | Gets or sets a value indicating the trimming direction. |
| [Priority](#Priority) | Gets or sets a value indicating the priority that determines the order in which MutationAttributes are evaluated. |
| [RequiresContext](MutationAttribute.md#RequiresContext) | A flag indicating the attribute requires a non-null MutationContext&lt;T&gt; to perform validation. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this TrimAttribute. |


<a name='TrimAttributeCharArray'></a>
## TrimAttribute(Char[])

Initializes a new instance of the *TrimAttribute* class.

#### Syntax

```csharp
public TrimAttribute(
	params char[] characters
)
```

#### Parameters

<dl>
	<dt>characters</dt>
	<dd>Type: System.Char[]<br />An array of Unicode characters to remove, or **null**.</dd>
</dl>


<a name='Characters'></a>
## Characters

Gets an array of Unicode characters to remove.

#### Syntax

```csharp
public char[] Characters { get; private set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Char[]</dd>
</dl>


<a name='Direction'></a>
## Direction

Gets or sets a value indicating the trimming direction.

#### Syntax

```csharp
public TrimOptions Direction { get; set; }
```

<dl>
	<dt>Type</dt>
	<dd>Dado.ComponentModel.DataMutations.TrimOptions</dd>
</dl>


<a name='Priority'></a>
## Priority

Gets or sets a value indicating the priority that determines the order in which *MutationAttributes* are evaluated. Defaults to `40`.

#### Syntax

```csharp
public override int Priority { get; set; } = 40;
```

<dl>
	<dt>Type</dt>
	<dd>System.Int32</dd>
</dl>


<a name='MutateValueObjectIMutationContext'></a>
## MutateValue(Object, IMutationContext)

Implements the mutation logic for this *TrimAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

The string that remains after all occurrences of the characters in the the *Characters* array are removed from the start and/or end of the specified string. If the *Characters* array is **null** or an empty array, white-space characters are removed instead.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: Dado.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
