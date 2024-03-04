using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAppAPI.Models;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<User>> Post(User user)
        {
            await _authServices.CreateAsync(user);
            return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
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
