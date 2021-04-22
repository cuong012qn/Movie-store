using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie_Store_API.Middleware
{
    public class StaticFilesMiddleware
    {
        private readonly RequestDelegate _next;

        public StaticFilesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/Static")
            {
                await context.Response.WriteAsync("Access to Static");
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
