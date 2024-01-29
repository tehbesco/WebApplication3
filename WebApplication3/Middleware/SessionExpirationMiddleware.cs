using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Middleware
{
    public class SessionExpirationMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if the request is for the login page
            if (context.Request.Path.Equals("/login", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Check if the session is available and contains keys
            if (context.Session != null && context.Session.IsAvailable && context.Session.Keys.Any())
            {
                // If the session is valid, proceed with the request
                await _next(context);
            }
            else
            {
                // Redirect to the login page if the session is not available or empty
                context.Response.Redirect("/login");
            }
        }
    }
}
