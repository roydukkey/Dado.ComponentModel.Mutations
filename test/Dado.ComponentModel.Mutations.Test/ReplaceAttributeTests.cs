// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class ReplaceAttributeTests
	{
		[Theory]
		[InlineData("This is a nightm$are!", "$")]
		[InlineData("Thinites is a nightmare!", "nite" )]
		public static void MutateReplacesAntecedentWithNothing(string input, string antecedent)
		{
			var attribute = new ReplaceAttribute(antecedent);

			Assert.Equal("This is a nightmare!", attribute.Mutate(input));
		}

		[Theory]
		[InlineData("This is a nightm$are!", new[] { "$" })]
		[InlineData("Thinites is a nightmare!", new[] { "nite" })]
		[InlineData("Thinites is a nigbu&ckhtmare!", new[] { "nite", "bu&ck" })]
		public static void MutateReplacesAntecedentsWithNothing(string input, string[] antecedents)
		{
			var attribute = new ReplaceAttribute(antecedents);

			Assert.Equal("This is a nightmare!", attribute.Mutate(input));
		}

		[Theory]
		[InlineData("This is a night$re!", "$", "ma")]
		[InlineData("This is a nitemare!", "nite", "night")]
		public static void MutateReplacesAntecedentWithReplacement(string input, string antecedent, string replacement)
		{
			var attribute = new ReplaceAttribute(antecedent) {
				Replacement = replacement
			};

			Assert.Equal("This is a nightmare!", attribute.Mutate(input));
		}

		[Theory]
		[InlineData("This is a night$re!", new[] { "$" }, "ma")]
		[InlineData("This is a nitemare!", new[] { "nite" }, "night")]
		[InlineData("Those ^^ a nightmare!", new[] { "^^", "ose" }, "is")]
		public static void MutateReplacesAntecedentsWithReplacement(string input, string[] antecedents, string replacement)
		{
			var attribute = new ReplaceAttribute(antecedents) {
				Replacement = replacement
			};

			Assert.Equal("This is a nightmare!", attribute.Mutate(input));
		}
	}
}
