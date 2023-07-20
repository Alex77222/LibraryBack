using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Library.Exceptions;

namespace Library.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException.InnerException);
                break;
            case AuthException authException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(authException.Message);
                break;
            case UserException userException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(userException.Message);
                break;
            case BookException bookException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(bookException.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonSerializer.Serialize(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}

public static class MiddlewareExceptions
{
    public static IApplicationBuilder UseException(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}