# ToUpperAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutate the specified string to a uppercase.

**Namespace:** Dado.ComponentModel.DataMutations<br />
**Implements:** Dado.ComponentModel.DataMutations.MutationAttribute (in Dado.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class ToUpperAttribute : MutationAttribute
```


### Properties

| Name | Description |
| ---- | ----------- |
| [CultureInfo](#CultureInfo) | Gets or sets the CultureInfo to be used when determining the appropriate case. |
| [Priority](#Priority) | Gets or sets a value indicating the priority that determines the order in which MutationAttributes are evaluated. |
| [RequiresContext](MutationAttribute.md#RequiresContext) | A flag indicating the attribute requires a non-null MutationContext&lt;T&gt; to perform validation. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this ToUpperAttribute. |


<a name='CultureInfo'></a>
## CultureInfo

Gets or sets the *CultureInfo* to be used when determining the appropriate case.

#### Syntax

```csharp
public CultureInfo CultureInfo { get; set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Globalization.CultureInfo</dd>
</dl>


<a name='Priority'></a>
## Priority

Gets or sets a value indicating the priority that determines the order in which *MutationAttributes* are evaluated. Defaults to `30`.

#### Syntax

```csharp
public override int Priority { get; set; } = 30;
```

<dl>
	<dt>Type</dt>
	<dd>System.Integer</dd>
</dl>


<a name='MutateValueObjectIMutationContext'></a>
## MutateValue(Object, IMutationContext)

Implements the mutation logic for this *ToUpperAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

The specified `value` converted to uppercase.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: Dado.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
