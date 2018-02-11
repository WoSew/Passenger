using Microsoft.AspNetCore.Builder;

namespace Passenger.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UserExceptionHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}