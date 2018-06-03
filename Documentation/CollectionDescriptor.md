# ApplyMaxLengthAttribute.CollectionDescriptor [..](README.md#documentation-index 'Documentation Index')

Used to describe how collections should be mutated.

**Namespace:** Dado.ComponentModel.DataMutations

#### Syntax

```csharp
public sealed class CollectionDescriptor
```

#### Remarks

This class will likely be obsolete once generics or lambdas are implemented for *System.Attribute*s.


### Properties

| Name | Description |
| ---- | ----------- |
| [CollectionMutator](#CollectionMutator) | A function accepting a length and collection which returns a collection. |


### Methods

| Name | Description |
| ---- | ----------- |
| [SetCollectionMutator&lt;T&gt;(Func&lt;Int32, T, T&gt;)](#SetCollectionMutatorFunc) | Sets the CollectionMutator for an ICollection. |
| [SetGenericCollectionMutator&lt;T&gt;(Func&lt;Int32, ICollection&lt;T&gt;, ICollection&lt;T&gt;&gt;)](#SetGenericCollectionMutatorFunc) | Sets the CollectionMutator for a generic ICollection&lt;T&gt;. |


<a name='CollectionMutator'></a>
## CollectionMutator

A function accepting a length and collection which returns a collection.

#### Syntax

```csharp
public Func<int, IEnumerable, IEnumerable> CollectionMutator { get; private set; }
```

<dl>
	<dt>Type</dt>
	<dd>System.Func&lt;Int32, IEnumerable, IEnumerable&gt;</dd>
</dl>


<a name='SetCollectionMutatorFunc'></a>
## SetCollectionMutator&lt;T&gt;(Func&lt;Int32, T, T&gt;)

Sets the *CollectionMutator* for an *ICollection*.

#### Syntax

```csharp
public CollectionDescriptor SetCollectionMutator<T>(
	Func<int, T, T> mutator
)
	where T : ICollection
```

<dl>
	<dt>T</dt>
	<dd>The type of that describes an <em>ICollection</em>.</dd>
</dl>

#### Returns

Returns this instance of the *CollectionDescriptor*.

#### Parameters

<dl>
	<dt>mutator</dt>
	<dd>Type: System.Func&lt;Int32, T, T&gt;<br />The function accepting a length and collection which returns a collection.</dd>
</dl>


<a name='SetGenericCollectionMutatorFunc'></a>
## SetGenericCollectionMutator&lt;T&gt;(Func&lt;Int32, ICollection&lt;T&gt;, ICollection&lt;T&gt;&gt;)

Sets the *CollectionMutator* for a generic *ICollection&lt;T&gt;*.

#### Syntax

```csharp
public CollectionDescriptor SetGenericCollectionMutator<T>(
	Func<int, ICollection<T>, ICollection<T>> mutator
)
```

<dl>
	<dt>T</dt>
	<dd>The type of the elements in the collection.</dd>
</dl>

#### Returns

Returns this instance of the *CollectionDescriptor*.

#### Parameters

<dl>
	<dt>mutator</dt>
	<dd>Type: System.Func&lt;Int32, ICollection&lt;T&gt;, ICollection&lt;T&gt;&gt;<br />The function accepting a length and collection which returns a collection.</dd>
</dl>


### See Also
dotnet/csharplang#124
dotnet/csharplang#343
