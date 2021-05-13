using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_FE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Movie_Store_FE.ApiClient.Interface;

namespace Movie_Store_FE.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApiClient userApiClient;

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        private IConfiguration configuration;

        public UserController(IConfiguration configuration, IUserApiClient userApiClient)
        {
            this.configuration = configuration;
            this.userApiClient = userApiClient;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            //string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjQ5MTFhYTQyLTJlZjgtNGUxZi05ZDc0LWI4YTYyMWM0OWJkNyIsInVuaXF1ZV9uYW1lIjoiYWRtaW4iLCJuYmYiOjE2MjA5MjYzODcsImV4cCI6MTYyMDkyNzU4NywiaWF0IjoxNjIwOTI2Mzg3fQ.KFfRe7q4jRmFzQdSWzm-6H6XltOlcFFWsw8hrLCllLY";

            var response = await userApiClient.Authencate(user);

            if (response.Success)
            {

                var userPrincipal = IsValidToken(response.Token);

                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                    IsPersistent = false
                };

                HttpContext.Session.SetString("Token", response.Token);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    authProperties
                    );
                return RedirectToAction("Index", "User");
            }
            return View(nameof(Login));
        }

        public ClaimsPrincipal IsValidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey"));
            var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return claims;
        }
    }
}
