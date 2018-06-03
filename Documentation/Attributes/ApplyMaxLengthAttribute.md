# ApplyMaxLengthAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutate a string data to the maximum allowable length according to the associated *StringLengthAttribute.MaximumLength* or *MaxLengthAttribute.Length*.

**Namespace:** Dado.ComponentModel.DataMutations<br />
**Implements:** Dado.ComponentModel.DataMutations.MutationAttribute (in Dado.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class ApplyMaxLengthAttribute : MutationAttribute
```

#### Remarks

Since *System.Attribute*s cannot accept generics or lambdas, there is not a practical way to alter the length of collections maintaining their type information. Therefore, *ApplyMaxLengthAttribute.CollectionDescriptor* can be used to extend *ApplyMaxLengthAttribute* through its protected constructor.

Expect *ApplyMaxLengthAttribute.CollectionDescriptor* to be obsolete once generics or lambdas are implemented for *System.Attribute*s.


### Constructors

| Name | Description |
| ---- | ----------- |
| [ApplyMaxLengthAttribute()](#ApplyMaxLengthAttribute) | Initializes a new instance of the ApplyMaxLengthAttribute class. |
| [ApplyMaxLengthAttribute(CollectionDescriptor)](#ApplyMaxLengthAttributeCollectionDescriptor) | Initializes a new instance of the ApplyMaxLengthAttribute class. |


### Properties

| Name | Description |
| ---- | ----------- |
| [Priority](MutationAttribute.md#Priority) | Gets or sets a value indicating the priority that determines the order in which MutationAttributes are evaluated. |
| [RequiresContext](#RequiresContext) | A flag indicating the attribute requires a non-null MutationContext&lt;T&gt; to perform validation. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [Mutate(Object, Int32)](#MutateObjectInt32) | Mutates the given value according to this MutationAttribute. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this ToDefaultValueAttribute. |


<a name='ApplyMaxLengthAttribute'></a>
## ApplyMaxLengthAttribute()

Initializes a new instance of the *ApplyMaxLengthAttribute* class.

#### Syntax

```csharp
public ApplyMaxLengthAttribute()
```


<a name='ApplyMaxLengthAttributeCollectionDescriptor'></a>
## ApplyMaxLengthAttribute(CollectionDescriptor)

Initializes a new instance of the *ApplyMaxLengthAttribute* class.

#### Syntax

```csharp
protected ApplyMaxLengthAttribute(
	Dado.ComponentModel.DataMutations.ApplyMaxLengthAttribute.CollectionDescriptor descriptor
)
```

#### Parameters

<dl>
	<dt>descriptor</dt>
	<dd>Type: ApplyMaxLengthAttribute.CollectionDescriptor<br />Describe a collection should be mutated.</dd>
</dl>


<a name='RequiresContext'></a>
## RequiresContext

A flag indicating the attribute requires a non-null *MutationContext&lt;T&gt;* to perform validation. Returns `true`.

#### Syntax

```csharp
public override bool RequiresContext { get; protected set; } = true;
```

<dl>
	<dt>Type</dt>
	<dd>System.Boolean</dd>
</dl>


<a name='MutateObjectInt32'></a>
## Mutate(Object, Int32)

Mutates the given value according to this *MutationAttribute*.

#### Syntax

```csharp
public object Mutate(
	object value,
	int maximumLength
)
```

#### Returns

The resulting mutated value.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>maximumLength</dt>
	<dd>Type: System.Int32<br />The maximum allowable length to apply to the string data.</dd>
</dl>


<a name='MutateValueObjectIMutationContext'></a>
## MutateValue(Object, IMutationContext)

Implements the mutation logic for this *ApplyMaxLengthAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

The truncated string data when the specified `value` exceeds the maximum allowable length.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: Dado.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
