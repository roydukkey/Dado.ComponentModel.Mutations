// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutate the specified string or char to a lowercase.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ToLowerAttribute : MutationAttribute
	{
		#region Properties

		/// <summary>
		///		Gets or sets the <see cref="Globalization.CultureInfo" /> to be used when determining the appropriate case.
		/// </summary>
		public CultureInfo CultureInfo { get; set; } = CultureInfo.CurrentCulture;

		/// <summary>
		///		Gets or sets a value indicating the priority that determines the order in which <see cref="MutationAttribute" />s are evaluated. Defaults to <c>30</c>.
		/// </summary>
		public override int Priority { get; set; } = 30;

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implements the mutation logic for this <see cref="ToLowerAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The specified <paramref name="value" /> converted to lowercase.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value != null) {
				var newString = value as string;

				if (newString != null) {
					return CultureInfo.TextInfo.ToLower(newString);
				}

				if (value is Char) {
					var newChar = (char)value;

					return CultureInfo.TextInfo.ToLower(newChar);
				}
			}

			return value;
		}

		#endregion Protected Methods
	}
}
