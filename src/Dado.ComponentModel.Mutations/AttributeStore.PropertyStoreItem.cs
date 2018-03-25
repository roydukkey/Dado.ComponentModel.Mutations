// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Dado.ComponentModel.DataMutations
{
	internal partial class AttributeStore
	{
		/// <summary>
		///		Private class to store data associated with a property.
		/// </summary>
		private class PropertyStoreItem : StoreItem
		{
			#region Fields

			private readonly Type _type;

			#endregion Fields

			#region Constructors

			/// <summary>
			///		Initializes a new instance of the <see cref="PropertyStoreItem" /> class.
			/// </summary>
			/// <param name="type">The type of property to store.</param>
			/// <param name="attributes">The attributes associated with the property type.</param>
			internal PropertyStoreItem(Type type, IEnumerable<Attribute> attributes) : base(attributes)
			{
				_type = type;
			}

			#endregion Constructors

			#region Properties

			/// <summary>
			///		Gets the property type.
			/// </summary>
			internal Type PropertyType => _type;

			#endregion Properties
		}
	}
}
