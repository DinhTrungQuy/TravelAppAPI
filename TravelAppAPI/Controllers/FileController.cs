using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController(UserServices userServices) : ControllerBase
    {
        private readonly UserServices _userServices = userServices;

        [HttpPost]
        [Route("UploadUserImage")]
        public async Task<IActionResult> UploadUserImage(IFormFile file)
        {
            var request = HttpContext.Request;
            var userId = _userServices.DecodeJwtToken(request);
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            var path = Path.Combine("D:\\Publish\\IIS\\quydt.speak.vn_Images\\users", userId + Path.GetExtension(file.FileName));

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok("File uploaded successfully");
        }
       
    }
}
