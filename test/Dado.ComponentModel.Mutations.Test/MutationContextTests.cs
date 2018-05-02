// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using Dado.ComponentModel.DataMutations.Test.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dado.ComponentModel.DataMutations.Test
{
	#region Testing Components
	
	[CustomTest, CustomTestMutation]
	public class TestClass
	{
		public int IntergerTen { get; set; } = 10;

		[ToDefaultValue(" ")]
		public string StringSpace { get; set; } = " ";
	}

	public class TestServiceProvider : IServiceProvider
	{
		public object GetService(Type serviceType)
			=> null;
	}

	public class CustomTestAttribute : Attribute { }

	public class CustomTestMutationAttribute : MutationAttribute
	{
		protected override object MutateValue(object value, IMutationContext context)
		{
			if (value is TestClass typedValue) {
				typedValue.IntergerTen = 100;
			}

			return value;
		}
	}

	public class ItemsTestMutationAttribute : MutationAttribute
	{
		public override bool RequiresContext => true;

		protected override object MutateValue(object value, IMutationContext context)
		{
			return context.Items["Yes"];
		}
	}

	#endregion Testing Components

	public class MutationContextTests
	{
		[Fact]
		public static void ConstructorThrowsIfPassedNullInstance()
		{
			AssertExtensions.Throws<ArgumentNullException>("instance",
				() => new MutationContext<string>(null)
			);
		}

		[Fact]
		public static void ConstructorCreatesNewInstanceForOneArgConstructor()
		{
			var derivedClass = new TestClass();

			new MutationContext<TestClass>(derivedClass);
		}

		[Fact]
		public static void ConstructorCreatesNewInstanceForTwoArgConstructor()
		{
			var derivedClass = new TestClass();

			new MutationContext<TestClass>(derivedClass, default(IServiceProvider));
			new MutationContext<TestClass>(derivedClass, default(Dictionary<object, object>));

			var items = new Dictionary<object, object>();

			new MutationContext<TestClass>(derivedClass, items);
		}
		
		[Fact]
		public static void ConstructorCreatesNewInstanceForThreeArgConstructor()
		{
			var derivedClass = new TestClass();

			new MutationContext<TestClass>(derivedClass, null, null);

			var items = new Dictionary<object, object>();

			new MutationContext<TestClass>(derivedClass, items, null);

			var serviceProvider = new TestServiceProvider();

			new MutationContext<TestClass>(derivedClass, items, serviceProvider);
		}

		[Fact]
		public static void ObjectInstanceReturnsSameInstanceAsPassed()
		{
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);

			Assert.Same(derivedClass, context.ObjectInstance);
		}

		[Fact]
		public static void ItemsReturnsNewDictionaryWithSameKeysAndValues()
		{
			var derivedClass = new TestClass();
			var items = new Dictionary<object, object>() {
				{ "testKey1", "testValue1" },
				{ "testKey2", "testValue2" }
			};
			var context = new MutationContext<TestClass>(derivedClass, items);

			Assert.NotSame(items, context.Items);
			Assert.Equal(2, context.Items.Count);
			Assert.Equal("testValue1", context.Items["testKey1"]);
			Assert.Equal("testValue2", context.Items["testKey2"]);
		}
		
		[Fact]
		public void GetServiceCustomServiceProviderReturnsNull()
		{
			var context = new MutationContext<object>(new object());

			context.InitializeServiceProvider(type =>
			{
				Assert.Equal(typeof(int), type);

				return typeof(bool);
			});

			Assert.Equal(typeof(bool), context.GetService(typeof(int)));
		}
		
		[Fact]
		public void GetServiceNullServiceProviderReturnsNull()
		{
			var context = new MutationContext<object>(new object());

			Assert.Null(context.GetService(typeof(int)));
		}
		
		[Fact]
		public static void AttributesPropertyReturnsClassAttributes()
		{
			var derivedClass = new TestClass();
			var context = new MutationContext<TestClass>(derivedClass);

			Assert.IsType<CustomTestAttribute>(context.Attributes.First());
		}

		[Fact]
		public static void MutationItemsAreAccessibleInMutateValue()
		{
			var items = new Dictionary<object, object>() {
				{ "Yes", "No" }
			};
			var context = new MutationContext<string>("Yes", items);
			var attributes = new[] { new ItemsTestMutationAttribute() };

			Assert.Equal("No", context.Mutate(attributes));
			Assert.Equal("Yes", context.ObjectInstance);
		}
	}
}
