# IMutableObject [..](README.md#documentation-index 'Documentation Index')

Describes custom mutation logic that should be preformed on an object during mutation.

**Namespace:** Dado.ComponentModel.DataMutations

#### Syntax

```csharp
public interface IMutableObject
```

### Methods

| Name | Description |
| ---- | ----------- |
| [Mutate(IMutationContext)](#MutateIMutationContext) | A method to implement custom mutation logic. |


<a name='MutateIMutationContext'></a>
## Mutate(IMutationContext)

A method to implement custom mutation logic.

#### Syntax

```csharp
void Mutate(
	IMutationContext context
)
```

#### Parameters

<dl>
	<dt>context</dt>
	<dd>Type: Dado.ComponentModel.DataMutations.IMutationContext<br />Describes the object being mutated and provides services and context for mutation.</dd>
</dl>
