﻿using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviours;

public class LoggingPipelineBehaviour<TRequest, TResponse>
	: IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
	where TResponse : Result
{
	private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;

	public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
	{
		_logger = logger;
	}

	public async Task<TResponse> Handle(
		TRequest request,
		RequestHandlerDelegate<TResponse> next,
		CancellationToken cancellationToken)
	{
		_logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}",
			typeof(TRequest).Name,
			DateTime.UtcNow);

		var result = await next();

		if (result.IsFailure)
		{
			_logger.LogError("Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
				typeof(TRequest).Name,
				result.Errors,
				DateTime.UtcNow);
		}
		else
		{
			_logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}",
				typeof(TRequest).Name,
				DateTime.UtcNow);
		}

		return result;
	}
}
