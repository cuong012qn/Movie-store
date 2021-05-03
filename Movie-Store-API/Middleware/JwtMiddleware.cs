using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.Services.Interface;
using Microsoft.Extensions.Configuration;
using Movie_Store_API.Extensions;

namespace Movie_Store_API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly string _secretkey = string.Empty;

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;

            _secretkey = _configuration.GetValue<string>("SecretKey");
        }

        public async Task InvokeAsync(HttpContext context, IUserService userSevice)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await attachUserToContext(context, userSevice, token);


            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, IUserService userService, string token)
        {
            try
            {
                var jwtToken = JwtHelpers.GetInstance(_secretkey).IsValidToken(token);
                string userId = jwtToken.Claims.First(x => x.Type == "id").Value.ToString();

                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetUserByIDAsync(userId);
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
