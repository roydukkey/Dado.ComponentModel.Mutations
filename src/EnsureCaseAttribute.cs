// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;

namespace System.ComponentModel.DataMutations
{
	/// <summary>
	///		Enumeration of casing options that may be used for the mutation of <see cref="EnsureCaseAttribute" />s.
	/// </summary>
	public enum CaseOptions
	{
		/// <summary>
		///		Lowercase
		/// </summary>
		Lower = 1 << 0,

		/// <summary>
		///		Uppercase
		/// </summary>
		Upper = 1 << 1,

#if !FEATURE_CORECLR
		/// <summary>
		///		Title Case
		/// </summary>
		Title = 1 << 2
#endif
	}

	/// <summary>
	///		Used to mutated the specified string to a specified case.
	/// </summary>
#if !FEATURE_CORECLR
	/// <remarks>
	///		Generally, title casing converts the first character of a word to uppercase and the rest of the characters to lowercase. However, this method does not currently provide proper casing to convert a word that is entirely uppercase, such as an acronym.
	/// </remarks>
#endif
	[AttributeUsage(AttributeTargets.Property)]
	public class EnsureCaseAttribute : MutationAttribute
	{
		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="EnsureCaseAttribute" /> class.
		/// </summary>
		/// <param name="caseOption">The desired case of the string after mutation.</param>
		public EnsureCaseAttribute(CaseOptions caseOption)
		{
			Case = caseOption;
		}

		#endregion Constructors

		#region Properties

		/// <summary>
		///		Gets or sets the <see cref="Globalization.CultureInfo" /> to be used when determining the appropriate case.
		/// </summary>
		public CultureInfo CultureInfo { get; set; } = CultureInfo.CurrentCulture;

		/// <summary>
		///		Gets the desired case of the string after mutation.
		/// </summary>
		public CaseOptions Case { get; private set; }

		#endregion Properties

		#region Protected Methods

		/// <summary>
		///		Implement the mutation logic for this <see cref="EnsureCaseAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The specified <paramref name="value" /> converted to the specified <see cref="Case" />.</returns>
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value is String) {
				var newValue = value.ToString();

				if (Case.HasFlag(CaseOptions.Upper)) {
					return CultureInfo.TextInfo.ToUpper(newValue);
				}
#if !FEATURE_CORECLR
				else if (Case.HasFlag(CaseOptions.Title)) {
					return CultureInfo.TextInfo.ToTitleCase(newValue);
				}
#endif
				else {
					return CultureInfo.TextInfo.ToLower(newValue);
				}
			}

			return value;
		}

		#endregion Protected Methods
	}
}
