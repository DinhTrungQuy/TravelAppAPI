using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAppAPI.Models;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserServices userServices) : ControllerBase
    {
        private readonly UserServices _userServices = userServices;
        //[HttpGet]
        //public async Task<ActionResult<User>> Get()
        //{
           
        //}
    }
}
