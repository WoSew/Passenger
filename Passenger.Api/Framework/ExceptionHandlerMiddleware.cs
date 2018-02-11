using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Passenger.Api.Framework
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext contex)
        {
            try
            {
                await _next(contex); //spróbuj wykonac żądanie HTTP
            }
            catch(Exception exception)
            {
                await HandlerExceptionAsync(contex, exception);
            }
        }

        private static Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            var errorCode = "error";
            var statusCode = HttpStatusCode.BadRequest;
            var exceptionType = exception.GetType();
            switch(exception)
            {
                case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                    statusCode = HttpStatusCode.Unauthorized; //401
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var response = new { code = errorCode, message = exception.Message };
            var payload = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);

        }

    }
}