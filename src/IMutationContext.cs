// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Describes the context in which mutation is performed.
	/// </summary>
	/// <remarks>
	///		It supports <see cref="IServiceProvider" /> so that custom mutation code can acquire additional services to help it perform its mutation.
	/// </remarks>
	public interface IMutationContext : IServiceProvider
	{
		/// <summary>
		///		Gets the instance being mutated.
		/// </summary>
		object ObjectInstance { get; }

		/// <summary>
		///		Gets the dictionary of key/value pairs associated with this context.
		/// </summary>
		IDictionary<object, object> Items { get; }

		/// <summary>
		///		Gets the attributes associated with this context.
		/// </summary>
		IEnumerable<Attribute> Attributes { get; }
	}
}
