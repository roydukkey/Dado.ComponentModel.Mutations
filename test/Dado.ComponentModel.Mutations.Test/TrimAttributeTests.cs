// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class TrimAttributeTests
	{
		[Fact]
		public static void MutateTrimsWhiteSpace()
		{
			var attribute = new TrimAttribute();

			Assert.Equal("string", attribute.Mutate("  string \t \n "));
		}

		[Fact]
		public static void MutateTrimsSingleChar()
		{
			var attribute = new TrimAttribute('a');

			Assert.Equal("b", attribute.Mutate("aba"));
		}

		[Fact]
		public static void MutateTrimsMultipleChars()
		{
			var attribute = new TrimAttribute('a', 'd', 'b', 'c');

			Assert.Equal("e", attribute.Mutate("abcdedcba"));
		}

		[Fact]
		public static void MutateTrimsDirectionAll()
		{
			var attribute = new TrimAttribute('a') {
				Direction = TrimOptions.All
			};

			Assert.Equal("b", attribute.Mutate("aba"));
		}

		[Fact]
		public static void MutateTrimsFromStart()
		{
			var attribute = new TrimAttribute('a') {
				Direction = TrimOptions.FromStart
			};

			Assert.Equal("ba", attribute.Mutate("aba"));
		}

		[Fact]
		public static void MutateTrimsFromEnd()
		{
			var attribute = new TrimAttribute('a') {
				Direction = TrimOptions.FromEnd
			};

			Assert.Equal("ab", attribute.Mutate("aba"));
		}
	}
}
