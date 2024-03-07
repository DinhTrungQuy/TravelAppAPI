using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TravelAppAPI.Models;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(UserServices userServices) : ControllerBase
    {
        private readonly UserServices _userServices = userServices;

  

        [HttpGet]
        public async Task<ActionResult<User>> Get()

        {
            var request = HttpContext.Request;
            string userId = _userServices.DecodeJwtToken(request);
            return Ok(await _userServices.GetAsync(userId));

        }
    }
}
