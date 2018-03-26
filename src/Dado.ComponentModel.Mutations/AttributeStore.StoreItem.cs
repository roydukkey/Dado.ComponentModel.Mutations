// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dado.ComponentModel.DataMutations
{
	internal partial class AttributeStore
	{
		/// <summary>
		///		Private abstract class for all store items.
		/// </summary>
		private abstract class StoreItem
		{
			#region Constructors

			/// <summary>
			///		Initializes a new instance of the <see cref="StoreItem" /> class.
			/// </summary>
			/// <param name="attributes">The attributes to associated with this <see cref="StoreItem" />.</param>
			internal StoreItem(IEnumerable<Attribute> attributes)
			{
				Attributes = attributes;
			}

			#endregion Constructors

			#region Properties

			/// <summary>
			///		Gets the attributes associated with this <see cref="StoreItem" />.
			/// </summary>
			internal IEnumerable<Attribute> Attributes { get; }

			#endregion Properties
		}
	}
}
