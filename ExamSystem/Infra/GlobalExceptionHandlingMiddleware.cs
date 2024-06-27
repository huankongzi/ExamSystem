using System.Net;
using System.Text.Json;
using ExamSystem.Dto;

namespace ExamSystem.Infra;

/// <summary>
///     Global exception handling middleware
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    ///     Constructor of GlobalExceptionHandlingMiddleware
    /// </summary>
    /// <param name="next">next action</param>
    /// <param name="logger">logger</param>
    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    ///     Invoke the middleware
    /// </summary>
    /// <param name="context">http context</param>
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
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.Clear();
        _logger.LogError($"An unhandled exception has occurred: {exception}");
        var response = new Response<object> { ErrorMessage = "Internal Server Error" };
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
    }
}