// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;
using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class RegexReplaceAttributeTests
	{
		[Theory]
		[InlineData("1Aa2Bb3Cc4", "1234", "[A-z]")]
		[InlineData("1Aa2Bb3Cc4", "AaBbCc", "[^A-z]")]
		public static void MutateReplacesPatternWithNothing(string input, string output, string pattern)
		{
			var attribute = new RegexReplaceAttribute(pattern);

			Assert.Equal(output, attribute.Mutate(input));
		}

		[Theory]
		[InlineData("1Aa2Bb3Cc4", "1a2b3c4", new[] { "[A-Z]" })]
		[InlineData("1Aa2Bb3Cc4", "ABC", new[] { "[^A-Z]" })]
		[InlineData("1Aa2B$b3Cc4", "a$bc", new[] { "[A-Z]", "[0-9]" })]
		public static void MutateReplacesPatternsWithNothing(string input, string output, string[] patterns)
		{
			var attribute = new RegexReplaceAttribute(patterns);

			Assert.Equal(output, attribute.Mutate(input));
		}

		[Theory]
		[InlineData("1Aa2Bb3Cc4", "1Xa2Xb3Xc4", "[A-Z]")]
		[InlineData("1Aa2Bb3Cc4", "XAXXBXXCXX", "[^A-Z]")]
		public static void MutateReplacesPatternWithReplacement(string input, string output, string pattern)
		{
			var attribute = new RegexReplaceAttribute(pattern) {
				Replacement = "X"
			};

			Assert.Equal(output, attribute.Mutate(input));
		}

		[Theory]
		[InlineData("1Aa2Bb3Cc4", "1Xa2Xb3Xc4", new[] { "[A-Z]" })]
		[InlineData("1Aa2Bb3Cc4", "XAXXBXXCXX", new[] { "[^A-Z]" })]
		[InlineData("1Aa2B$b3Cc4", "XXaXX$bXXcX", new[] { "[A-Z]", "[0-9]" })]
		public static void MutateReplacesPatternsWithReplacement(string input, string output, string[] patterns)
		{
			var attribute = new RegexReplaceAttribute(patterns) {
				Replacement = "X"
			};

			Assert.Equal(output, attribute.Mutate(input));
		}

		[Theory]
		[InlineData("1Aa2Bb3Cc4", "1XX2XX3XX4", "[A-Z]")]
		[InlineData("1Aa2Bb3Cc4", "XAaXBbXCcX", "[^A-Z]")]
		public static void MutateReplacesPatternWithReplacementAndOptions(string input, string output, string pattern)
		{
			var attribute = new RegexReplaceAttribute(pattern) {
				Replacement = "X",
				Options = RegexOptions.IgnoreCase
			};

			Assert.Equal(output, attribute.Mutate(input));
		}

		[Theory]
		[InlineData("1Aa2Bb3Cc4", "1XX2XX3XX4", new[] { "[A-Z]" })]
		[InlineData("1Aa2Bb3Cc4", "XAaXBbXCcX", new[] { "[^A-Z]" })]
		[InlineData("1Aa2B$b3Cc4", "XXXXX$XXXXX", new[] { "[A-Z]", "[0-9]" })]
		public static void MutateReplacesPatternsWithReplacementAndOptions(string input, string output, string[] patterns)
		{
			var attribute = new RegexReplaceAttribute(patterns) {
				Replacement = "X",
				Options = RegexOptions.IgnoreCase
			};

			Assert.Equal(output, attribute.Mutate(input));
		}
	}
}
