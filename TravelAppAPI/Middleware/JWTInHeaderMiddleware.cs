using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.IO;

namespace Middleware
{
    public class JWTInHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTInHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authenticationCookieName = "token";
            var token = context.Request.Cookies[authenticationCookieName];
            if (token != null)
            {

                context.Request.Headers.Append("Authorization", "Bearer " + token);
            }

            await _next(context);
        }

        
    }
    public static class JWTInHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseJWTInHeader(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTInHeaderMiddleware>();
        }
    }
}