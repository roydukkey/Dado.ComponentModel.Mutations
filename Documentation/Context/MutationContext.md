# MutationContext&lt;T&gt; [..](../README.md#documentation-index 'Documentation Index')

Describes the context in which mutation is performed.

**Namespace:** Dado.ComponentModel.DataMutations<br />
**Implements:** Dado.ComponentModel.DataMutations.IMutationContext (in Dado.ComponentModel.Mutations)

#### Syntax

```csharp
public sealed class MutationContext<T> : IMutationContext
```

<dl>
	<dt>T</dt>
	<dd>The type to consult during mutation.</dd>
</dl>

#### Remarks

This class contains information describing the instance on which mutation is performed.

An *Items* property bag is available for additional contextual information about the mutation. Values stored in *Items* will be available to mutation methods that use this *MutationContext&lt;T&gt;*.


### Constructors

| Name | Description |
| ---- | ----------- |
| [MutationContext&lt;T&gt;(T)](#MutationContextT) | Initializes a new instance of the MutationContext&lt;T&gt; class for a given object instance. |
| [MutationContext&lt;T&gt;(T, IDictionary<object, object>)](#MutationContextTIDictionary) | Initializes a new instance of the MutationContext&lt;T&gt; class for a given object instance and a property bag of items. |
| [MutationContext&lt;T&gt;(T, IServiceProvider)](#MutationContextTIServiceProvider) | Initializes a new instance of the MutationContext&lt;T&gt; class for a given object instance and a serviceProvider. |
| [MutationContext&lt;T&gt;(T, IDictionary<object, object>, IServiceProvider)](#MutationContextTIDictionaryIServiceProvider) | Initializes a new instance of the MutationContext&lt;T&gt; class for a given object instance, a serviceProvider, and a property bag of items. |


### Properties

| Name | Description |
| ---- | ----------- |
| [Attributes](#Attributes) | Gets the attributes associated with this context. |
| [Items](#Items) | Gets the dictionary of key/value pairs associated with this context. |
| [ObjectInstance](#ObjectInstance) | Gets the instance being mutated. |


### Methods

| Name | Description |
| ---- | ----------- |
| [GetService(Type)](#GetServiceType) | Returns the service that provides custom mutation. |
| [InitializeServiceProvider(Func&lt;Type, object&gt;)](#InitializeServiceProviderFuncTypeObject) | Initializes the MutationContext&lt;T&gt; with a service provider that can return service instances by Type when GetService is called. |


### Explicit Interface Implementations

| Name | Description |
| ---- | ----------- |
| [IMutationContext.ObjectInstance](#IMutationContextObjectInstance) | Gets the instance being mutated. |


### Extension Methods

| Name | Description |
| ---- | ----------- |
| [Mutate&lt;T&gt;()](Mutator.md#MutateMutationContext) | Mutates the instance associated with the current context. |
| [Mutate&lt;T&gt;(IEnumerable&lt;MutationAttribute&gt;)](Mutator.md#MutateMutationContextIEnumerable) | Mutates the value or instance associated with the current context and the specified MutationAttributes. |
| [Mutate&lt;T&gt;(IEnumerable&lt;MutationAttribute&gt;, T)](Mutator.md#MutateMutationContextIEnumerableT) | Mutates the specified value against the current context and the specified MutationAttributes. |
| [Mutate&lt;T&gt;(T)](Mutator.md#MutateMutationContextT) | Mutates the specified instance against the current context. |
| [Mutate&lt;T&gt;(T, IEnumerable&lt;MutationAttribute&gt;)](Mutator.md#MutateMutationContextTIEnumerable) | Mutates the specified instance against the current context and the specified MutationAttributes. |
| [MutateProperty&lt;T&gt;(PropertyInfo)](Mutator.md#MutatePropertyMutationContextPropertyInfo) | Mutates the specified property of the instance associated with the current context. |
| [MutateProperty&lt;T, P&gt;(PropertyInfo, P)](Mutator.md#MutatePropertyMutationContextPropertyInfoP) | Mutates the specified value against the specified property of the instance associated with the current context. |
| [MutateProperty&lt;T, P&gt;(Expression&lt;Func&lt;T, P&gt;&gt;)](Mutator.md#MutatePropertyMutationContextExpression) | Mutates the specified property of the instance associated with the current context. |
| [MutateProperty&lt;T, P&gt;(Expression&lt;Func&lt;T, P&gt;&gt;, P)](Mutator.md#MutatePropertyMutationContextExpressionP) | Mutates the specified value against the specified property of the instance associated with the current context. |


<a name='MutationContextT'></a>
## MutationContext&lt;T&gt;(T)

Initializes a new instance of the *MutationContext&lt;T&gt;* class for a given object `instance`.

```csharp
public MutationContext(
	T instance
)
```

#### Parameters

<dl>
	<dt>instance</dt>
	<dd>Type: `T<br />The instance to be modified during mutation.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `instance` is **null**. |


<a name='MutationContextTIDictionary'></a>
## MutationContext&lt;T&gt;(T, IDictionary<object, object>)

Initializes a new instance of the *MutationContext&lt;T&gt;* class for a given object `instance` and a property bag of `items`.

```csharp
public MutationContext(
	T instance,
	IDictionary<object, object> items
)
```

#### Parameters

<dl>
	<dt>instance</dt>
	<dd>Type: `T<br />The instance to be modified during mutation.</dd>
	<dt>items</dt>
	<dd>Type: System.Collections.Generic.IDictionary&lt;System.Object, System.Object&gt;<br />A set of key/value pairs to make available to consumers via <em>Items</em>. The set of key/value pairs will be copied into a new dictionary, preventing consumers from modifying the original dictionary.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `instance` is **null**. |


<a name='MutationContextTIServiceProvider'></a>
## MutationContext&lt;T&gt;(T, IServiceProvider)

Initializes a new instance of the *MutationContext&lt;T&gt;* class for a given object `instance` and a `serviceProvider`.

```csharp
public MutationContext(
	T instance,
	IServiceProvider serviceProvider
)
```

#### Parameters

<dl>
	<dt>instance</dt>
	<dd>Type: `T<br />The instance to be modified during mutation.</dd>
	<dt>serviceProvider</dt>
	<dd>Type: System.IServiceProvider<br />A <em>IServiceProvider</em> to use when <em>GetService</em> is called.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `instance` is **null**. |


<a name='MutationContextTIDictionaryIServiceProvider'></a>
## MutationContext&lt;T&gt;(T, IDictionary<object, object>, IServiceProvider)

Initializes a new instance of the *MutationContext&lt;T&gt;* class for a given object `instance`, a `serviceProvider`, and a property bag of `items`.

```csharp
public MutationContext(
	T instance,
	IDictionary<object, object> items,
	IServiceProvider serviceProvider
)
```

#### Parameters

<dl>
	<dt>instance</dt>
	<dd>Type: `T<br />The instance to be modified during mutation.</dd>
	<dt>items</dt>
	<dd>Type: System.Collections.Generic.IDictionary&lt;System.Object, System.Object&gt;<br />A set of key/value pairs to make available to consumers via <em>Items</em>. The set of key/value pairs will be copied into a new dictionary, preventing consumers from modifying the original dictionary.</dd>
	<dt>serviceProvider</dt>
	<dd>Type: System.IServiceProvider<br />A <em>IServiceProvider</em> to use when <em>GetService</em> is called.</dd>
</dl>

#### Exceptions

| Exception | Condition |
| --------- | --------- |
| System.ArgumentNullException | When `instance` is **null**. |


<a name='Attributes'></a>
## Attributes

Gets the attributes associated with this context.

#### Syntax

```csharp
IEnumerable<Attribute> Attributes { get; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Collections.Generic&lt;System.Attribute&gt;</dd>
</dl>


<a name='Items'></a>
## Items

Gets the dictionary of key/value pairs associated with this context.

```csharp
public IDictionary<object, object> Items { get; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Collections.Generic.IDictionary&lt;System.Object, System.Object&gt;</dd>
</dl>

#### Property Value

This property will never be **null**, but the dictionary may be empty. Changes made to items in this dictionary will never affect the original dictionary specified in the constructor.


<a name='ObjectInstance'></a>
## ObjectInstance

Gets the instance being mutated. While it will not be **null**, the state of the instance is indeterminate as it might only be partially initialized during mutation.

Consume this instance with caution!

```csharp
public T ObjectInstance { get; }
```

<dl>
	<dt>Type</dt>
	<dd>`T</dd>
</dl>

#### Remarks

During mutation, especially property-level mutation, the instance might be in a indeterminate state. For example, the property being mutated, as well as other properties on the instance might not have been updated to their new values.


<a name='GetServiceType'></a>
## GetService(Type)

Returns the service that provides custom mutation.

```csharp
public object GetService(
	Type serviceType
)
```

#### Returns

An instance of that service or **null** if it is not available.

#### Parameters

<dl>
	<dt>serviceType</dt>
	<dd>Type: System.Type<br />The type of the service needed.</dd>
</dl>


<a name='InitializeServiceProviderFuncTypeObject'></a>
## InitializeServiceProvider(Func&lt;Type, object&gt;)

Initializes the *MutationContext&lt;T&gt;* with a service provider that can return service instances by *Type* when *GetService* is called.

```csharp
public void InitializeServiceProvider(
	Func<Type, object> serviceProvider
)
```

#### Parameters

<dl>
	<dt>serviceProvider</dt>
	<dd>Type: System.Func&lt;System.Type, System.Object&gt;<br />A <em>Func&lt;Type, object&gt;</em> that can return service instances given the desired <em>Type</em> when <em>GetService</em> is called. If it is <strong>null</strong>, <em>GetService</em> will always return <strong>null</strong>.</dd>
</dl>


<a name='IMutationContextObjectInstance'></a>
## IMutationContext.ObjectInstance

Gets the instance being mutated.

```csharp
object IMutationContext.ObjectInstance { get; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Object</dd>
</dl>
