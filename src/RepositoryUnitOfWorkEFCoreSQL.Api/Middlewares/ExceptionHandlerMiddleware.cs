using FluentValidation;
using RepositoryUnitOfWorkEFCoreSQL.Api.Exceptions;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Models;
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
            await HandleServiceExceptionAsync(context, exception);
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

        var message = "The request contains invalid data.";
        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

        var errorResponse = new ErrorResponse(message, errors);
        context.Response.StatusCode = (int)httpStatusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    private async Task HandleServiceExceptionAsync(HttpContext context, Exception exception)
    {
        logger.LogError("InternalSeverError in ExceptionHandlerMiddleware at {Datetime} with exception {@Exception}", DateTime.Now, exception);

        var errorResponse = new ErrorResponse("Something went wrong. Please try again later.");
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
