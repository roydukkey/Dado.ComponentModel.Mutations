// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Describes the context in which mutation is performed.
	/// </summary>
	/// <remarks>
	///		This class contains information describing the instance on which mutation is performed.
	///		<para>
	///			An <see cref="Items" /> property bag is available for additional contextual information about the mutation. Values stored in <see cref="Items" /> will be available to mutation methods that use this <see cref="MutationContext{T}" />.
	///		</para>
	/// </remarks>
	/// <typeparam name="T">The type to consult during mutation.</typeparam>
	public sealed class MutationContext<T> : IMutationContext
	{
		#region Fields

		private readonly Dictionary<object, object> _items;
		private Func<Type, object> _serviceProvider;

		#endregion Fields

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" /> and a property bag of <paramref name="items" />.
		/// </summary>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <param name="items">A set of key/value pairs to make available to consumers via <see cref="Items" />. The set of key/value pairs will be copied into a new dictionary, preventing consumers from modifying the original dictionary.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		public MutationContext(T instance, IDictionary<object, object> items) : this(instance, null, items, null) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" /> and a <paramref name="serviceProvider" />.
		/// </summary>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <param name="serviceProvider">A <see cref="IServiceProvider" /> to use when <see cref="GetService" /> is called.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		public MutationContext(T instance, IServiceProvider serviceProvider) : this(instance, null, null, serviceProvider) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" />, a <paramref name="serviceProvider" />, and a property bag of <paramref name="items" />.
		/// </summary>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <param name="items">A set of key/value pairs to make available to consumers via <see cref="Items" />. The set of key/value pairs will be copied into a new dictionary, preventing consumers from modifying the original dictionary.</param>
		/// <param name="serviceProvider">A <see cref="IServiceProvider" /> to use when <see cref="GetService" /> is called.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		public MutationContext(T instance, IDictionary<object, object> items, IServiceProvider serviceProvider) : this(instance, null, items, serviceProvider) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" />.
		/// </summary>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		public MutationContext(T instance) : this(instance, null, null, null) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" />, the current type or property <paramref name="attributes" />, a <paramref name="serviceProvider" />, and a property bag of <paramref name="items" />.
		/// </summary>
		/// <remarks>
		///		When <paramref name="attributes" /> is null, the attributes of the <paramref name="instance" /> will be used.
		///	</remarks>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <param name="attributes">A enumeration of attributes associated to the current type or property.</param>
		/// <param name="items">A set of key/value pairs to make available to consumers via <see cref="Items" />. The set of key/value pairs will be copied into a new dictionary, preventing consumers from modifying the original dictionary.</param>
		/// <param name="serviceProvider">A <see cref="IServiceProvider" /> to use when <see cref="GetService" /> is called.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		internal MutationContext(T instance, IEnumerable<Attribute> attributes, IDictionary<object, object> items, IServiceProvider serviceProvider)
		{
			if (instance == null) {
				throw new ArgumentNullException(nameof(instance));
			}

			ObjectInstance = instance;
			Attributes = new List<Attribute>(attributes ?? AttributeStore.Instance.GetTypeAttributes(this));

			if (serviceProvider != null) {
				InitializeServiceProvider(serviceType => serviceProvider.GetService(serviceType));
			}

			_items = items != null ? new Dictionary<object, object>(items) : new Dictionary<object, object>();
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		///		Gets the instance being mutated. While it will not be <c>null</c>, the state of the instance is indeterminate as it might only be partially initialized during mutation.
		///		<para>
		///			Consume this instance with caution!
		///		</para>
		/// </summary>
		/// <remarks>
		///		During mutation, especially property-level mutation, the instance might be in an indeterminate state. For example, the property being mutated, as well as other properties on the instance might not have been updated to their new values.
		/// </remarks>
		public T ObjectInstance { get; }

		/// <summary>
		///		Gets the instance being mutated.
		/// </summary>
		object IMutationContext.ObjectInstance => ObjectInstance;

		/// <summary>
		///		Gets the dictionary of key/value pairs associated with this context.
		/// </summary>
		/// <value>
		///		This property will never be <c>null</c>, but the dictionary may be empty. Changes made to items in this dictionary will never affect the original dictionary specified in the constructor.
		/// </value>
		public IDictionary<object, object> Items => _items;

		/// <summary>
		///		Gets the attributes associated with this context.
		/// </summary>
		public IEnumerable<Attribute> Attributes { get; }

		#endregion Properties

		#region Public Methods

		/// <summary>
		///		Initializes the <see cref="MutationContext{T}" /> with a service provider that can return service instances by <see cref="Type" /> when <see cref="GetService" /> is called.
		/// </summary>
		/// <param name="serviceProvider">A <see cref="Func{T, TResult}" /> that can return service instances given the desired <see cref="Type" /> when <see cref="GetService" /> is called. If it is <c>null</c>, <see cref="GetService" /> will always return <c>null</c>.</param>
		public void InitializeServiceProvider(Func<Type, object> serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		/// <summary>
		///		Returns the service that provides custom mutation.
		/// </summary>
		/// <param name="serviceType">The type of the service needed.</param>
		/// <returns>An instance of that service or <c>null</c> if it is not available.</returns>
		public object GetService(Type serviceType)
			=> _serviceProvider?.Invoke(serviceType);

		#endregion Public Methods
	}
}
