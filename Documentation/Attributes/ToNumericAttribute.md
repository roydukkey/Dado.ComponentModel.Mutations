# ToNumericAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutate a string to allow only numeric characters.

**Namespace:** Dado.ComponentModel.DataMutations<br />
**Implements:** Dado.ComponentModel.DataMutations.MutationAttribute (in Dado.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class ToNumericAttribute : MutationAttribute
```


### Properties

| Name | Description |
| ---- | ----------- |
| [PreserveFloatingPoint](#PreserveFloatingPoint) | Gets or sets a value indicating whether a floating point indication (.) should be preserved during mutation. |
| [PreserveSign](#PreserveSign) | Gets or sets a value indicating whether a sign indication (±) should be preserved during mutation. |
| [Priority](#Priority) | Gets or sets a value indicating the priority that determines the order in which MutationAttributes are evaluated. |
| [RequiresContext](MutationAttribute.md#RequiresContext) | A flag indicating the attribute requires a non-null MutationContext&lt;T&gt; to perform validation. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this ToNumericAttribute. |


<a name='PreserveFloatingPoint'></a>
## PreserveFloatingPoint

Gets or sets a value indicating whether a floating point indication (.) should be preserved during mutation.

#### Syntax

```csharp
public bool PreserveFloatingPoint { get; set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Boolean</dd>
</dl>

<a name='PreserveSign'></a>
## PreserveSign

Gets or sets a value indicating whether a sign indication (±) should be preserved during mutation.

#### Syntax

```csharp
public bool PreserveSign { get; set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Boolean</dd>
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


<a name='MutateValueObjectIMutationContext'></a>
## MutateValue(Object, IMutationContext)

Implements the mutation logic for this *ToNumericAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

The resulting mutated value in the specified numeric format.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: Dado.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
