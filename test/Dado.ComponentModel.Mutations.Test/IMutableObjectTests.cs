// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	#region Testing Components
	
	public class IMutableObjectTestClass : IMutableObject
	{
		// When true Integer must be more than 0 and less then 100.
		public bool EnforceRange { get; set; }

		public int Integer { get; set; }

		void IMutableObject.Mutate(IMutationContext context)
		{
			if (EnforceRange) {
				Integer = Math.Max(1, Math.Min(99, Integer));
			}
		}
	}

	#endregion Testing Components

	public class IMutableObjectTests
	{
		[Theory]
		[InlineData(-1000)]
		[InlineData(50)]
		[InlineData(1000)]
		public static void MutateObjectEnforceRangeIsFalse(int input)
		{
			var derivedClass = new IMutableObjectTestClass() {
				Integer = input
			};
			var context = new MutationContext<IMutableObjectTestClass>(derivedClass);

			context.Mutate();

			Assert.Equal(input, derivedClass.Integer);
		}

		[Theory]
		[InlineData(-1000, 1)]
		[InlineData(50, 50)]
		[InlineData(1000, 99)]
		public static void MutateObjectEnforceRangeIsTrue(int input, int output)
		{
			var derivedClass = new IMutableObjectTestClass() {
				Integer = input,
				EnforceRange = true
			};
			var context = new MutationContext<IMutableObjectTestClass>(derivedClass);
			
			context.Mutate();

			Assert.Equal(output, derivedClass.Integer);
		}
	}
}
