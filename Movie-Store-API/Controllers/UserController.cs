using Microsoft.AspNetCore.Mvc;
using Movie_Store_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie_Store_API.Extensions;
using Microsoft.Extensions.Configuration;
using Movie_Store_API.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace Movie_Store_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly string secretkey = string.Empty;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;

            secretkey = _configuration.GetValue<string>("Secretkey");
        }

        [HttpGet("login")]
        [Authorize]
        public IActionResult GetValue()
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest user)
        {
            var validUser = await _userService.Login(user.Username, user.Password);

            if (validUser != null)
            {
                string getToken = JwtHelpers.GetInstance(secretkey).CreateToken(validUser);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    success = true,
                    message = "Login successful, your token will expired in 20 minutes!",
                    response = new UserResonpse
                    {
                        Username = validUser.Username,
                        Token = getToken
                    }
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { message = "Username or password is invalid!" });
            }
        }
    }
}
