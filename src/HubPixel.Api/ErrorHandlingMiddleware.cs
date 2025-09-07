using System.Net;
using System.Text.Json;

namespace HubPixel.Api;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode code;
        string result;

        switch (exception)
        {
            case ArgumentException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(new { message = exception.Message });
                break;
            case InvalidOperationException:
                code = HttpStatusCode.Conflict;
                result = JsonSerializer.Serialize(new { message = exception.Message });
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                result = JsonSerializer.Serialize(new { message = "Ocorreu um erro interno. Por favor, tente novamente." });
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}
