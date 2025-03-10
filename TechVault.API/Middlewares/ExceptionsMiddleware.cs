using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;
using TechVault.API.Helper;

namespace TechVault.API.Middlewares
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IHostEnvironment _hostEnvironment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
        public ExceptionsMiddleware(RequestDelegate next, IHostEnvironment hostEnvironment, IMemoryCache memoryCache)
        {
            _next = next;
            _hostEnvironment = hostEnvironment;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                ApplySecurity(httpContext);

                if(IsRequestAllowed(httpContext) == false)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    httpContext.Response.ContentType = "application/json";
                    var response = new
                        ApiExceptions((int)HttpStatusCode.TooManyRequests, "Too many requests pls try again later");

                    await httpContext.Response.WriteAsJsonAsync(response);
                }
               await _next(httpContext);
            }
            catch (Exception ex)
            {

                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var response = _hostEnvironment.IsDevelopment() ?
                    new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                        : new ApiExceptions((int)HttpStatusCode.InternalServerError, ex.Message);
                var json = JsonSerializer.Serialize(response);
                await httpContext.Response.WriteAsync(json);
            }
        }

        private bool IsRequestAllowed(HttpContext httpContext)
        {
            var ip = httpContext.Connection.RemoteIpAddress?.ToString();
            var cachKey = $"Rate:{ip}";
            var dateNow = DateTime.Now;

            var (timeStamp, count) = _memoryCache.GetOrCreate(cachKey,entry => {

                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timeStamp: dateNow, count: 0);

               });

            if((dateNow-timeStamp) < _rateLimitWindow)
            {
                if(count >= 8)
                {
                    return false;
                }
                _memoryCache.Set(cachKey, (timeStamp, count += 1), _rateLimitWindow);
            } 
            else
            {
                _memoryCache.Set(cachKey, (timeStamp, count), _rateLimitWindow);

            }
            return true;

        }

        private void ApplySecurity(HttpContext httpContext)
        {
            httpContext.Response.Headers["X-Content-Type-Options"] = "nosniff";

            httpContext.Response.Headers["X-XSS-Protection"] = "1:mode=block";

            httpContext.Response.Headers["X-Frame-Options"] = "Deny";
        }
    }
}
