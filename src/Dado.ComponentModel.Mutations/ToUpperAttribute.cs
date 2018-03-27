// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutate the specified string or char to a uppercase.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ToUpperAttribute : MutationAttribute
	{
		#region Properties

		/// <summary>
		///		Gets or sets the <see cref="System.Globalization.CultureInfo" /> to be used when determining the appropriate case.
		/// </summary>
		public CultureInfo CultureInfo { get; set; } = CultureInfo.CurrentCulture;

		/// <summary>
		///		Gets or sets a value indicating the priority that determines the order in which <see cref="MutationAttribute" />s are evaluated. Defaults to <c>30</c>.
		/// </summary>
		public override int Priority { get; set; } = 30;

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implements the mutation logic for this <see cref="ToUpperAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The specified <paramref name="value" /> converted to uppercase.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value is string valueAsString && !String.IsNullOrWhiteSpace(valueAsString)) {
				return CultureInfo.TextInfo.ToUpper(valueAsString);
			}

			if (value is char valueAsChar) {
				return CultureInfo.TextInfo.ToUpper(valueAsChar);
			}

			return value;
		}

		#endregion Protected Methods
	}
}
