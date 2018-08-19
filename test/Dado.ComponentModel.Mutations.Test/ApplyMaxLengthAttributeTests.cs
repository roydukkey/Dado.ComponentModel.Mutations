// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class ApplyMaxLengthAttributeTests
	{
		#region Testing Components

		public class ApplyMaxLengthAttributeClass
		{
			[StringLength(5), ApplyMaxLength]
			public string StringLengthOfTen { get; set; } = "1234567890";

			[MaxLength(5), ApplyMaxLength]
			public string MaxLengthOfTen { get; set; } = "1234567890";

			[MaxLength(5), ApplyListLength]
			public IList<string> GenericStringListOfTen { get; set; } = new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

			[MaxLength(5), ApplyStackLength]
			public Stack StackOfTen { get; set; } = new Stack(new[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" });
		}

		public class ApplyListLengthAttribute : ApplyMaxLengthAttribute
		{
			private static readonly CollectionDescriptor descriptor = new CollectionDescriptor().SetGenericCollectionMutator<string>(
				(length, collection) => collection.Take(length).ToList()
			);

			public ApplyListLengthAttribute() : base(descriptor) { }
		}

		public class ApplyStackLengthAttribute : ApplyMaxLengthAttribute
		{
			private static readonly CollectionDescriptor descriptor = new CollectionDescriptor().SetCollectionMutator<Stack>(
				(length, collection) => new Stack(collection.ToArray().Reverse().Take(length).ToList())
			);

			public ApplyStackLengthAttribute() : base(descriptor) { }
		}

		#endregion Testing Components

		[Fact]
		public static void MutateRequiresContextThroughMutator()
		{
			var attribute = new ApplyMaxLengthAttribute();

			Assert.True(attribute.RequiresContext);
			var exception = Assert.Throws<ArgumentNullException>("context",
				() => Mutator.Mutate<int>(null, new[] { attribute })
			);
			Assert.Equal($"A mutation context is required by this mutation attribute.{Environment.NewLine}Parameter name: context", exception.Message);
		}

		[Theory]
		[InlineData(-2, "1234567890")]
		[InlineData(0, "")]
		[InlineData(5, "12345")]
		[InlineData(10, "1234567890")]
		[InlineData(11, "1234567890")]
		public static void MutateTurncatesString(int maxLength, string output)
		{
			var attribute = new ApplyMaxLengthAttribute();

			Assert.Equal(output, attribute.Mutate("1234567890", maxLength));
		}

		[Fact]
		public static void MutateTurncatesStringToInferredLength()
		{
			var derivedClass = new ApplyMaxLengthAttributeClass();
			var context = new MutationContext<ApplyMaxLengthAttributeClass>(derivedClass);

			Assert.Equal("12345", context.MutateProperty(x => x.StringLengthOfTen));
			Assert.Equal("12345", context.MutateProperty(x => x.MaxLengthOfTen));
		}

		[Fact]
		public static void MutateTurncatesGenericListToInferredLength()
		{
			var derivedClass = new ApplyMaxLengthAttributeClass();
			var context = new MutationContext<ApplyMaxLengthAttributeClass>(derivedClass);

			Assert.Equal(5, context.MutateProperty(x => x.GenericStringListOfTen).Count);
			Assert.Equal("a", derivedClass.GenericStringListOfTen[0]);
			Assert.Equal("b", derivedClass.GenericStringListOfTen[1]);
			Assert.Equal("c", derivedClass.GenericStringListOfTen[2]);
			Assert.Equal("d", derivedClass.GenericStringListOfTen[3]);
			Assert.Equal("e", derivedClass.GenericStringListOfTen[4]);
			Assert.Throws<ArgumentOutOfRangeException>(() => derivedClass.GenericStringListOfTen[5]);
		}

		[Fact]
		public static void MutateTurncatesStackToInferredLength()
		{
			var derivedClass = new ApplyMaxLengthAttributeClass();
			var context = new MutationContext<ApplyMaxLengthAttributeClass>(derivedClass);

			Assert.Equal(5, context.MutateProperty(x => x.StackOfTen).Count);
			Assert.Equal("e", derivedClass.StackOfTen.Pop());
			Assert.Equal("d", derivedClass.StackOfTen.Pop());
			Assert.Equal("c", derivedClass.StackOfTen.Pop());
			Assert.Equal("b", derivedClass.StackOfTen.Pop());
			Assert.Equal("a", derivedClass.StackOfTen.Pop());
			Assert.Throws<InvalidOperationException>(() => derivedClass.StackOfTen.Pop());
		}
	}
}
