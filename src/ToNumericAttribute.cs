// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text.RegularExpressions;

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutated a string to allow only numeric characters.
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
		///		Implement the mutation logic for this <see cref="ToNumericAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The resulting mutated value in the specified numeric format.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value != null) {
				var newString = value as string;

				if (value != null) {
					if (String.IsNullOrWhiteSpace(newString)) {
						return null;
					}

					string rgxMod = "";

					if (PreserveFloatingPoint) {
						rgxMod = ".";

						int search = newString.LastIndexOf('.');

						if (search > -1) {
							newString = newString.Substring(0, search).Replace(".", "") + newString.Substring(search);
						}
					}

					if (PreserveSign) {
						rgxMod += "+-";
					}

					newString = Regex.Replace(newString, $"[^0-9{rgxMod}]", "");

					if (PreserveSign && newString.Length > 0) {
						newString = newString[0] + Regex.Replace(newString.Substring(1), "[+-]", "");

						if (newString.Length == 1 && newString.IndexOfAny(new[] { '+', '-' }) == 0) {
							return null;
						}
					}

					return String.IsNullOrEmpty(newString) ? null : newString;
				}
			}

			return value;
		}

		#endregion Protected Methods
	}
}
