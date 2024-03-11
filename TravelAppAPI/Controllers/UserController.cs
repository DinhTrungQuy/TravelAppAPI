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
    public class UserController(UserServices userServices,FileServices fileServices, AuthServices authServices) : ControllerBase
    {
        private readonly UserServices _userServices = userServices;
        private readonly FileServices _fileServices = fileServices;
        private readonly AuthServices _authServices = authServices;


  

        [HttpGet]
        public async Task<ActionResult<User>> Get()

        {
            var request = HttpContext.Request;
            string userId = _userServices.DecodeJwtToken(request);
            return Ok(await _userServices.GetAsync(userId));

        }
        [HttpPost]
        [Route("ImageUpload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            var request = HttpContext.Request;
            string userId = _userServices.DecodeJwtToken(request);
            var filePath = await _fileServices.SaveUserFile(file, userId);
            var user = await _userServices.GetAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }
            user.ImageUrl = "https://quydt.speak.vn/images/user" + userId + Path.GetExtension(filePath);
            await _authServices.UpdateAsync(userId, user);
            return Ok(user);
        }
    }
}
