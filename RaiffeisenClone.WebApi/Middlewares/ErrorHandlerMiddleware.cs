using System.Net;
using System.Text.Json;
using FluentValidation;
using RaiffeisenClone.Application.Exceptions;

namespace RaiffeisenClone.WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch(error)
            {
                case ValidationException validationException:
                    response.StatusCode = (int) HttpStatusCode.OK;
                    break;
                case AppException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ArgumentException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                error?.Message
            });
            await response.WriteAsync(result);
        }
    }
}