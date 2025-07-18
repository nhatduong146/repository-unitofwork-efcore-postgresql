﻿using FluentValidation;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Models;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Resources;
using RepositoryUnitOfWorkEFCoreSQL.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace RepositoryUnitOfWorkEFCoreSQL.Api.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException exception)
        {
            await HandleRequestExceptionAsync(context, exception, HttpStatusCode.NotFound);
        }
        catch (BadRequestException exception)
        {
            await HandleRequestExceptionAsync(context, exception, HttpStatusCode.BadRequest);
        }
        catch (ValidationException exception)
        {
            await HandleValidationExceptionAsync(context, exception, HttpStatusCode.BadRequest);
        }
        catch (Exception exception)
        {
            await HandleServerExceptionAsync(context, exception);
        }
    }

    public async Task HandleRequestExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
    {
        logger.LogError("Error in ExceptionHandlerMiddleware at {Datetime} with status code {StatusCode} and exception {@Exception}",
            DateTime.Now, (int)httpStatusCode, exception);

        var errorResponse = new ErrorResponse(exception.Message);
        if (!env.IsProduction())
        {
            errorResponse.Detail = exception.StackTrace;
        }

        context.Response.StatusCode = (int)httpStatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    public async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception, HttpStatusCode httpStatusCode)
    {
        logger.LogError("Validation error in ExceptionHandlerMiddleware at {Datetime} with status code {StatusCode} and exception {@Exception}",
            DateTime.Now, (int)httpStatusCode, exception);

        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

        var errorResponse = new ErrorResponse(ErrorMessages.RequestContainsInvalidData, errors);
        context.Response.StatusCode = (int)httpStatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    private async Task HandleServerExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError("InternalSeverError in ExceptionHandlerMiddleware at {Datetime} with exception {@Exception}", DateTime.Now, exception);

        var errorResponse = new ErrorResponse(ErrorMessages.InternalServer);
        if (!env.IsProduction())
        {
            errorResponse.Message = exception.Message;
            errorResponse.Detail = exception.StackTrace;
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
