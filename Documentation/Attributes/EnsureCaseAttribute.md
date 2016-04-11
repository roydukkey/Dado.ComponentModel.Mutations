# EnsureCaseAttribute [..](../README.md#documentation-index 'Documentation Index')

Used to mutated the specified string to a specified case.

**Namespace:** System.ComponentModel.DataMutations<br />
**Implements:** System.ComponentModel.DataMutations.MutationAttribute (in System.ComponentModel.Mutations)

#### Syntax

```csharp
[AttributeUsage(AttributeTargets.Property)]
public class EnsureCaseAttribute : MutationAttribute
```

#### Remarks

**@!coreclr =>** Generally, title casing converts the first character of a word to uppercase and the rest of the characters to lowercase. However, this method does not currently provide proper casing to convert a word that is entirely uppercase, such as an acronym.


### Constructors

| Name | Description |
| ---- | ----------- |
| [EnsureCaseAttribute(CaseOptions)](#EnsureCaseAttributeCaseOptions) | Initializes a new instance of the EnsureCaseAttribute class. |


### Properties

| Name | Description |
| ---- | ----------- |
| [Case](#Case) | Gets the desired case of the string after mutation. |
| [CultureInfo](#CultureInfo) | Gets or sets the CultureInfo to be used when determining the appropriate case. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](MutationAttribute.md#MutateObjectIMutationContext) | Mutates the given value according to this MutationContext&lt;T&gt;. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | Implement the mutation logic for this EnsureCaseAttribute. |


<a name='EnsureCaseAttributeCaseOptions'></a>
## EnsureCaseAttribute(CaseOptions)

Initializes a new instance of the *EnsureCaseAttribute* class.

#### Syntax

```csharp
public EnsureCaseAttribute(
	CaseOptions caseOption
)
```

#### Parameters

<dl>
	<dt>caseOption</dt>
	<dd>Type: System.ComponentModel.DataMutations.CaseOptions<br />The desired case of the string after mutation.</dd>
</dl>


<a name='Case'></a>
## Case

Gets the desired case of the string after mutation.

#### Syntax

```csharp
public CaseOptions Case { get; private set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.ComponentModel.DataMutations.CaseOptions</dd>
</dl>


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

Implement the mutation logic for this *EnsureCaseAttribute*.

#### Syntax

```csharp
protected override object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

The specified `value` converted to the specified *Case*.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
