using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movie_Store_API.Extensions;
using Movie_Store_API.Services.Interface;
using Movie_Store_API.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using Movie_Store_Data.Models;

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
            return Ok(new
            {
                message = "Authorize",
                success = true
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (user == null) return StatusCode(StatusCodes.Status400BadRequest);

            try
            {
                await _userService.AddUserAsync(user);
                await _userService.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created,
                    new
                    {
                        message = "Register successfully",
                        success = true
                    });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "Server error!",
                    success = false
                });
            }
        }

        [Authorize]
        [HttpPost("login/renew")]
        public async Task<IActionResult> RenewToken()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var validToken = JwtHelpers.GetInstance(secretkey).IsValidToken(token);

            string id = validToken.Claims.First(x => x.Type.Equals("id")).Value.ToString();

            var user = await _userService.GetUserByIDAsync(id);

            return Ok(new
            {
                token = JwtHelpers.GetInstance(secretkey).CreateToken(user)
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest user)
        {
            var validUser = await _userService.Login(user.Username, user.Password);

            if (validUser != null)
            {
                string getToken = JwtHelpers.GetInstance(secretkey).CreateToken(validUser, 60);

                return StatusCode(StatusCodes.Status200OK, new
                {
                    message ="Authorized, your token will expired in 60 minutes",
                    success = true,
                    token = getToken
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { success = false, token = "" });
            }
        }
    }
}
