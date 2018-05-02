// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class ToLowerAttributeTests
	{
		[Fact]
		public static void MutateTransformsStringToLowercase()
		{
			var attribute = new ToLowerAttribute();

			Assert.Equal("lowercase", attribute.Mutate("LOWERCASE"));
		}

		[Fact]
		public static void MutateTransformsCharToLowercase()
		{
			var attribute = new ToLowerAttribute();

			Assert.Equal('l', attribute.Mutate('L'));
		}
	}
}
