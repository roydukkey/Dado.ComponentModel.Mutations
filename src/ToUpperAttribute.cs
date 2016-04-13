// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutated the specified string or char to a uppercase.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ToUpperAttribute : MutationAttribute
	{
		#region Properties

		/// <summary>
		///		Gets or sets the <see cref="Globalization.CultureInfo" /> to be used when determining the appropriate case.
		/// </summary>
		public CultureInfo CultureInfo { get; set; } = CultureInfo.CurrentCulture;

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implement the mutation logic for this <see cref="ToUpperAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The specified <paramref name="value" /> converted to uppercase.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value != null) {
				var newString = value as string;

				if (value != null) {
					return CultureInfo.TextInfo.ToUpper(newString);
				}

				if (value is Char) {
					var newChar = (char)value;

					return CultureInfo.TextInfo.ToUpper(newChar);
				}
			}

			return value;
		}

		#endregion Protected Methods
	}
}
