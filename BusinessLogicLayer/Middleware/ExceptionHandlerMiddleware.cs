using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace BusinessLogicLayer.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            var errorName = exception.GetType().Name;
            int statusCode = (int)HttpStatusCode.BadRequest;
            if (errorName.Equals("CreateTokenException") || errorName.Equals("SaveDbException"))
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
            }
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message,
                ErrorName = errorName
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
