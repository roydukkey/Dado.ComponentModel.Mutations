// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		A Cache of <see cref="AttributeStore" />s.
	/// </summary>
	/// <remarks>
	///		This internal class serves as a cache of attributes. It exists both to help performance as well as to abstract away the differences between Reflection and TypeDescriptor.
	/// </remarks>
	internal partial class AttributeStore
	{
		#region Fields

		private static readonly AttributeStore _Singleton = new AttributeStore();
		private readonly Dictionary<Type, TypeStoreItem> _typeStoreItems = new Dictionary<Type, TypeStoreItem>();

		#endregion Fields

		#region Properties

		/// <summary>
		///		Gets the singleton <see cref="AttributeStore" />.
		/// </summary>
		internal static AttributeStore Instance => _Singleton;

		#endregion Properties

		#region Internal Methods

		internal static bool IsPublic(PropertyInfo p)
			=> (p.GetMethod != null && p.GetMethod.IsPublic) || (p.SetMethod != null && p.SetMethod.IsPublic);

		/// <summary>
		///		Retrieves the set of attributes for the property.
		/// </summary>
		/// <param name="context">The context that describes the object containing property.</param>
		/// <param name="property">The <see cref="PropertyInfo" /> that describes the property.</param>
		/// <returns>The collection of attributes.</returns>
		internal IEnumerable<Attribute> GetPropertyAttributes(IMutationContext context, PropertyInfo property)
		{
			var typeItem = GetTypeStoreItem(context.ObjectInstance.GetType());
			var item = typeItem.GetPropertyStoreItem(property.Name);

			return item.Attributes;
		}

		/// <summary>
		///		Retrieves the Type of the given property.
		/// </summary>
		/// <param name="context">The context that describes the object containing property.</param>
		/// <param name="property">The <see cref="PropertyInfo" /> that describes the property.</param>
		/// <returns>The type of the specified property.</returns>
		internal Type GetPropertyType(IMutationContext context, PropertyInfo property)
		{
			var typeItem = GetTypeStoreItem(context.ObjectInstance.GetType());
			var item = typeItem.GetPropertyStoreItem(property.Name);

			return item.PropertyType;
		}

		/// <summary>
		///		Retrieves the type level attributes for the given type.
		/// </summary>
		/// <param name="context">The context that describes the type.</param>
		/// <returns>The collection of attributes.</returns>
		internal IEnumerable<Attribute> GetTypeAttributes(IMutationContext context)
			=> GetTypeStoreItem(context.ObjectInstance.GetType()).Attributes;

		#endregion Internal Methods

		#region Private Methods

		/// <summary>
		///		Retrieves or creates the store item for the given type.
		/// </summary>
		/// <param name="type">The type whose store item is needed.</param>
		/// <returns>The type store item. It will not be <c>null</c>.</returns>
		private TypeStoreItem GetTypeStoreItem(Type type)
		{
			Debug.Assert(type != null);

			lock (_typeStoreItems) {
				TypeStoreItem item = null;

				if (!_typeStoreItems.TryGetValue(type, out item)) {
					// Use CustomAttributeExtensions.GetCustomAttributes() to get inherited attributes as well as direct ones
					var attributes = CustomAttributeExtensions.GetCustomAttributes(type.GetTypeInfo(), true);

					_typeStoreItems[type] = item = new TypeStoreItem(type, attributes);
				}

				return item;
			}
		}

		#endregion Private Methods
	}
}
