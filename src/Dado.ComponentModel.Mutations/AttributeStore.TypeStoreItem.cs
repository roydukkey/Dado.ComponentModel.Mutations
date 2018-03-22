// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dado.ComponentModel.DataMutations
{
	internal partial class AttributeStore
	{
		/// <summary>
		///		Private class to store data associated with a type.
		/// </summary>
		private class TypeStoreItem : StoreItem
		{
			#region Fields

			private readonly object _syncRoot = new object();
			private readonly Type _type;
			private Dictionary<string, PropertyStoreItem> _propertyStoreItems;

			#endregion Fields

			#region Constructors

			/// <summary>
			///		Initializes a new instance of the <see cref="TypeStoreItem" /> class.
			/// </summary>
			/// <param name="type">The type to store.</param>
			/// <param name="attributes">The attributes associated with the type.</param>
			internal TypeStoreItem(Type type, IEnumerable<Attribute> attributes) : base(attributes)
			{
				_type = type;
			}

			#endregion Constructors

			#region Internal Methods

			/// <summary>
			///		Gets a <see cref="PropertyStoreItem" /> by property name.
			/// </summary>
			/// <param name="propertyName">The property name to lookup.</param>
			/// <returns>Returns a <see cref="PropertyStoreItem" />.</returns>
			internal PropertyStoreItem GetPropertyStoreItem(string propertyName)
			{
				PropertyStoreItem item = null;

				if (!TryGetPropertyStoreItem(propertyName, out item)) {
					throw new ArgumentException($"The type '{_type.Name}' does not contain a public property named '{propertyName}'.", nameof(propertyName));
				}

				return item;
			}

			/// <summary>
			///		Determines if a <see cref="PropertyStoreItem" /> exists for the specified property name.
			/// </summary>
			/// <param name="propertyName">The property name to lookup.</param>
			/// <param name="item">The <see cref="PropertyStoreItem" /> that was found.</param>
			/// <returns><c>true</c> if a <see cref="PropertyStoreItem" /> is found for the specified property name, otherwise, <c>false</c>.</returns>
			internal bool TryGetPropertyStoreItem(string propertyName, out PropertyStoreItem item)
			{
				if (String.IsNullOrEmpty(propertyName)) {
					throw new ArgumentNullException(nameof(propertyName));
				}

				if (_propertyStoreItems == null) {
					lock (_syncRoot) {
						_propertyStoreItems = CreatePropertyStoreItems();
					}
				}

				return _propertyStoreItems.TryGetValue(propertyName, out item);
			}

			#endregion Internal Methods

			#region Private Methods

			/// <summary>
			///		Creates and stores <see cref="PropertyStoreItem" />s.
			/// </summary>
			/// <returns>The dictionary of <see cref="PropertyStoreItem"/> associated by property name.</returns>
			private Dictionary<string, PropertyStoreItem> CreatePropertyStoreItems()
			{
				var propertyStoreItems = new Dictionary<string, PropertyStoreItem>();

				// exclude index properties to match old TypeDescriptor functionality
				var properties = _type.GetRuntimeProperties().Where(prop => IsPublic(prop) && !prop.GetIndexParameters().Any());

				foreach (PropertyInfo property in properties) {
					// use CustomAttributeExtensions.GetCustomAttributes() to get inherited attributes as well as direct ones
					var item = new PropertyStoreItem(property.PropertyType, CustomAttributeExtensions.GetCustomAttributes(property, true));

					propertyStoreItems[property.Name] = item;
				}

				return propertyStoreItems;
			}

			#endregion Private Methods
		}
	}
}
