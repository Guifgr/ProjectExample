using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Project.Domain.Exceptions;

namespace Project.Application.Middleware;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
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
        var response = new Error
        {
            Message = exception.Message
        };

        context.Response.StatusCode = exception switch
        {
            BadRequestException => 400,
            NoContentException => 204,
            ServerException => 500,
            _ => context.Response.StatusCode
        };

        var result = JsonConvert.SerializeObject(response);
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result);
    }


    private class Error
    {
        public string Message { get; set; }
    }
}