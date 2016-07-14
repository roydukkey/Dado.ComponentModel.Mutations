// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		Enumeration of trimming options that may be used for the mutation of <see cref="TrimAttribute" />s.
	/// </summary>
	[Flags]
	public enum TrimOptions
	{
		/// <summary>
		///		Trim leading and trailing occurrences.
		/// </summary>
		All = 1 << 0,

		/// <summary>
		///		Trim leading occurrences.
		/// </summary>
		FromStart = 1 << 1,

		/// <summary>
		///		Trim trailing occurrences.
		/// </summary>
		FromEnd = 1 << 2
	}

	/// <summary>
	///		Used to mutated a specified string in which all leading and/or trailing occurrences of a set of specified characters are removed.
	/// </summary>
	/// <remarks>
	///		The Trim method removes from the specified string all leading and/or trailing characters that are specified in <see cref="Characters" />. Each leading and trailing trim operation stops when a character that is not in <see cref="Characters" /> is encountered. For example, if the string is "123abc456xyz789" and <see cref="Characters" /> contains the digits from "1" through "9", the resulting string is "abc456xyz".
	///		<para>
	///			If the specified string equals <see cref="String.Empty" /> or all the characters in the string consist of characters in the <see cref="Characters" /> array, the resulting string is <see cref="String.Empty" />.
	///		</para>
	///		<para>
	///			If <see cref="Characters" /> is <c>null</c> or an empty array, mutation removes any leading or trailing characters that result in <see cref="Char.IsWhiteSpace(char)" /> returning <c>true</c> when the character is passed to the method.
	///		</para>
	/// </remarks>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class TrimAttribute : MutationAttribute
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="TrimAttribute" /> class.
		/// </summary>
		/// <param name="characters">An array of Unicode characters to remove, or <c>null</c>.</param>
		public TrimAttribute(params char[] characters)
		{
			Characters = characters;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		///		Gets an array of Unicode characters to remove.
		/// </summary>
		public char[] Characters { get; private set; }

		/// <summary>
		///		Gets or sets a value indicating the trimming direction.
		/// </summary>
		public TrimOptions Direction { get; set; }

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implements the mutation logic for this <see cref="TrimAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The string that remains after all occurrences of the characters in the the <see cref="Characters" /> array are removed from the start and/or end of the specified string. If the <see cref="Characters" /> array is null or an empty array, white-space characters are removed instead.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value != null) {
				var newString = value as string;

				if (newString != null) {
					if (Direction.IsSingleFlag()) {
						if (Direction.HasFlag(TrimOptions.FromStart)) {
							return newString.TrimStart(Characters);
						}
						else if (Direction.HasFlag(TrimOptions.FromEnd)) {
							return newString.TrimEnd(Characters);
						}
					}

					return newString.Trim(Characters);
				}
			}

			return value;
		}

		#endregion Protected Methods
	}
}
