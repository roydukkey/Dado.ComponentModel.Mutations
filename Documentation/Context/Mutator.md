# Mutator [..](../README.md#documentation-index 'Documentation Index')

Helper class to validate objects, properties, and other values using their associated MutationAttributes and custom mutation as implemented through the IMutableObject interface.

**Namespace:** System.ComponentModel.DataMutations

#### Syntax

```csharp
public static class Mutator
```


### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate&lt;T&gt;(MutationContext&lt;T&gt;)](#MutateMutationContext) | Mutates the instance associated with the current context. |
| [Mutate&lt;T&gt;(MutationContext&lt;T&gt;, IEnumerable&lt;MutationAttribute&gt;)](#MutateMutationContextIEnumerable) | Mutates the value or instance associated with the current context and the specified MutationAttributes. |
| [Mutate&lt;T&gt;(MutationContext&lt;T&gt;, IEnumerable&lt;MutationAttribute&gt;, T)](#MutateMutationContextIEnumerableT) | Mutates the specified value against the current context and the specified MutationAttributes. |
| [Mutate&lt;T&gt;(MutationContext&lt;T&gt;, T)](#MutateMutationContextT) | Mutates the specified instance against the current context. |
| [Mutate&lt;T&gt;(MutationContext&lt;T&gt;, T, IEnumerable&lt;MutationAttribute&gt;)](#MutateMutationContextTIEnumerable) | Mutates the specified instance against the current context and the specified MutationAttributes. |
| [MutateProperty&lt;T&gt;(MutationContext&lt;T&gt;, PropertyInfo)](#MutatePropertyMutationContextPropertyInfo) | Mutates the specified property of the instance associated with the current context. |
| [MutateProperty&lt;T, P&gt;(MutationContext&lt;T&gt;, PropertyInfo, P)](#MutatePropertyMutationContextPropertyInfoP) | Mutates the specified value against the specified property of the instance associated with the current context. |
| [MutateProperty&lt;T, P&gt;(MutationContext&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;)](#MutatePropertyMutationContextExpression) | Mutates the specified property of the instance associated with the current context. |
| [MutateProperty&lt;T, P&gt;(MutationContext&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;, P)](#MutatePropertyMutationContextExpressionP) | Mutates the specified value against the specified property of the instance associated with the current context. |


<a name='MutateMutationContext'></a>
## Mutate&lt;T&gt;(MutationContext&lt;T&gt;)

Mutates the instance associated with the current context.

#### Syntax

```csharp
public static T Mutate<T>(
	this MutationContext<T> context
)
	where T : class
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
</dl>

#### Returns

The object whose value and properties has been modified according to any associated *MutationAttribute*s and *IMutableObject* implementation.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |


<a name='MutateMutationContextIEnumerable'></a>
## Mutate&lt;T&gt;(MutationContext&lt;T&gt;, IEnumerable&lt;MutationAttribute&gt;)

Mutates the value or instance associated with the current context and the specified *MutationAttribute*s.

#### Syntax

```csharp
public static T Mutate<T>(
	this MutationContext<T> context,
	IEnumerable<MutationAttribute> attributes
)
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
</dl>

#### Returns

The object whose value and/or properties has been modified according to any associated *MutationAttribute*s and *IMutableObject* implementation.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
	<dt>attributes</dt>
	<dd>Type: System.Collections.Generic.IEnumerable&lt;System.ComponentModel.DataMutations.MutationAttribute&gt;<br />The list of <em>MutationAttribute</em>s to modify the specified <em>ObjectInstance</em> against.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |
| System.ArgumentNullException | When `attributes` is **null**. |

#### Remarks

When the consulting type, specified by `T`, is a reference type, the instance and its properties will be mutated. Likewise, only a value type's value will be mutated.

The *MutationAttribute*s specified in `attributes` will only be used to mutate the specified value or instance. Any properties that are mutated will be mutated according to their respective *MutationAttribute*s.


<a name='MutateMutationContextIEnumerableT'></a>
## Mutate&lt;T&gt;(MutationContext&lt;T&gt;, IEnumerable&lt;MutationAttribute&gt;, T)

Mutates the specified value against the current context and the specified *MutationAttribute*s.

#### Syntax

```csharp
public static T Mutate<T>(
	this MutationContext<T> context,
	IEnumerable<MutationAttribute> attributes,
	T value
)
	where T : struct
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
</dl>

#### Returns

The value that has been modified according to any associated *MutationAttribute*s and *IMutableObject* implementation.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
	<dt>attributes</dt>
	<dd>Type: System.Collections.Generic.IEnumerable&lt;System.ComponentModel.DataMutations.MutationAttribute&gt;<br />The list of <em>MutationAttribute</em>s to modify the specified <code>value</code> against.</dd>
	<dt>value</dt>
	<dd>Type: `T<br />The value to be mutated.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |
| System.ArgumentNullException | When `attributes` is **null**. |


<a name='MutateMutationContextT'></a>
## Mutate&lt;T&gt;(MutationContext&lt;T&gt;, T)

Mutates the specified instance against the current context.

#### Syntax

```csharp
public static T Mutate<T>(
	this MutationContext<T> context,
	T instance
)
	where T : class
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
</dl>

#### Returns

The object whose value and/or properties has been modified according to any associated *MutationAttribute*s and *IMutableObject* implementation.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
	<dt>instance</dt>
	<dd>Type: `T<br />The instance to be modified.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |
| System.ArgumentNullException | When `attributes` is **null**. |


<a name='MutateMutationContextTIEnumerable'></a>
## Mutate&lt;T&gt;(MutationContext&lt;T&gt;, T, IEnumerable&lt;MutationAttribute&gt;)

Mutates the specified instance against the current context and the specified *MutationAttribute*s.

#### Syntax

```csharp
public static T Mutate<T>(
	this MutationContext<T> context,
	T instance,
	IEnumerable<MutationAttribute> attributes
)
	where T : class
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
</dl>

#### Returns

The object whose value and properties has been modified according to any associated *MutationAttribute*s and *IMutableObject* implementation.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
	<dt>instance</dt>
	<dd>Type: `T<br />The instance to be modified.</dd>
	<dt>attributes</dt>
	<dd>Type: System.Collections.Generic.IEnumerable&lt;System.ComponentModel.DataMutations.MutationAttribute&gt;<br />The list of <em>MutationAttribute</em>s to modify the specified <code>instance</code> against.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |
| System.ArgumentNullException | When `attributes` is **null**. |


<a name='MutatePropertyMutationContextPropertyInfo'></a>
## MutateProperty&lt;T&gt;(MutationContext&lt;T&gt;, PropertyInfo)

Mutates the specified property of the instance associated with the current context.

#### Syntax

```csharp
public static object MutateProperty<T>(
	this MutationContext<T> context,
	PropertyInfo property
)
	where T : class
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
</dl>

#### Returns

The property value that has been modified according to any associated *MutationAttribute*s.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
	<dt>property</dt>
	<dd>Type: System.Reflection.PropertyInfo<br />The property info that describes the member to be modified.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |
| System.ArgumentNullException | When `property` is **null**. |
| System.ArgumentException | When the *PropertyInfo.Name* of `context` is not a valid property. |


<a name='MutatePropertyMutationContextPropertyInfoP'></a>
## MutateProperty&lt;T, P&gt;(MutationContext&lt;T&gt;, PropertyInfo, P)

Mutates the specified value against the specified property of the instance associated with the current context.

#### Syntax

```csharp
public static P MutateProperty<T, P>(
	this MutationContext<T> context,
	PropertyInfo property,
	P value
)
	where T : class
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
	<dt>P</dt>
	<dd>The property type to consult for <em>MutationAttribute</em>s.</dd>
</dl>

#### Returns

The value that has been modified according to any *MutationAttribute*s associated with the specified property.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
	<dt>property</dt>
	<dd>Type: System.Reflection.PropertyInfo<br />The property info that describes the <code>value</code> to be modified.</dd>
	<dt>value</dt>
	<dd>Type: `P<br />The value to be mutated.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |
| System.ArgumentNullException | When `property` is **null**. |
| System.ArgumentException | When the *PropertyInfo.Name* of `context` is not a valid property. |


<a name='MutatePropertyMutationContextExpression'></a>
## MutateProperty&lt;T, P&gt;(MutationContext&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;)

Mutates the specified property of the instance associated with the current context.

#### Syntax

```csharp
public static P MutateProperty<T, P>(
	this MutationContext<T> context,
	Expression<Func<T, P>> property
)
	where T : class
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
	<dt>P</dt>
	<dd>The property type to consult for <em>MutationAttribute</em>s.</dd>
</dl>

#### Returns

The property value that has been modified according to any associated *MutationAttribute*s.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
	<dt>property</dt>
	<dd>Type: System.Linq.Expressions.Expression&lt;System.Func&lt;`T, `P&gt;&gt;<br />The expression that selects the property to be modified.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |
| System.ArgumentNullException | When `property` is **null**. |
| System.ArgumentException | When the expression doesn't indicate a valid `property`. |
| System.ArgumentException | When the *PropertyInfo.Name* of `context` is not a valid property. |


<a name='MutatePropertyMutationContextExpressionP'></a>
## MutateProperty&lt;T, P&gt;(MutationContext&lt;T&gt;, Expression&lt;Func&lt;T, P&gt;&gt;, P)

Mutates the specified value against the specified property of the instance associated with the current context.

#### Syntax

```csharp
public static P MutateProperty<T, P>(
	this MutationContext<T> context,
	Expression<Func<T, P>> property,
	P value
)
	where T : class
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
	<dt>P</dt>
	<dd>The property type to consult for <em>MutationAttribute</em>s.</dd>
</dl>

#### Returns

The value that has been modified according to any *MutationAttribute*s associated with the specified property.

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: System.ComponentModel.DataMutations.MutationContext&lt;`T&gt;<br />Describes the type of object being mutated and provides services and context for mutation.</dd>
	<dt>property</dt>
	<dd>Type: System.Linq.Expressions.Expression&lt;System.Func&lt;`T, `P&gt;&gt;<br />The expression that selects the property that describes the `value` to be modified.</dd>
	<dt>value</dt>
	<dd>Type: `P<br />The value to be mutated.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `context` is **null**. |
| System.ArgumentNullException | When `property` is **null**. |
| System.ArgumentException | When the expression doesn't indicate a valid `property`. |
| System.ArgumentException | When the *PropertyInfo.Name* of `context` is not a valid property. |
