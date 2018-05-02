// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;
using Xunit;

namespace Dado.ComponentModel.DataMutations.Test.Extensions
{
	public static class AssertExtensions
	{
		public static void Throws<T>(Action action, string message)
			where T : Exception
			=> Assert.Equal(Assert.Throws<T>(action).Message, message);

		public static T Throws<T>(string paramName, Action action)
			where T : ArgumentException
		{
			T exception = Assert.Throws<T>(action);

			Assert.Equal(paramName, exception.ParamName);

			return exception;
		}
	}
}
