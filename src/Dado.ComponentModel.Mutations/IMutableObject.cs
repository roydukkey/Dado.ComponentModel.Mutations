// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Describes custom mutation logic that should be preformed on an object during mutation.
	/// </summary>
	public interface IMutableObject
	{
		/// <summary>
		///		A method to implement custom mutation logic.
		/// </summary>
		/// <param name="context">Describes the object being mutated and provides services and context for mutation.</param>
		void Mutate(IMutationContext context);
	}
}
