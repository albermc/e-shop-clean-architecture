﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
	public class Result<TValue> : Result
	{
		private readonly TValue? _value;

		protected internal Result(TValue? value, bool isSuccess, Error error)
			: base(isSuccess, error) => _value = value;

		protected internal Result(TValue? value, bool isSuccess, Error[] errors)
			: base(isSuccess, errors) => _value = value;

		public TValue Value => IsSuccess ?
			_value! :
			throw new InvalidOperationException("The value of a failure result can not be accessed");

		// IMPORTANT
		// Returning the value will implicitly create a Result<value> object
		public static implicit operator Result<TValue>(TValue? value) => Create(value);
	}
}
