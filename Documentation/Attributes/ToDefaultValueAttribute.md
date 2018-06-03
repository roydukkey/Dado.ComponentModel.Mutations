# ToDefaultValueAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutate the specified values to the associated *DefaultValueAttribute.Value* or the type's default value.

**Namespace:** Dado.ComponentModel.DataMutations<br />
**Implements:** Dado.ComponentModel.DataMutations.MutationAttribute (in Dado.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class ToDefaultValueAttribute : MutationAttribute
```

#### Remarks

The *ToDefaultValueAttribute* replaces any values that are specified in *Values* with the associated *DefaultValueAttribute.Value* or the type's default value.

If no *Values* are specified, the type's default value will be replaced with the associated *DefaultValueAttribute.Value*.


### Constructors

| Name | Description |
| ---- | ----------- |
| [ToDefaultValueAttribute(Object[])](#ToDefaultValueAttributeObjectArray) | Initializes a new instance of the ToDefaultValueAttribute class. |


### Properties

| Name | Description |
| ---- | ----------- |
| [Priority](#Priority) | Gets or sets a value indicating the priority that determines the order in which MutationAttributes are evaluated. |
| [RequiresContext](MutationAttribute.md#RequiresContext) | A flag indicating the attribute requires a non-null MutationContext&lt;T&gt; to perform validation. |
| [Values](#Values) | Gets the values to make default. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [Mutate(Object, Object, IMutationContext)](#MutateObjectObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this ToDefaultValueAttribute. |


<a name='ToDefaultValueAttributeObjectArray'></a>
## ToDefaultValueAttribute(Object[])

Initializes a new instance of the *ToDefaultValueAttribute* class.

#### Syntax

```csharp
public ToDefaultValueAttribute(
	params object[] values
)
```

#### Parameters

<dl>
	<dt>values</dt>
	<dd>Type: System.Object[]<br />An array of values that should be made default.</dd>
</dl>


<a name='Priority'></a>
## Priority

Gets or sets a value indicating the priority that determines the order in which *MutationAttributes* are evaluated. Defaults to `50`.

#### Syntax

```csharp
public override int Priority { get; set; } = 50;
```

<dl>
	<dt>Type</dt>
	<dd>System.Int32</dd>
</dl>


<a name='Values'></a>
## Values

Gets the values to make default.

#### Syntax

```csharp
public IEnumerable<object> Values { get; private set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Collections.Generic.IEnumerable&lt;System.Object&gt;</dd>
</dl>


<a name='MutateObjectObjectIMutationContext'></a>
## Mutate(Object, Object, IMutationContext)

Mutates the given value according to this *MutationAttribute*.

#### Syntax

```csharp
public object Mutate(
	object value,
	object defaultValue,
	IMutationContext context = null
)
```

#### Returns

The resulting mutated value.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>defaultValue</dt>
	<dd>Type: System.Object<br />The value to be used instead of the type's default value.</dd>
	<dt>context</dt>
	<dd>Type: Dado.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is required and **null**. |


<a name='MutateValueObjectIMutationContext'></a>
## MutateValue(Object, IMutationContext)

Implements the mutation logic for this *ToDefaultValueAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

The type's default value when the specified `value` is in *Values*.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: Dado.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
