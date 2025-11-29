using EquineConnect.Core.Interfaces;
using EquineConnect.Core.Models;
using EquineConnect.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EquineConnect.Api.Controllers
{
    [Route("api/horses")]
    [ApiController]
    public class HorseController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IHorseService _horseService;

        public HorseController(UserManager<User> userManager, IHorseService horseService)
        {
            _userManager = userManager;
            _horseService = horseService;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<Horse>> AddHorse([FromBody] Horse horse)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var newHorse = await _horseService.AddHorse(horse, user.Id);

            if (newHorse == null)
            {
                return StatusCode(500, "Could not add horse");
            }

            return newHorse != null ? Ok(newHorse) : StatusCode(500, "Could not add horse");
        }
    }
}