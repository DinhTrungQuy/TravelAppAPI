using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelAppAPI.Models;
using TravelAppAPI.Models.Dto;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController(AuthServices authServices) : ControllerBase
    {
        private readonly AuthServices _authServices = authServices;
        [HttpGet]
        public async Task<ActionResult<User>> Get()
        {
            return Ok(await _authServices.GetAsync());
        }
        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _authServices.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Login(LoginDto user, IOptions<JwtSettings> settings)
        {
            var userId = await _authServices.CheckExist(user.Username, user.Password);
            var ExpriredTime = DateTime.Now.AddHours(1);
            if (!String.IsNullOrEmpty(userId))
            {
                var issuer = settings.Value.Issuer;
                var audience = settings.Value.Audience;
                var key = Encoding.ASCII.GetBytes(settings.Value.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                                new Claim("Id", userId),
                                new Claim("Username", user.Username),
                            }),
                    Expires = ExpriredTime,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                CookieOptions option = new()
                {
                    Expires = ExpriredTime,
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                };
                Response.Cookies.Append("Token", stringToken, option);
                return Ok(stringToken);
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Register(User user)
        {
            if (await _authServices.CheckExistUser(user.Username))
            {
                return BadRequest("Username is already exist");
            }
            user.Password = _authServices.CreateMD5(user.Password);
            await _authServices.CreateAsync(user);
            return Ok();
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, User userIn)
        {
            var user = _authServices.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _authServices.UpdateAsync(id, userIn);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = _authServices.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _authServices.RemoveAsync(id);
            return NoContent();
        }
    }
}