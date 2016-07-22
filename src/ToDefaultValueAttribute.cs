// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutate the specified values to the associated <see cref="DefaultValueAttribute.Value" /> or the type's default value.
	/// </summary>
	/// <remarks>
	///		The <see cref="ToDefaultValueAttribute" /> replaces any values that are specified in <see cref="Values" /> with the associated <see cref="DefaultValueAttribute.Value" /> or the type's default value.
	///		<para>
	///			If no <see cref="Values" /> are specified, the type's default value will be replaced with the associated <see cref="DefaultValueAttribute.Value" />.
	///		</para>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property)]
	public class ToDefaultValueAttribute : MutationAttribute
	{
		#region Constructors

		/// <summary>
		/// 	Initializes a new instance of the <see cref="ToDefaultValueAttribute" /> class.
		/// </summary>
		/// <param name="values">An array of values that should be made default.</param>
		public ToDefaultValueAttribute(params object[] values)
		{
			Values = values == null
				? new[] { values }
				: values.Count() == 0
					? null
					: values;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		///		Gets the values to make default.
		/// </summary>
		public IEnumerable<object> Values { get; private set; }

		/// <summary>
		///		Gets or sets a value indicating the priority that determines the order in which <see cref="MutationAttribute" />s are evaluated. Defaults to <c>50</c>.
		/// </summary>
		public override int Priority { get; set; } = 50;

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
			object defaultValue = null;

			if (value != null) {
				var type = value.GetType();

				if (type.GetTypeInfo().IsValueType) {
					// Is there better way to get the default value?
					defaultValue = Activator.CreateInstance(type);
				}
			}

			if (Values == null) {
				if (value == defaultValue || value.Equals(defaultValue)) {
					value = TryGetAttributeValue(context, defaultValue);
				}
			}
			else {
				foreach (var testValue in Values) {
					if (value == testValue || (value != null && value.Equals(testValue))) {
						value = TryGetAttributeValue(context, defaultValue);
					}
				}
			}

			return value;
		}

		#endregion Protected Methods

		#region Private Methods

		/// <summary>
		///		Tries to determine the default value from a <see cref="DefaultValueAttribute" />.
		/// </summary>
		/// <param name="context">Describes the value being mutated and provides services and context for mutation.</param>
		/// <param name="defaultValue">The value to be used when a value is not specified by an attribute.</param>
		/// <returns>The value of an associated <see cref="DefaultValueAttribute" />, otherwise the value specified by <paramref name="defaultValue" />.</returns>
		private object TryGetAttributeValue(IMutationContext context, object defaultValue)
		{
			var attribute = context?.Attributes.FirstOrDefault(x => x is DefaultValueAttribute) as DefaultValueAttribute;

			return attribute != null ? attribute.Value : defaultValue;
		}

		#endregion Private Methods
	}
}
