using EquineConnect.Core.Interfaces;
using EquineConnect.Core.Models;
using EquineConnect.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EquineConnect.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        //private readonly IConfiguration _config;
        private readonly IAuthService _authService;

        public AuthController(UserManager<User> userManager, IConfiguration config, IAuthService authService)
        {
            _userManager = userManager;
            //_config = config;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register register)
        {
            var result = await _authService.Register(register);

            return result != null ? Ok(result) : BadRequest("User creation failed");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var token = await _authService.Login(login);
            return token != null ? Ok(new { token }) : Unauthorized("Invalid credentials");
        }

        //[HttpPost("forgot-password")]
        //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        //{
        //    var result = await _authService.ForgotPassword(forgotPassword);
        //    return Ok(new { Message = result });
        //}

        //[HttpPost("reset-password")]
        //public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        //{
        //    var result = await _authService.ResetPassword(resetPassword);
        //    return Ok(new { Message = result });
        //}
    }
}