// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Helper class to validate objects, properties, and other values using their associated <see cref="MutationAttribute" />s and custom mutation as implemented through the <see cref="IMutableObject" /> interface.
	/// </summary>
	public static class Mutator
	{
		#region Mutate Value

		/// <summary>
		///		Mutates the value or instance associated with the current context and the specified <see cref="MutationAttribute" />s.
		/// </summary>
		/// <remarks>
		///		When the consulting type, specified by <typeparamref name="T" />, is a reference type, the instance and its properties will be mutated. Likewise, only a value type's value will be mutated.
		///		<para>
		///			The <see cref="MutationAttribute" />s specified in <paramref name="attributes" /> will only be used to mutate the specified value or instance. Any properties that are mutated will be mutated according to their respective <see cref="MutationAttribute" />s.
		///		</para>
		/// </remarks>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="attributes">The list of <see cref="MutationAttribute" />s to modify the specified <see cref="MutationContext{T}.ObjectInstance" /> against.</param>
		/// <returns>The object whose value and/or properties has been modified according to any associated <see cref="MutationAttribute" />s and <see cref="IMutableObject" /> implementation.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="attributes" /> is <c>null</c>.</exception>
		public static T Mutate<T>(this MutationContext<T> context, IEnumerable<MutationAttribute> attributes)
			=> typeof(T).GetTypeInfo().IsValueType
				? GetMutatedValue(context, attributes, context == null ? default(T) : context.ObjectInstance)
				: GetMutatedObject(context, attributes, true);

		/// <summary>
		///		Mutates the specified value against the current context and the specified <see cref="MutationAttribute" />s.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="attributes">The list of <see cref="MutationAttribute" />s to modify the specified <paramref name="value" /> against.</param>
		/// <param name="value">The value to be mutated.</param>
		/// <returns>The value that has been modified according to any associated <see cref="MutationAttribute" />s and <see cref="IMutableObject" /> implementation.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="attributes" /> is <c>null</c>.</exception>
		public static T Mutate<T>(this MutationContext<T> context, IEnumerable<MutationAttribute> attributes, T value)
			where T : struct
			=> GetMutatedValue(context, attributes, value);

		#endregion Mutate Value

		#region Mutate Object

		/// <summary>
		///		Mutates the instance associated with the current context.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <returns>The object whose value and properties has been modified according to any associated <see cref="MutationAttribute" />s and <see cref="IMutableObject" /> implementation.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		public static T Mutate<T>(this MutationContext<T> context)
			where T : class
			=> GetMutatedObject(context, true);

		/// <summary>
		///		Mutates the specified instance against the current context.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="instance">The instance to be modified.</param>
		/// <returns>The object whose value and/or properties has been modified according to any associated <see cref="MutationAttribute" />s and <see cref="IMutableObject" /> implementation.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		public static T Mutate<T>(this MutationContext<T> context, T instance)
			where T : class
			=> GetMutatedObject(context, false, instance);

		/// <summary>
		///		Mutates the specified instance against the current context and the specified <see cref="MutationAttribute" />s.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="instance">The instance to be modified.</param>
		/// <param name="attributes">The list of <see cref="MutationAttribute" />s to modify the specified <paramref name="instance" /> against.</param>
		/// <returns>The object whose value and properties has been modified according to any associated <see cref="MutationAttribute" />s and <see cref="IMutableObject" /> implementation.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="attributes" /> is <c>null</c>.</exception>
		public static T Mutate<T>(this MutationContext<T> context, T instance, IEnumerable<MutationAttribute> attributes)
			where T : class
			=> GetMutatedObject(context, attributes, false, instance);

		#endregion Mutate Object

		#region Mutate Property

		/// <summary>
		///		Mutates the specified property of the instance associated with the current context.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="property">The property info that describes the member to be modified.</param>
		/// <returns>The property value that has been modified according to any associated <see cref="MutationAttribute" />s.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="property" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">When the <see cref="MemberInfo.Name" /> of <paramref name="context" /> is not a valid property.</exception>
		public static object MutateProperty<T>(this MutationContext<T> context, PropertyInfo property)
			where T : class
			=> GetMutatedProperty<T, object>(context, property, true);

		/// <summary>
		///		Mutates the specified value against the specified property of the instance associated with the current context.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <typeparam name="P">The property type to consult for <see cref="MutationAttribute" />s.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="property">The property info that describes the <paramref name="value" /> to be modified.</param>
		/// <param name="value">The value to be mutated.</param>
		/// <returns>The value that has been modified according to any <see cref="MutationAttribute" />s associated with the specified property.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="property" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">When the <see cref="MemberInfo.Name" /> of <paramref name="context" /> is not a valid property.</exception>
		public static P MutateProperty<T, P>(this MutationContext<T> context, PropertyInfo property, P value)
			where T : class
			=> GetMutatedProperty(context, property, true, value);

		/// <summary>
		///		Mutates the specified property of the instance associated with the current context.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <typeparam name="P">The property type to consult for <see cref="MutationAttribute" />s.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="property">The expression that selects the property to be modified.</param>
		/// <returns>The property value that has been modified according to any associated <see cref="MutationAttribute" />s.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="property" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">When the expression doesn't indicate a valid <paramref name="property" />.</exception>
		/// <exception cref="ArgumentException">When the <see cref="MemberInfo.Name" /> of <paramref name="context" /> is not a valid property.</exception>
		public static P MutateProperty<T, P>(this MutationContext<T> context, Expression<Func<T, P>> property)
			where T : class
			=> GetMutatedProperty<T, P>(context, GetPropertyInfo(property), true);

		/// <summary>
		///		Mutates the specified value against the specified property of the instance associated with the current context.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <typeparam name="P">The property type to consult for <see cref="MutationAttribute" />s.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="property">The expression that selects the property that describes the <paramref name="value" /> to be modified.</param>
		/// <param name="value">The value to be mutated.</param>
		/// <returns>The value that has been modified according to any <see cref="MutationAttribute" />s associated with the specified property.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="property" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">When the expression doesn't indicate a valid <paramref name="property" />.</exception>
		/// <exception cref="ArgumentException">When the <see cref="MemberInfo.Name" /> of <paramref name="context" /> is not a valid property.</exception>
		public static P MutateProperty<T, P>(this MutationContext<T> context, Expression<Func<T, P>> property, P value)
			where T : class
			=> GetMutatedProperty(context, GetPropertyInfo(property), false, value);

		#endregion Mutate Property

		#region Private Methods

		/// <summary>
		///		Mutates the specified value against the current context and the specified <see cref="MutationAttribute" />s.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="attributes">The list of <see cref="MutationAttribute" />s to modify the specified <paramref name="value" /> against.</param>
		/// <param name="value">The value to be mutated.</param>
		/// <returns>The value that has been modified according to any associated <see cref="MutationAttribute" />s and <see cref="IMutableObject" /> implementation.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="attributes" /> is <c>null</c>.</exception>
		private static T GetMutatedValue<T>(IMutationContext context, IEnumerable<MutationAttribute> attributes, T value)
		{
			if (context == null) {
				throw new ArgumentNullException(nameof(context));
			}

			if (attributes == null) {
				throw new ArgumentNullException(nameof(attributes));
			}

			foreach (var attribute in attributes.OrderBy(x => x.Priority)) {
				value = (T)attribute.Mutate(value, context);
			}

			return value;
		}

		/// <summary>
		///		Mutates the specified value against the specified property of the instance associated with the current context.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <typeparam name="P">The property type to consult for <see cref="MutationAttribute" />s.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="property">The property info that describes the value to be modified.</param>
		/// <param name="inferValue">If <c>true</c>, the current <paramref name="property" /> value will be mutated in place of specified <paramref name="value" />.</param>
		/// <param name="value">The value to be mutated when <paramref name="inferValue" /> is <c>false</c>.</param>
		/// <returns>The value that has been modified according to any <see cref="MutationAttribute" />s associated with the specified property.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="property" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">When the <see cref="MemberInfo.Name" /> of <paramref name="context" /> is not a valid property.</exception>
		private static P GetMutatedProperty<T, P>(MutationContext<T> context, PropertyInfo property, bool inferValue, P value = default(P))
		{
			if (context == null) {
				throw new ArgumentNullException(nameof(context));
			}

			if (property == null) {
				throw new ArgumentNullException(nameof(property));
			}

			if (inferValue) {
				value = (P)property.GetValue(context.ObjectInstance);
			}

			// Throw if a value cannot be assigned to this property.
			EnsureValidPropertyType(property.Name, AttributeStore.Instance.GetPropertyType(context, property), value);

			// Create a new context using the existing context's IServiceProvider and items, so that we will not overwrite the properties of the existing instance.
			var propertyContext = new MutationContext<T>(context.ObjectInstance, AttributeStore.Instance.GetPropertyAttributes(context, property), context.Items, context);

			var attributes = propertyContext.Attributes.OfType<MutationAttribute>();

			if (attributes.Any()) {
				value = GetMutatedValue(propertyContext, attributes, value);

				if (inferValue && CanBeAssigned(property.PropertyType, value)) {
					property.SetValue(context.ObjectInstance, value);
				}
			}

			return value;
		}

		/// <summary>
		///		Mutates an object instance against the current context.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="inferValue">If <c>true</c>, the <see cref="MutationContext{T}.ObjectInstance" /> will be mutated in place of specified <paramref name="instance" />.</param>
		/// <param name="instance">The instance to be modified.</param>
		/// <returns>The object whose value and properties has been modified according to any associated <see cref="MutationAttribute" />s and <see cref="IMutableObject" /> implementation.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		private static T GetMutatedObject<T>(MutationContext<T> context, bool inferValue, T instance = default(T))
			=> GetMutatedObject(context, context.Attributes.OfType<MutationAttribute>(), inferValue, instance);

		/// <summary>
		///		Mutates an object instance against the current context and the specified <see cref="MutationAttribute" />s.
		/// </summary>
		/// <typeparam name="T">The type to consult during mutation.</typeparam>
		/// <param name="context">Describes the type of object being mutated and provides services and context for mutation.</param>
		/// <param name="attributes">The list of <see cref="MutationAttribute" />s to modify the specified <paramref name="instance" /> against.</param>
		/// <param name="inferValue">If <c>true</c>, the <see cref="MutationContext{T}.ObjectInstance" /> will be mutated in place of specified <paramref name="instance" />.</param>
		/// <param name="instance">The instance to be modified.</param>
		/// <returns>The object whose value and properties has been modified according to any associated <see cref="MutationAttribute" />s and <see cref="IMutableObject" /> implementation.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is <c>null</c>.</exception>
		/// <exception cref="ArgumentNullException">When <paramref name="attributes" /> is <c>null</c>.</exception>
		private static T GetMutatedObject<T>(MutationContext<T> context, IEnumerable<MutationAttribute> attributes, bool inferValue, T instance = default(T))
		{
			if (context == null) {
				throw new ArgumentNullException(nameof(context));
			}

			if (attributes == null) {
				throw new ArgumentNullException(nameof(attributes));
			}

			instance = inferValue ? context.ObjectInstance : instance;

			// Step 1: Mutate the object properties' mutation attributes
			var properties = instance.GetType().GetRuntimeProperties().Where(p => AttributeStore.IsPublic(p) && !p.GetIndexParameters().Any());

			foreach (var property in properties) {
				GetMutatedProperty<T, object>(context, property, true);
			}

			// Step 2: Mutate the object's mutation attributes
			instance = GetMutatedValue(context, attributes, instance);

			// Step 3: Test for IMutableObject implementation
			(instance as IMutableObject)?.Mutate(context);

			return instance;
		}

		/// <summary>
		///		Gets the corresponding <see cref="PropertyInfo" /> from an <see cref="Expression" />.
		/// </summary>
		/// <param name="property">The expression that selects the property to get info on.</param>
		/// <returns>The property info collected from the expression.</returns>
		private static PropertyInfo GetPropertyInfo(Expression property)
		{
			if (property is LambdaExpression) {
				property = ((LambdaExpression)property).Body;
			}

			switch (property.NodeType) {
				case ExpressionType.MemberAccess: {
					return (PropertyInfo)((MemberExpression)property).Member;
				}

				default: {
					throw new ArgumentException($"The expression doesn't indicate a valid property. [ {property} ]");
				}
			}
		}

		/// <summary>
		///		Determines whether the given value can legally be assigned to the given property.
		/// </summary>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="propertyType">The type of the property.</param>
		/// <param name="value">The value. Null is permitted only if the property will accept it.</param>
		/// <exception cref="ArgumentException"> is thrown if <paramref name="value" /> is the wrong type for this property.</exception>
		private static void EnsureValidPropertyType(string propertyName, Type propertyType, object value)
		{
			if (!CanBeAssigned(propertyType, value)) {
				throw new ArgumentException($"The value for property '{propertyName}' must be of type '{propertyType}'.", nameof(value));
			}
		}

		/// <summary>
		///		Determine whether the given value can legally be assigned into the specified type.
		/// </summary>
		/// <param name="destinationType">The destination <see cref="Type" /> for the value.</param>
		/// <param name="value">The value to test to see if it can be assigned as the Type indicated by <paramref name="destinationType" />.</param>
		/// <returns><c>true</c> if the assignment is legal, otherwise, <c>false</c>.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="destinationType" /> is <c>null</c>.</exception>
		private static bool CanBeAssigned(Type destinationType, object value)
			=> value == null

				// Null can be assigned only to reference types or Nullable or Nullable<>
				? !destinationType.GetTypeInfo().IsValueType || (
						destinationType.GetTypeInfo().IsGenericType && destinationType.GetGenericTypeDefinition() == typeof(Nullable<>)
					)

				// Not null -- be sure it can be cast to the right type
				: destinationType.GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo());

		#endregion Private Methods
	}
}
