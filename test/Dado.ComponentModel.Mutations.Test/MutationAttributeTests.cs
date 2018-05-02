// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Dado.ComponentModel.DataMutations.Test.Extensions;
using System;
using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	#region Testing Components

	public class MutationAttributeMinimalImpl : MutationAttribute
	{
		protected override object MutateValue(object value, IMutationContext context)
			=> value;
	}

	public class MutationAttributeRequiresContext : MutationAttributeMinimalImpl
	{
		public override bool RequiresContext => true;
	}

	#endregion Testing Components

	public class MutationAttributeTests
	{
		[Fact]
		public static void DefaultValueOfRequiresMutationContextIsFalse()
		{
			var attribute = new MutationAttributeMinimalImpl();
			Assert.False(attribute.RequiresContext);
		}

		[Fact]
		public static void MutateThrowsIfContextIsNullWhenRequiresContextIsTrue()
		{
			var attribute = new MutationAttributeRequiresContext();

			AssertExtensions.Throws<ArgumentNullException>("context",
				() => attribute.Mutate(null, null)
			);
		}
	}
}
