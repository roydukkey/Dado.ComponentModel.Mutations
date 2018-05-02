// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class ToUpperAttributeTests
	{
		[Fact]
		public static void MutateTransformsStringToUppercase()
		{
			var attribute = new ToUpperAttribute();

			Assert.Equal("UPPERCASE", attribute.Mutate("uppercase"));
		}

		[Fact]
		public static void MutateTransformsCharToUppercase()
		{
			var attribute = new ToUpperAttribute();

			Assert.Equal('U', attribute.Mutate('u'));
		}
	}
}
