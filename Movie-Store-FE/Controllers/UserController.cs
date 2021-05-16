using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using Movie_Store_FE.Extensions;
using Movie_Store_Data.Models;
using Microsoft.IdentityModel.Logging;

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

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            user.Password = PasswordHelper.GetEncrypt(user.Password);
            var result = await userApiClient.Register(user);
            if (result.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Can not register right now! Try again!");
            return View("");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            ModelState.Remove("Email");
            ModelState.Remove("FullName");

            //Encrypt password
            user.Password = PasswordHelper.GetEncrypt(user.Password);

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

            ModelState.AddModelError("", "Login fail! Username or password is incorrect");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "User");
        }

        public ClaimsPrincipal IsValidToken(string token)
        {
            IdentityModelEventSource.ShowPII = true;
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
