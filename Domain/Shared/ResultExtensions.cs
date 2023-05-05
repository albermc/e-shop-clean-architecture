﻿namespace Domain.Shared;

public static class ResultExtensions
{


	public static Result<TOut> Map<TIn, TOut>(
		this Result<TIn> result,
		Func<TIn, TOut> mappingFunc)
	{
		return result.IsSuccess ?
			Result.Success(mappingFunc(result.Value)) :
			Result.Failure<TOut>(result.Errors);
	}
}
