// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutate a string replacing all strings that match a regular expression pattern with a specified replacement string.
	/// </summary>
	/// <remarks>
	///		If <see cref="Replacement" /> is <c>null</c>, each match of the specified <see cref="Patterns" /> are removed.
	///		<para>
	///			The <see cref="Replacement" /> property specifies the string that is to replace each match in the current value. <see cref="Replacement" /> can consist of any combination of literal text and substitutions. For example, the replacement pattern, <code>*${test}b</code>, inserts the string, "a*", followed by the substring that is matched by the test capturing group, if any, followed by the string, "b". An asterisk (*) is not recognized as a metacharacter within a replacement pattern.
	///		</para>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class RegexReplaceAttribute : MutationAttribute
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="RegexReplaceAttribute" /> class.
		/// </summary>
		/// <param name="pattern">The regular expression pattern to match.</param>
		/// <param name="additional">Additional regular expression pattern to match.</param>
		public RegexReplaceAttribute(string pattern, params string[] additional)
		{
			Patterns = new[] { pattern };

			if (additional != null) {
				Patterns = Patterns.Concat(additional);
			}
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		///		Gets the regular expression pattern to match in a string.
		/// </summary>
		public IEnumerable<string> Patterns { get; private set; }

		/// <summary>
		///		Gets or sets the replacement pattern that will be used to replace each match of the specified <see cref="Patterns" />.
		/// </summary>
		public string Replacement { get; set; } = "";

		/// <summary>
		///		Gets or sets a bitwise combination of the enumeration values that modify the regular expression.
		/// </summary>
		public RegexOptions Options { get; set; }

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implements the mutation logic for this <see cref="RegexReplaceAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>A new string that is identical to the input string, except that the replacement string takes the place of each matched string. If the regular expression pattern is not matched in the current instance, the method returns the current instance unchanged.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value != null) {
				var newString = value as string;

				if (newString != null) {
					foreach (string pattern in Patterns) {
						value = new Regex(pattern, Options).Replace(newString, Replacement);
					}
				}
			}

			return value;
		}

		#endregion Protected Methods
	}
}
