// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Dado.ComponentModel.DataMutations.Test.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	public class MutatorTests
	{
		#region Mutate Value

		[Fact]
		public static void MutateValueThrowsIfContextIsNull()
		{
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.Mutate<int>(null, Enumerable.Empty<MutationAttribute>())
			);
		}

		[Fact]
		public static void MutateValueThrowsIfAttributeEnumerableIsNull()
		{
			var context = new MutationContext<int>(default(int));

			AssertExtensions.Throws<ArgumentNullException>("attributes",
				() => context.Mutate(default(IEnumerable<MutationAttribute>))
			);
			AssertExtensions.Throws<ArgumentNullException>("attributes",
				() => context.Mutate(default(IEnumerable<MutationAttribute>), default(int))
			);
		}

		[Fact]
		public static void MutateValueSucceedsIfNoAttributesAreGivenRegardlessOfValue()
		{
			var context = new MutationContext<int>(default(int));
			var attributes = Enumerable.Empty<MutationAttribute>();

			Assert.Equal(0, context.Mutate(attributes));
			Assert.Equal(1, context.Mutate(attributes, 1));
		}

		[Fact]
		public static void MutateValueTransformsWithToDefaultValueAttribute()
		{
			var context = new MutationContext<int>(1);
			var attributes = new[] { new ToDefaultValueAttribute(1, 2) };

			Assert.Equal(0, context.Mutate(attributes));
			Assert.Equal(0, context.Mutate(attributes, 2));
			Assert.Equal(1, context.ObjectInstance);
		}

		[Fact]
		public static void MutateValueTransformsWithCorrectPriority()
		{
			var context = new MutationContext<string>("abcba");
			var attributes = new[] {
				new TrimAttribute('a') { Priority = 10 },
				new TrimAttribute('b') { Priority = 20 }
			};

			Assert.Equal("c", context.Mutate(attributes));

			attributes[0].Priority = 30;

			Assert.Equal("bcb", context.Mutate(attributes));
		}

		#endregion Mutate Value

		#region Mutate Object

		[Fact]
		public static void MutateObjectThrowsIfContextIsNull()
		{
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.Mutate<TestClass>(null)
			);
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.Mutate<TestClass>(null, Enumerable.Empty<MutationAttribute>())
			);
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.Mutate(null, default(TestClass))
			);
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.Mutate(null, default(TestClass), Enumerable.Empty<MutationAttribute>())
			);
		}

		[Fact]
		public static void MutateObjectThrowsIfAttributeEnumerableIsNull()
		{
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);

			AssertExtensions.Throws<ArgumentNullException>("attributes",
				() => context.Mutate(default(IEnumerable<MutationAttribute>))
			);
			AssertExtensions.Throws<ArgumentNullException>("attributes",
				() => context.Mutate(derivedClass, default(IEnumerable<MutationAttribute>))
			);
		}

		[Fact]
		public static void MutateObjectDoNotThrowIfInstanceIsNull()
		{
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);

			context.Mutate(default(TestClass));
			context.Mutate(null, Enumerable.Empty<MutationAttribute>());
		}

		[Fact]
		public static void MutateObjectSucceedsIfNoAttributesAreGivenRegardlessOfValue()
		{
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);
			var attributes = Enumerable.Empty<MutationAttribute>();

			Assert.Equal(derivedClass, context.Mutate(attributes));
			Assert.Equal(derivedClass, context.Mutate(derivedClass, attributes));
		}

		[Fact]
		public static void MutateObjectTransformsWithMutationAttributesOnClass()
		{
			var derivedClassOne = new TestClass();
			var contextOne = new MutationContext<TestClass>(derivedClassOne);
		
			Assert.Equal(derivedClassOne, contextOne.Mutate());
			Assert.Equal(derivedClassOne, contextOne.ObjectInstance);
			Assert.Equal(derivedClassOne.IntergerTen, contextOne.ObjectInstance.IntergerTen);
			Assert.Equal(100, derivedClassOne.IntergerTen);

			var derivedClassTwo = new TestClass();
			var derivedClassThree = new TestClass();
			var contextTwo = new MutationContext<TestClass>(derivedClassThree);

			Assert.Equal(derivedClassTwo, contextTwo.Mutate(derivedClassTwo));
			Assert.NotEqual(derivedClassTwo, contextTwo.ObjectInstance);
			Assert.NotEqual(derivedClassTwo.IntergerTen, contextTwo.ObjectInstance.IntergerTen);
			Assert.Equal(100, derivedClassTwo.IntergerTen);
		}

		#endregion Mutate Object

		#region Mutate Property

		[Fact]
		public static void MutatePropertyThrowsIfContextIsNull()
		{
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.MutateProperty<TestClass>(null, default(PropertyInfo))
			);
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.MutateProperty<TestClass, object>(null, default(PropertyInfo), null)
			);
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.MutateProperty<TestClass, object>(null, x => x.IntergerTen)
			);
			AssertExtensions.Throws<ArgumentNullException>("context",
				() => Mutator.MutateProperty<TestClass, object>(null, x => x.IntergerTen, null)
			);
		}

		[Fact]
		public static void MutatePropertyThrowsIfPropertyIsNull()
		{
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);

			AssertExtensions.Throws<ArgumentNullException>("property",
				() => context.MutateProperty(default(PropertyInfo))
			);
			AssertExtensions.Throws<ArgumentNullException>("property",
				() => context.MutateProperty<TestClass, object>(default(PropertyInfo), null)
			);
			AssertExtensions.Throws<ArgumentNullException>("property",
				() => context.MutateProperty(default(Expression<Func<TestClass, object>>))
			);
			AssertExtensions.Throws<ArgumentNullException>("property",
				() => context.MutateProperty(default(Expression<Func<TestClass, object>>), null)
			);
		}

		[Fact]
		public static void MutatePropertyDoNotThrowIfValueIsNull()
		{
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);
			var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.StringSpace));

			context.MutateProperty<TestClass, string>(propertyInfo, null);
			context.MutateProperty(x => x.StringSpace, null);
		}

		[Fact]
		public static void MutatePropertyTransformsWithToDefaultValueAttribute()
		{
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);
			var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.StringSpace));

			Assert.Null(context.MutateProperty(propertyInfo));
			Assert.Equal(derivedClass.StringSpace, context.ObjectInstance.StringSpace);

			derivedClass = new TestClass();
			context = new MutationContext<TestClass>(derivedClass);

			Assert.Null(context.MutateProperty(x => x.StringSpace));
			Assert.Equal(derivedClass.StringSpace, context.ObjectInstance.StringSpace);
		}

		[Fact]
		public static void MutatePropertyTransformsWithToDefaultValueAttributeWhenSuppliedValue()
		{
			// Check that source property value is not altered when custom value is supplied
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);
			var propertyInfo = typeof(TestClass).GetProperty(nameof(TestClass.StringSpace));

			Assert.Null(context.MutateProperty(propertyInfo, " "));
			Assert.Equal(" ", context.ObjectInstance.StringSpace);
			Assert.Equal(derivedClass.StringSpace, context.ObjectInstance.StringSpace);

			Assert.Null(context.MutateProperty(x => x.StringSpace, " "));
			Assert.Equal(" ", context.ObjectInstance.StringSpace);
			Assert.Equal(derivedClass.StringSpace, context.ObjectInstance.StringSpace);
		}

		#endregion Mutate Property
	}
}
