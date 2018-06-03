// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutate a string data to the maximum allowable length according to the associated <see cref="StringLengthAttribute.MaximumLength" /> or <see cref="MaxLengthAttribute.Length" />.
	/// </summary>
	/// <remarks>
	///		Since <see cref="System.Attribute" />s cannot accept generics or lambdas, there is not a practical way to alter the length of collections maintaining their type information. Therefore, <see cref="CollectionDescriptor" /> can be used to extend <see cref="ApplyMaxLengthAttribute" /> through its protected constructor.
	///		<para>
	///			Expect <see cref="CollectionDescriptor" /> to be obsolete once generics or lambdas are implemented for <see cref="System.Attribute" />s.
	///		</para>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property)]
	public class ApplyMaxLengthAttribute : MutationAttribute
	{
		#region Fields

		private int _maximumLength = -1;
		private CollectionDescriptor _descriptor;

		#endregion Fields

		#region Constructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="ApplyMaxLengthAttribute" /> class.
		/// </summary>
		public ApplyMaxLengthAttribute() { }

		/// <summary>
		///		Initializes a new instance of the <see cref="ApplyMaxLengthAttribute" /> class.
		/// </summary>
		/// <param name="descriptor">Describe a collection should be mutated.</param>
		protected ApplyMaxLengthAttribute(CollectionDescriptor descriptor)
		{
			_descriptor = descriptor;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		///		A flag indicating the attribute requires a non-null <see cref="IMutationContext" /> to perform validation. Returns <c>true</c>.
		/// </summary>
		public override bool RequiresContext { get; protected set; } = true;

		#endregion Properties

		#region Public Methods

		/// <summary>
		///		Mutates the given value according to this <see cref="MutationAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="maximumLength">The maximum allowable length to apply to the string data.</param>
		/// <returns>The resulting mutated value.</returns>
		public object Mutate(object value, int maximumLength)
		{
			_maximumLength = maximumLength;
			RequiresContext = false;

			var mutatedValue = Mutate(value);

			RequiresContext = true;

			return mutatedValue;
		}

		#endregion Public Methods

		#region Protected Methods

		/// <summary>
		///		Implements the mutation logic for this <see cref="ApplyMaxLengthAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The truncated string data when the specified <paramref name="value" /> exceeds the maximum allowable length.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value != null) {
				if (
					_descriptor == null && value is string valueAsString &&
					(
						_maximumLength > -1 || TryGetStringLengthOrMaxLengthAttributeValue(context, out _maximumLength)
					) &&
					valueAsString.Length > _maximumLength
				) {
					value = valueAsString.Substring(0, _maximumLength);
				}
				else if (
					value is ICollection valueAsCollection &&
					(
						_maximumLength > -1 || TryGetMaxLengthAttributeValue(context, out _maximumLength)
					) &&
					valueAsCollection.Count > _maximumLength
				) {
					value = _descriptor?.CollectionMutator?.Invoke(_maximumLength, valueAsCollection);
				}
			}

			_maximumLength = -1;

			return value;
		}

		#endregion Protected Methods

		#region Private Methods

		/// <summary>
		///		Gets the maximum allowable length value from an associated <see cref="StringLengthAttribute" /> or <see cref="MaxLengthAttribute" />.
		/// </summary>
		/// <param name="context">Describes the value being mutated and provides services and context for mutation.</param>
		/// <param name="maxLength">When a <see cref="StringLengthAttribute" /> is found, contains its <see cref="StringLengthAttribute.MaximumLength" /> value, or when a <see cref="MaxLengthAttribute" /> is found, contains its <see cref="MaxLengthAttribute.Length" /> value; otherwise, the value is <c>-1</c>. This parameter is passed uninitialized.</param>
		/// <returns><c>true</c> if either a <see cref="StringLengthAttribute" /> or <see cref="MaxLengthAttribute" /> are found, otherwise <c>false</c>.</returns>
		private bool TryGetStringLengthOrMaxLengthAttributeValue(IMutationContext context, out int maxLength)
		{
			var attribute = context?.Attributes.OfType<StringLengthAttribute>().FirstOrDefault();

			if (attribute != null) {
				maxLength = attribute.MaximumLength;

				return true;
			}

			return TryGetMaxLengthAttributeValue(context, out maxLength);
		}

		/// <summary>
		///		Gets the maximum allowable length value from an associated <see cref="MaxLengthAttribute" />.
		/// </summary>
		/// <param name="context">Describes the value being mutated and provides services and context for mutation.</param>
		/// <param name="maxLength">When a <see cref="MaxLengthAttribute" /> is found, contains its <see cref="MaxLengthAttribute.Length" /> value; otherwise, the value is <c>-1</c>. This parameter is passed uninitialized.</param>
		/// <returns><c>true</c> if a <see cref="MaxLengthAttribute" /> is found, otherwise <c>false</c>.</returns>
		private bool TryGetMaxLengthAttributeValue(IMutationContext context, out int maxLength)
		{
			var attribute = context?.Attributes.OfType<MaxLengthAttribute>().FirstOrDefault();

			if (attribute != null) {
				maxLength = attribute.Length;

				return true;
			}

			maxLength = -1;

			return false;
		}

		#endregion Private Methods

		/// <summary>
		///		Used to describe how collections should be mutated.
		/// </summary>
		/// <remarks>
		///		This class will likely be obsolete once generics or lambdas are implemented for <see cref="System.Attribute" />s.
		/// </remarks>
		/// <seealso href="https://github.com/dotnet/csharplang/issues/124" />
		/// <seealso href="https://github.com/dotnet/csharplang/issues/343" />
		public sealed class CollectionDescriptor
		{
			#region Properties

			/// <summary>
			///		A function accepting a length and collection which returns a collection.
			/// </summary>
			public Func<int, IEnumerable, IEnumerable> CollectionMutator { get; private set; }

			#endregion Properties

			#region Public Methods

			/// <summary>
			///		Sets the <see cref="CollectionMutator" /> for an <see cref="ICollection" />.
			/// </summary>
			/// <typeparam name="T">The type of that describes an <see cref="ICollection" />.</typeparam>
			/// <param name="mutator">The function accepting a length and collection which returns a collection.</param>
			/// <returns>Returns this instance of the <see cref="CollectionDescriptor" />.</returns>
			public CollectionDescriptor SetCollectionMutator<T>(Func<int, T, T> mutator) where T : ICollection
			{
				CollectionMutator = (length, collection) => mutator.Invoke(length, (T)collection);

				return this;
			}

			/// <summary>
			///		Sets the <see cref="CollectionMutator" /> for a generic <see cref="ICollection{T}" />.
			/// </summary>
			/// <typeparam name="T">The type of the elements in the collection.</typeparam>
			/// <param name="mutator">The function accepting a length and collection which returns a collection.</param>
			/// <returns>Returns this instance of the <see cref="CollectionDescriptor" />.</returns>
			public CollectionDescriptor SetGenericCollectionMutator<T>(Func<int, ICollection<T>, ICollection<T>> mutator)
			{
				CollectionMutator = (length, collection) => mutator.Invoke(length, (ICollection<T>)collection);

				return this;
			}

			#endregion Public Methods
		}
	}
}
