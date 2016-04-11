// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace System.ComponentModel.DataMutations
{
	internal static class EnumExtensions
	{
		#region Public Methods

		/// <summary>
		///		Determines if the Enum contains only a single flag.
		/// </summary>
		/// <param name="source">The Enum to check for flags.</param>
		/// <returns><c>true</c> when only one flag is set, otherwise <c>false</c>.</returns>
		public static bool IsSingleFlag(this Enum source)
		{
			int value = Convert.ToInt32(source);
			return value != 0 && (value & (value - 1)) == 0;
		}

		#endregion Public Methods
	}
}
