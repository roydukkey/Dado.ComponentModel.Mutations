// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.RegularExpressions;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutate a string to allow only numeric characters.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class ToNumericAttribute : MutationAttribute
	{
		#region Properties

		/// <summary>
		///		Gets or sets a value indicating whether a floating point indication (.) should be preserved during mutation.
		/// </summary>
		public bool PreserveFloatingPoint { get; set; }

		/// <summary>
		///		Gets or sets a value indicating whether a sign indication (±) should be preserved during mutation.
		/// </summary>
		public bool PreserveSign { get; set; }

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implements the mutation logic for this <see cref="ToNumericAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The resulting mutated value in the specified numeric format.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value is string valueAsString) {
				if (String.IsNullOrWhiteSpace(valueAsString)) {
					return null;
				}

				string rgxMod = "";

				if (PreserveFloatingPoint) {
					rgxMod = ".";

					int search = valueAsString.LastIndexOf('.');

					if (search > -1) {
						valueAsString = valueAsString.Substring(0, search).Replace(".", "") + valueAsString.Substring(search);
					}
				}

				if (PreserveSign) {
					rgxMod += "+-";
				}

				valueAsString = Regex.Replace(valueAsString, $"[^0-9{rgxMod}]", "");

				if (PreserveSign && valueAsString.Length > 0) {
					valueAsString = valueAsString[0] + Regex.Replace(valueAsString.Substring(1), "[+-]", "");

					if (valueAsString.Length == 1 && valueAsString.IndexOfAny(new[] { '+', '-' }) == 0) {
						return null;
					}
				}

				return String.IsNullOrEmpty(valueAsString) ? null : valueAsString;
			}
			
			return value;
		}

		#endregion Protected Methods
	}
}
