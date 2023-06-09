﻿using System.Text.Json;
using System.Net;

namespace eShop.App.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
	private readonly ILogger _logger;

	public GlobalExceptionHandlingMiddleware(ILogger logger)
	{
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception e)
		{
			_logger.LogError(e, e.Message);

			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";

			await context.Response.WriteAsync(
				JsonSerializer.Serialize(new
				{
					Status = (int)HttpStatusCode.InternalServerError,
					Type = "Server error",
					Title = "Server error",
					Detail = "An internal server error has occurred"
				}));

		}
	}
}
