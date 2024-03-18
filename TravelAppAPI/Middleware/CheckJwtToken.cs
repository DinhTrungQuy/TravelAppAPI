using System.Globalization;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Middleware
{
    public class CheckJwtToken
    {
        private readonly RequestDelegate _next;
        private readonly CacheServices _cacheServices;

        public CheckJwtToken(RequestDelegate next, CacheServices cacheServices)
        {
            _cacheServices = cacheServices;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var TokenInRedis = _cacheServices.GetData<bool>(token);
                if (!TokenInRedis)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token is revoked");
                    return;
                }
            }
            await _next(context);
        }

    }
    public static class CheckJwtTokenExtensions
    {
        public static IApplicationBuilder UseCheckBlacklistJwtToken(
                           this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckJwtToken>();
        }
    }
}
