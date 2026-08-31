using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Empo.EmloyeeService.Api;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    private static readonly JsonSerializerOptions JsonOption = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IHostEnvironment env)

    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, errorResponse) = MapException(exception, context);

        if (statusCode >= 500)
        {
            _logger.LogError(exception, "Unhandled exception on {Method} {Path}",
                context.Request.Method, context.Request.Path);
        }
        else
        {
            _logger.LogWarning("{ErrorCode} on {Method} {Path}: {Message}",
                errorResponse.ErrorCode, context.Request.Method, context.Request.Path, errorResponse.Message);
        }
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse, JsonOption));

    }
    private (int StatusCOde, ErrorResponse Response) MapException(Exception exception, HttpContext context)
    {
        var traceId = context.TraceIdentifier;

        switch (exception)
        {

            case AppException appEx:
                return ((int)appEx.StatusCode, new ErrorResponse
                {
                    ErrorCode = appEx.ErrorCode,
                    Message = appEx.Message,
                    TraceId = traceId
                });

            case UnauthorizedAccessException authEx:
                return ((int)HttpStatusCode.Unauthorized, new ErrorResponse
                {
                    ErrorCode = "UNAUTHORIZED",
                    Message = authEx.Message,
                    TraceId = traceId
                });

            case KeyNotFoundException notFoundEx:
                return ((int)HttpStatusCode.NotFound, new ErrorResponse
                {
                    ErrorCode = "NOT_FOUND",
                    Message = notFoundEx.Message,
                    TraceId = traceId
                });

            case ArgumentException argEx:
                return ((int)HttpStatusCode.BadRequest, new ErrorResponse
                {
                    ErrorCode = "BAD_REQUEST",
                    Message = argEx.Message,
                    TraceId = traceId
                });

            case ValidationException validationEx:
                var errors = validationEx.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                return ((int)HttpStatusCode.BadRequest, new ErrorResponse
                {
                    ErrorCode = "VALIDATION_FAILED",
                    Message = "One or more validation errors occurred.",
                    Errors = errors,
                    TraceId = traceId
                });

            default:
                return ((int)HttpStatusCode.InternalServerError, new ErrorResponse
                {
                    ErrorCode = "INTERNAL_SERVER_ERROR",
                    Message = _env.IsDevelopment()
                        ? exception.Message
                        : "An unexpected error occurred. Please try again later.",
                    TraceId = traceId
                });
        }
    }
}

public static class GlobalExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}