using Microsoft.AspNetCore.Http;
using Shared.Api.BaseModel;
using Shared.Api.Exceptions;

namespace Shared.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private const int DomainExceptionStatusCode = 400;
    private const int ExceptionStatusCode = 500;
    private const int ServiceExceptionStatusCode = 503;
    private const int UnauthorizedAccessStatusCode = 403;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (DomainException ex)
        {
            await HandleExceptionAsync(httpContext, ex, DomainExceptionStatusCode, ex.ObjetoErro);
        }
        catch (ServiceException ex)
        {
            await HandleExceptionAsync(httpContext, ex, ServiceExceptionStatusCode, ex.ObjetoErro);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(httpContext, ex, UnauthorizedAccessStatusCode);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, ExceptionStatusCode);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        int statusCode,
        object objetoErro = null)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(new DetalhesDoErro()
        {
            StatusCode = context.Response.StatusCode,
            Mensagem = exception.Message,
            ObjetoErro = objetoErro
        }.ToString());
    }
}