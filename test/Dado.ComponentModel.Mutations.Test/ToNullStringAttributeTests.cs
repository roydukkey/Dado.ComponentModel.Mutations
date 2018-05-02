// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class ToNullStringAttributeTests
	{
		[Fact]
		public static void MutateTransformsEmptyStringToNull()
		{
			var attribute = new ToNullStringAttribute();

			Assert.Null(attribute.Mutate(""));
		}

		[Fact]
		public static void MutateTransformsWhiteSpaceToNull()
		{
			var attribute = new ToNullStringAttribute();

			Assert.Null(attribute.Mutate(" \t \n "));
		}
	}
}
