# ToDefaultValueAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutated the specified values to the associated *DefaultValueAttribute.Value* or the type's default value.

**Namespace:** System.ComponentModel.DataMutations<br />
**Implements:** System.ComponentModel.DataMutations.MutationAttribute (in System.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class ToDefaultValueAttribute : MutationAttribute
```


### Constructors

| Name | Description |
| ---- | ----------- |
| [ToDefaultValueAttribute(Object, Object[])](#ToDefaultValueAttributeObjectObjectArray) | Initializes a new instance of the ToDefaultValueAttribute class. |


### Properties

| Name | Description |
| ---- | ----------- |
| [Values](#Values) | Gets the values to make default. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationContext&lt;T&gt;. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this ToDefaultValueAttribute. |


<a name='ToDefaultValueAttributeObjectObjectArray'></a>
## ToDefaultValueAttribute(String, String[])

Initializes a new instance of the *ToDefaultValueAttribute* class.

#### Syntax

```csharp
public ToDefaultValueAttribute(
	object value,
	params object[] additional
)
```

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value that should be made default.</dd>
	<dt>additional</dt>
	<dd>Type: System.Object[]<br />Additional values to make default.</dd>
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
	<dd>Type: System.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
