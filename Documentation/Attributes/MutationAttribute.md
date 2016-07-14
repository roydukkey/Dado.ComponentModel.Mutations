# MutationAttribute [..](../README.md#documentation-index 'Documentation Index')

Base class for all mutation attributes.

**Namespace:** System.ComponentModel.DataMutations<br />
**Implements:** System.Attribute (in System)

#### Syntax

```csharp
public abstract class MutationAttribute : Attribute
```


### Properties

| Name | Description |
| ---- | ----------- |
| [Priority](#Priority) | Gets or sets a value indicating the priority that determines the order in which MutationAttributes are evaluated. |
| [RequiresContext](#RequiresContext) | A flag indicating the attribute requires a non-null MutationContext&lt;T&gt; to perform validation. |


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(Object, IMutationContext)](#MutateObjectIMutationContext) | Mutates the given value according to this MutationAttribute. |
| [MutateValue(Object, IMutationContext)](#MutateValueObjectIMutationContext) | A protected method to override and implement mutation logic. |


<a name='Priority'></a>
## Priority

Gets or sets a value indicating the priority that determines the order in which *MutationAttributes* are evaluated. Base class defaults to `10`. Override in child classes as appropriate.

#### Syntax

```csharp
public virtual int Priority { get; set; } = 10;
```

<dl>
	<dt>Type</dt>
	<dd>System.Integer</dd>
</dl>


<a name='RequiresContext'></a>
## RequiresContext

A flag indicating the attribute requires a non-null *MutationContext&lt;T&gt;* to perform validation. Base class returns `false`. Override in child classes as appropriate.

#### Syntax

```csharp
public virtual bool RequiresContext { get; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Boolean</dd>
</dl>


<a name='MutateObjectIMutationContext'></a>
## Mutate(Object, IMutationContext)

Mutates the given value according to this *MutationAttribute*.

#### Syntax

```csharp
public object Mutate(
	object value,
	IMutationContext context = null
)
```

#### Returns

The resulting mutated value.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is required and **null**. |


<a name='MutateValueObjectIMutationContext'></a>
## MutateValue(Object, IMutationContext)

A protected method to override and implement mutation logic.

#### Syntax

```csharp
protected abstract object MutateValue(
	object value,
	IMutationContext context
)
```

#### Returns

The resulting mutated value.

#### Parameters

<dl>
	<dt>value</dt>
	<dd>Type: System.Object<br />The value to mutate.</dd>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.IMutationContext<br />Describes the <code>value</code> being mutated and provides services and context for mutation.</dd>
</dl>
