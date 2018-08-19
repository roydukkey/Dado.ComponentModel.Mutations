// Copyright (c) roydukkey. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See the LICENSE file in the project root for more information.

using System;

namespace Dado.ComponentModel.DataMutations
{
	/// <summary>
	///		Base class for all mutation attributes.
	/// </summary>
	public abstract class MutationAttribute : Attribute
	{
		#region Properties

		/// <summary>
		///		Gets or sets a value indicating whether the attribute requires a non-null <see cref="IMutationContext" /> to perform validation. Base class returns <c>false</c>. Override in child classes as appropriate.
		/// </summary>
		public virtual bool RequiresContext { get; protected set; } = false;

		/// <summary>
		///		Gets or sets a value indicating the priority that determines the order in which <see cref="MutationAttribute" />s are evaluated. Base class defaults to <c>10</c>. Override in child classes as appropriate.
		/// </summary>
		public virtual int Priority { get; set; } = 10;

		#endregion Properties

		#region Public Methods

		/// <summary>
		///		Mutates the given value according to this <see cref="MutationAttribute" />.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The resulting mutated value.</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="context" /> is required and <c>null</c>.</exception>
		public object Mutate(object value, IMutationContext context = null)
		{
			if (RequiresContext && context == null) {
				throw new ArgumentNullException(nameof(context), "A mutation context is required by this mutation attribute.");
			}

			return MutateValue(value, context);
		}

		#endregion Public Methods

		#region Protected Methods

		/// <summary>
		///		A protected method to override and implement mutation logic.
		/// </summary>
		/// <param name="value">The value to mutate.</param>
		/// <param name="context">Describes the <paramref name="value" /> being mutated and provides services and context for mutation.</param>
		/// <returns>The resulting mutated value.</returns>
		protected abstract object MutateValue(object value, IMutationContext context);

		#endregion Protected Methods
	}
}
