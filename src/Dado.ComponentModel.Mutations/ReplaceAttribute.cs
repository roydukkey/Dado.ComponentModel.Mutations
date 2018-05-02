// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Used to mutate a string so all occurrences of a specified strings are replaced with another specified string.
	/// </summary>
	/// <remarks>
	///		If <see cref="Replacement" /> is <c>null</c>, all occurrences of the specified <see cref="Antecedents" /> are removed.
	///		<para>
	///			This attribute performs an ordinal (case-sensitive and culture-insensitive) search to find the specified <see cref="Antecedents" />.
	///		</para>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class ReplaceAttribute : MutationAttribute
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="ReplaceAttribute" /> class.
		/// </summary>
		/// <param name="antecedent">The string to be replaced.</param>
		public ReplaceAttribute(string antecedent) : this(new[] { antecedent }) { }

		/// <summary>
		///		Initializes a new instance of the <see cref="ReplaceAttribute" /> class.
		/// </summary>
		/// <param name="antecedents">The strings to be replaced.</param>
		public ReplaceAttribute(params string[] antecedents)
		{
			Antecedents = antecedents;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		///		Gets the values to be replaced in a string.
		/// </summary>
		public IEnumerable<string> Antecedents { get; }

		/// <summary>
		///		Gets or sets the string to replace all occurrences of the specified <see cref="Antecedents" />.
		/// </summary>
		public string Replacement { get; set; }

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implements the mutation logic for this <see cref="ReplaceAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>A string that is equivalent to the current <paramref name="value" /> except that all instances of specified <see cref="Antecedents" /> are replaced with the value of <see cref="Replacement" />. If none of the <see cref="Antecedents" /> are found in the current <paramref name="value" />, the method returns the current <paramref name="value" /> unchanged.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value != null && value is string valueAsString) {
				foreach (string antecedent in Antecedents) {
					valueAsString = valueAsString.Replace(antecedent, Replacement);
				}

				value = valueAsString;
			}

			return value;
		}

		#endregion Protected Methods
	}
}
