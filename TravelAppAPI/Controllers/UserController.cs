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
            String authHeader = HttpContext.Request.Headers.Authorization!;
            authHeader = authHeader.Replace("Bearer ", String.Empty);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            string userId = tokenS!.Claims.First(claim => claim.Type == "Id").Value;
            return Ok(await _userServices.GetAsync(userId));

        }
    }
}
