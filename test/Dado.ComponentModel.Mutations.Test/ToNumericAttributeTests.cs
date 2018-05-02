// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class ToNumericAttributeTests
	{
		[Theory]
		[InlineData("ab1.23&4c5+d.67f[8-e9)0", "1234567890")]
		[InlineData("-ab1.23&4c5+d.67f[8-e9)0", "1234567890")]
		[InlineData("ab+1.23&4c5+d.67f[8-e9)0", "1234567890")]
		public static void MutateTransformsToNumericString(string input, string output)
		{
			var attribute = new ToNumericAttribute();

			Assert.Equal(output, attribute.Mutate(input));
		}

		[Theory]
		[InlineData("ab1.23&4c5+d.67f[8-e9)0", "1234567890")]
		[InlineData("-ab1.23&4c5+d.67f[8-e9)0", "-1234567890")]
		[InlineData("ab+1.23&4c5+d.67f[8-e9)0", "+1234567890")]
		public static void MutateTransformsToNumericStringPreservingSign(string input, string output)
		{
			var attribute = new ToNumericAttribute() {
				PreserveSign = true
			};

			Assert.Equal(output, attribute.Mutate(input));
		}

		[Theory]
		[InlineData("ab1.23&4c5+d.67f[8-e9)0", "12345.67890")]
		[InlineData("-ab1.23&4c5+d.67f[8-e9)0", "12345.67890")]
		[InlineData("ab+1.23&4c5+d.67f[8-e9)0", "12345.67890")]
		public static void MutateTransformsToNumericStringPreservingFloatingPoint(string input, string output)
		{
			var attribute = new ToNumericAttribute() {
				PreserveFloatingPoint = true
			};

			Assert.Equal(output, attribute.Mutate(input));
		}

		[Theory]
		[InlineData("ab1.23&4c5+d.67f[8-e9)0", "12345.67890")]
		[InlineData("-ab1.23&4c5+d.67f[8-e9)0", "-12345.67890")]
		[InlineData("ab+1.23&4c5+d.67f[8-e9)0", "+12345.67890")]
		public static void MutateTransformsToNumericStringPreservingSignAndFloatingPoint(string input, string output)
		{
			var attribute = new ToNumericAttribute() {
				PreserveSign = true,
				PreserveFloatingPoint = true
			};

			Assert.Equal(output, attribute.Mutate(input));
		}
	}
}
