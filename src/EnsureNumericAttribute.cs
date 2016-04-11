// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text.RegularExpressions;

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutated a string to allow only numeric characters.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class EnsureNumericAttribute : MutationAttribute
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
		///		Implement the mutation logic for this <see cref="EnsureNumericAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The resulting mutated value in the specified numeric format.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value is String) {
				var newValue = value.ToString();

				if (String.IsNullOrWhiteSpace(newValue)) {
					return null;
				}

				string rgxMod = "";

				if (PreserveFloatingPoint) {
					rgxMod = ".";

					int search = newValue.LastIndexOf('.');

					if (search > -1) {
						newValue = newValue.Substring(0, search).Replace(".", "") + newValue.Substring(search);
					}
				}

				if (PreserveSign) {
					rgxMod += "+-";
				}

				newValue = Regex.Replace(newValue, $"[^0-9{rgxMod}]", "");

				if (PreserveSign && newValue.Length > 0) {
					newValue = newValue[0] + Regex.Replace(newValue.Substring(1), "[+-]", "");

					if (newValue.Length == 1 && newValue.IndexOfAny(new[] { '+', '-' }) == 0) {
						return null;
					}
				}

				return String.IsNullOrEmpty(newValue) ? null : newValue;
			}

			return value;
		}

		#endregion Protected Methods
	}
}
