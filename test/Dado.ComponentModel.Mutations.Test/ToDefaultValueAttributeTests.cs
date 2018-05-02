// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	#region Testing Components

	public class ToDefaultValueTestClass
	{
		[ToDefaultValue(1), DefaultValue(10)]
		public int Integer1To10 { get; set; } = 1;

		[ToDefaultValue(2D), DefaultValue(3D)]
		public double Double2Dto3D { get; set; } = 2D;

		[ToDefaultValue(3F), DefaultValue(4F)]
		public float Float3Fto4F { get; set; } = 3F;

		[ToDefaultValue(4L), DefaultValue(5L)]
		public long Long4Lto5L { get; set; } = 4L;

		[ToDefaultValue('a'), DefaultValue('b')]
		public char CharAtoB { get; set; } = 'a';

		[ToDefaultValue("c"), DefaultValue("d")]
		public string StringCtoD { get; set; } = "c";

		[ToDefaultValue(false), DefaultValue(true)]
		public bool BooleanFalseToTrue { get; set; } = false;
	}

	#endregion Testing Components

	public class ToDefaultValueAttributeTests
	{
		[Theory]
		[InlineData(1)]
		[InlineData(2D)]
		[InlineData(3F)]
		[InlineData(4L)]
		[InlineData('a')]
		[InlineData("c")]
		[InlineData(true)]
		public static void MutateTransformsToDefaultValue<T>(T value)
		{
			var attribute = new ToDefaultValueAttribute(value);

			Assert.Equal(default(T), attribute.Mutate(value));
		}

		[Theory]
		[InlineData(1, 2)]
		[InlineData(2D, 3D)]
		[InlineData(3F, 4F)]
		[InlineData(4L, 5L)]
		[InlineData('a', 'b')]
		[InlineData("c", "d")]
		[InlineData(false, true)]
		public static void MutateTransformsToSpecifiedDefaultValue<T>(T value, T defaultValue)
		{
			var attribute = new ToDefaultValueAttribute(value);

			Assert.Equal(defaultValue, attribute.Mutate(value, defaultValue));
		}

		[Fact]
		public static void MutateTransformsToInferredDefaultValue()
		{
			var derivedClass = new ToDefaultValueTestClass();
			var context = new MutationContext<ToDefaultValueTestClass>(derivedClass);

			Assert.Equal(10, context.MutateProperty(x => x.Integer1To10));
			Assert.Equal(3D, context.MutateProperty(x => x.Double2Dto3D));
			Assert.Equal(4F, context.MutateProperty(x => x.Float3Fto4F));
			Assert.Equal(5L, context.MutateProperty(x => x.Long4Lto5L));
			Assert.Equal('b', context.MutateProperty(x => x.CharAtoB));
			Assert.Equal("d", context.MutateProperty(x => x.StringCtoD));
			Assert.True(context.MutateProperty(x => x.BooleanFalseToTrue));
		}
	}
}
