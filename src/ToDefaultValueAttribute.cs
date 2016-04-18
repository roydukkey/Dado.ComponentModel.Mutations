// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutated the specified values to the associated <see cref="DefaultValueAttribute.Value" /> or the type's default value.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ToDefaultValueAttribute : MutationAttribute
	{
		#region Constructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="ToDefaultValueAttribute" /> class.
		/// </summary>
		/// <param name="value">The value that should be made default.</param>
		/// <param name="additional">Additional values to make default.</param>
		public ToDefaultValueAttribute(object value, params object[] additional)
		{
			Values = new[] { value }.Concat(additional);
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		///		Gets the values to make default.
		/// </summary>
		public IEnumerable<object> Values { get; private set; }

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implements the mutation logic for this <see cref="ToDefaultValueAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The type's default value when the specified <paramref name="value" /> is in <see cref="Values" />.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			foreach (var testValue in Values) {
				if (testValue.Equals(value)) {
					var attribute = context?.Attributes.FirstOrDefault(x => x is DefaultValueAttribute) as DefaultValueAttribute;

					if (attribute != null) {
						value = attribute.Value;
					}
					else {
						// Is there better way? Should the use a DefaultValueAttribute be required when using this attribute?
						var type = value.GetType();

						value = type.GetTypeInfo().IsValueType
							? Activator.CreateInstance(type)
							: null;
					}

					break;
				}
			}

			return value;
		}

		#endregion Protected Methods
	}
}
