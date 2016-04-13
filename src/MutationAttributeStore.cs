// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		A Cache of <see cref="MutationAttributeStore" />s.
	/// </summary>
	/// <remarks>
	///		This internal class serves as a cache of mutation attributes. It exists both to help performance as well as to abstract away the differences between Reflection and TypeDescriptor.
	/// </remarks>
	internal partial class MutationAttributeStore
	{
		#region Fields

		private static readonly MutationAttributeStore _Singleton = new MutationAttributeStore();
		private readonly Dictionary<Type, TypeStoreItem> _typeStoreItems = new Dictionary<Type, TypeStoreItem>();

		#endregion Fields

		#region Properties

		/// <summary>
		///		Gets the singleton <see cref="MutationAttributeStore" />.
		/// </summary>
		internal static MutationAttributeStore Instance => _Singleton;

		#endregion Properties

		#region Internal Methods

		internal static bool IsPublic(PropertyInfo p)
			=> (p.GetMethod != null && p.GetMethod.IsPublic) || (p.SetMethod != null && p.SetMethod.IsPublic);

		/// <summary>
		///		Retrieves the set of mutation attributes for the property.
		/// </summary>
		/// <param name="context">The context that describes the object containing property.</param>
		/// <param name="property">The <see cref="PropertyInfo" /> that describes the property.</param>
		/// <returns>The collection of mutation attributes.</returns>
		internal IEnumerable<MutationAttribute> GetPropertyMutationAttributes(IMutationContext context, PropertyInfo property)
		{
			var typeItem = GetTypeStoreItem(context.ObjectInstance.GetType());
			var item = typeItem.GetPropertyStoreItem(property.Name);

			return item.MutationAttributes;
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
		///		Retrieves the type level mutation attributes for the given type.
		/// </summary>
		/// <param name="context">The context that describes the type.</param>
		/// <returns>The collection of mutation attributes.</returns>
		internal IEnumerable<MutationAttribute> GetTypeMutationAttributes(IMutationContext context)
		{
			var item = GetTypeStoreItem(context.ObjectInstance.GetType());

			return item.MutationAttributes;
		}

		#endregion Internal Methods

		#region Private Methods

		/// <summary>
		///		Retrieves or creates the store item for the given type.
		/// </summary>
		/// <param name="type">The type whose store item is needed.</param>
		/// <returns>The type store item. It will not be <c>null</c>.</returns>
		private TypeStoreItem GetTypeStoreItem(Type type)
		{
			if (type == null) {
				throw new ArgumentNullException(nameof(type));
			}

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
