// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace System.ComponentModel.DataMutations
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

		private readonly Dictionary<object, object> _items = new Dictionary<object, object>();
		private readonly T _objectInstance;
		private Func<Type, object> _serviceProvider;

		#endregion Fields

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" /> and an property bag of <paramref name="items" />.
		/// </summary>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <param name="items">A set of key/value pairs to make available to consumers via <see cref="Items" />. The set of key/value pairs will be copied into a new dictionary, preventing consumers from modifying the original dictionary.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		public MutationContext(T instance, IDictionary<object, object> items) : this(instance, items, null) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" /> and an <paramref name="serviceProvider" />.
		/// </summary>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <param name="serviceProvider">A <see cref="IServiceProvider" /> to use when <see cref="GetService" /> is called.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		public MutationContext(T instance, IServiceProvider serviceProvider) : this(instance, null, serviceProvider) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" />, an <paramref name="serviceProvider" />, and an property bag of <paramref name="items" />.
		/// </summary>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <param name="items">A set of key/value pairs to make available to consumers via <see cref="Items" />. The set of key/value pairs will be copied into a new dictionary, preventing consumers from modifying the original dictionary.</param>
		/// <param name="serviceProvider">A <see cref="IServiceProvider" /> to use when <see cref="GetService" /> is called.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		public MutationContext(T instance, IDictionary<object, object> items, IServiceProvider serviceProvider) : this(instance)
		{
			if (serviceProvider != null) {
				InitializeServiceProvider(serviceType => serviceProvider.GetService(serviceType));
			}

			if (items != null) {
				_items = new Dictionary<object, object>(items);
			}
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="MutationContext{T}" /> class for a given object <paramref name="instance" />.
		/// </summary>
		/// <param name="instance">The instance to be modified during mutation.</param>
		/// <exception cref="ArgumentNullException">When <paramref name="instance" /> is <c>null</c>.</exception>
		public MutationContext(T instance)
		{
			if (instance == null) {
				throw new ArgumentNullException(nameof(instance));
			}

			_objectInstance = instance;
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
		public T ObjectInstance => _objectInstance;

		/// <summary>
		///		Gets the instance being mutated.
		/// </summary>
		object IMutationContext.ObjectInstance => _objectInstance;

		/// <summary>
		///		Gets the dictionary of key/value pairs associated with this context.
		/// </summary>
		/// <value>
		///		This property will never be <c>null</c>, but the dictionary may be empty. Changes made to items in this dictionary will never affect the original dictionary specified in the constructor.
		/// </value>
		public IDictionary<object, object> Items => _items;

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
			=> _serviceProvider != null
				? _serviceProvider(serviceType)
				: null;

		#endregion Public Methods
	}
}
