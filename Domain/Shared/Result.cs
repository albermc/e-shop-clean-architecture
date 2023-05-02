namespace Domain.Shared
{
	public class Result
	{
		protected internal Result(bool isSuccess, Error error)
		{
			if (isSuccess && error != Error.None)
			{
				throw new InvalidOperationException();
			}

			if (!isSuccess && error == Error.None)
			{
				throw new InvalidOperationException();
			}

			IsSuccess = isSuccess;
			Errors = new Error[] { error };
		}

		protected internal Result(bool isSuccess, Error[] errors)
		{
			IsSuccess = isSuccess;
			Errors = errors;
		}

		public bool IsSuccess { get; }
		public bool IsFailure => !IsSuccess;

		public Error[] Errors { get; }

		public static Result Success() => new Result(true, Error.None);
		public static Result<TValue> Success<TValue>(TValue value) => new Result<TValue>(value, true, Error.None);
		public static Result Failure(Error error) => new Result(false, error);
		public static Result Failure(Error[] errors) => new Result(false, errors);
		public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, error);
		public static Result<TValue> Failure<TValue>(Error[] errors) => new Result<TValue>(default, false, errors);
		public static Result<TValue> Create<TValue>(TValue? value) =>
			value is not null ?
				Success(value) :
				Failure<TValue>(Error.NullValue);

		public static Result<T> Ensure<T>(
			T value,
			Func<T, bool> predicate,
			Error error)
		{
			return predicate(value) ?
				Success(value) :
				Result.Failure<T>(error);
		}

		public static Result<T> Ensure<T>(
			T value,
			params (Func<T, bool> predicate, Error error)[] functions)
		{
			var results = new List<Result<T>>();
			foreach ((Func<T, bool> predicate, Error error) in functions)
			{
				results.Add(Ensure(value, predicate, error));
			}
			return Combine(results.ToArray());
		}

		public static Result<T> Combine<T>(params Result<T>[] results)
		{
			if (results.Any(x => x.IsFailure))
			{
				return Failure<T>(
					results.SelectMany(x => x.Errors).Distinct().ToArray());

			}

			return Success(results[0].Value);
		}
	}
}