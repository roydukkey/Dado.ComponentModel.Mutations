# ReplaceAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutate a string so all occurrences of a specified strings are replaced with another specified string.

**Namespace:** Dado.ComponentModel.DataMutations<br />
**Implements:** Dado.ComponentModel.DataMutations.MutationAttribute (in Dado.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class ReplaceAttribute : MutationAttribute
```

#### Remarks

If *Replacement* is **null**, all occurrences of the specified *Antecedents* are removed.

This attribute performs an ordinal (case-sensitive and culture-insensitive) search to find the specified *Antecedents*.


### Constructors

| Name | Description |
| ---- | ----------- |
| [ReplaceAttribute(String)](#ReplaceAttributeString) | Initializes a new instance of the ReplaceAttribute class. |
| [ReplaceAttribute(String[])](#ReplaceAttributeStringArray) | Initializes a new instance of the ReplaceAttribute class. |


### Properties

| Name | Description |
| ---- | ----------- |
| [Antecedents](#Antecedents) | Gets the values to be replaced in a string. |
| [Priority](#Priority) | Gets or sets a value indicating the priority that determines the order in which MutationAttributes are evaluated. |
| [Replacement](#Replacement) | Gets or sets the string to replace all occurrences of the specified Antecedents. |
| [RequiresContext](MutationAttribute.md#RequiresContext) | A flag indicating the attribute requires a non-null MutationContext&lt;T&gt; to perform validation. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this ReplaceAttribute. |


<a name='ReplaceAttributeString'></a>
## ReplaceAttribute(String)

Initializes a new instance of the *ReplaceAttribute* class.

#### Syntax

```csharp
public ReplaceAttribute(
	string antecedent
)
```

#### Parameters

<dl>
	<dt>antecedent</dt>
	<dd>Type: System.String<br />The string to be replaced.</dd>
</dl>


<a name='ReplaceAttributeStringArray'></a>
## ReplaceAttribute(String[])

Initializes a new instance of the *ReplaceAttribute* class.

#### Syntax

```csharp
public ReplaceAttribute(
	params string[] antecedents
)
```

#### Parameters

<dl>
	<dt>antecedents</dt>
	<dd>Type: System.String[]<br />The strings to be replaced.</dd>
</dl>


<a name='Antecedents'></a>
## Antecedents

Gets the values to be replaced in a string.

#### Syntax

```csharp
public IEnumerable<string> Antecedents { get; private set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Collections.Generic.IEnumerable&lt;System.String&gt;</dd>
</dl>


<a name='Priority'></a>
## Priority

Gets or sets a value indicating the priority that determines the order in which *MutationAttributes* are evaluated. Defaults to `20`.

#### Syntax

```csharp
public override int Priority { get; set; } = 20;
```

<dl>
	<dt>Type</dt>
	<dd>System.Int32</dd>
</dl>


<a name='Replacement'></a>
## Replacement

Gets or sets the string to replace all occurrences of the specified *Antecedents*.

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

Implements the mutation logic for this *ReplaceAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

A string that is equivalent to the current `value` except that all instances of specified *Antecedents* are replaced with the value of *Replacement*. If none of the *Antecedents* are found in the current `value`, the method returns the current `value` unchanged.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: Dado.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
