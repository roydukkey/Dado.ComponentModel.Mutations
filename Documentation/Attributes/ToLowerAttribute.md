# ToLowerAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutate the specified string to a lowercase.

**Namespace:** System.ComponentModel.DataMutations<br />
**Implements:** System.ComponentModel.DataMutations.MutationAttribute (in System.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class ToLowerAttribute : MutationAttribute
```


### Properties

| Name | Description |
| ---- | ----------- |
| [CultureInfo](#CultureInfo) | Gets or sets the CultureInfo to be used when determining the appropriate case. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationContext&lt;T&gt;. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implements the mutation logic for this ToLowerAttribute. |


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


<a name='MutateValueObjectIMutationContext'></a>
## MutateValue(Object, IMutationContext)

Implements the mutation logic for this *ToLowerAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

The specified `value` converted to lowercase.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
