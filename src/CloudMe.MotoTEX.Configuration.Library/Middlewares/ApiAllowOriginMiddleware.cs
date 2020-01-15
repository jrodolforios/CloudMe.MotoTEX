using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Configuration.Library.Middleware
{
    public class ApiAllowOriginMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiAllowOriginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //if (context.Request.Path.Value.Contains("/api"))
            {
                if (context.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    context.Response.Headers.Remove("X-Frame-Options");
                }

                if (!context.Response.Headers.ContainsKey("Access-Control-Allow-Origin"))
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                }

                if (!context.Response.Headers.ContainsKey("Access-Control-Allow-Headers"))
                {
                    context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                }

                if (!context.Response.Headers.ContainsKey("Access-Control-Allow-Methods"))
                {
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                }
                //
            }
            await _next(context);
        }
    }
    public static class ApiAllowOriginMiddlewareExtensions
    {
        public static IApplicationBuilder ApiAllowOrigin(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiAllowOriginMiddleware>();
        }
    }
}
