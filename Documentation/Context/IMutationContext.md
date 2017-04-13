# IMutationContext [..](../README.md#documentation-index 'Documentation Index')

Describes the context in which mutation is performed.

**Namespace:** Dado.ComponentModel.DataMutations<br />
**Implements:** System.IServiceProvider (in System.ComponentModel)

#### Syntax

```csharp
public interface IMutationContext : IServiceProvider
```

#### Remarks

It supports *IServiceProvider* so that custom mutation code can acquire additional services to help it perform its mutation.


### Properties

| Name | Description |
| ---- | ----------- |
| [Attributes](#Attributes) | Gets the attributes associated with this context. |
| [Items](#Items) | Gets the dictionary of key/value pairs associated with this context. |
| [ObjectInstance](#ObjectInstance) | Gets the instance being mutated. |


### Methods

| Name | Description |
| ---- | ----------- |
| [GetService(Type)](#GetServiceType) | Gets the service object of the specified type. |


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
IDictionary<object, object> Items { get; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Collections.Generic.IDictionary&lt;System.Object, System.Object&gt;</dd>
</dl>


<a name='ObjectInstance'></a>
## ObjectInstance

Gets the instance being mutated.

#### Syntax

```csharp
object ObjectInstance { get; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Object</dd>
</dl>


<a name='GetServiceType'></a>
## GetService(Type)

Gets the service object of the specified type.

#### Syntax

```csharp
object GetService(
	Type serviceType
)
```

#### Returns

A service object of type `serviceType` or **null** if there is no service object of type `serviceType`.

#### Parameters

<dl>
	<dt>serviceType</dt>
	<dd>Type: System.Type<br />An object that specifies the type of service object to get.</dd>
</dl>
