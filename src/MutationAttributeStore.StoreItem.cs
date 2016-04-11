// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace System.ComponentModel.DataMutations
{
	internal partial class MutationAttributeStore
	{
		/// <summary>
		///		Private abstract class for all store items.
		/// </summary>
		private abstract class StoreItem
		{
			#region Fields

			private readonly IEnumerable<MutationAttribute> _attributes;

			#endregion Fields

			#region Constructors

			/// <summary>
			///		Initializes a new instance of the <see cref="StoreItem" /> class.
			/// </summary>
			/// <param name="attributes">The attributes to associated with this <see cref="StoreItem" />.</param>
			internal StoreItem(IEnumerable<Attribute> attributes)
			{
				_attributes = attributes.OfType<MutationAttribute>();
			}

			#endregion Constructors

			#region Properties

			/// <summary>
			///		Gets the mutation attributes associated with this <see cref="StoreItem" />.
			/// </summary>
			internal IEnumerable<MutationAttribute> MutationAttributes => _attributes;

			#endregion Properties
		}
	}
}
