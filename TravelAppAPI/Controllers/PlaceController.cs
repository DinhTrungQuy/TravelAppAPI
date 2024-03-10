using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAppAPI.Model;
using TravelAppAPI.Models;
using TravelAppAPI.Models.Config;
using TravelAppAPI.Models.Dto;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PlaceController(PlaceServices placeServices, FileServices fileServices) : ControllerBase
    {
        private readonly PlaceServices _placeServices = placeServices;
        private readonly FileServices _fileServices = fileServices;
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<Place>> Get()
        {

            return Ok(await _placeServices.GetAsync());
        }
        [HttpGet("{id:length(24)}", Name = "GetPlace")]
        [AllowAnonymous]
        public async Task<ActionResult<Place>> Get(string id)
        {
            var place = await _placeServices.GetAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            return Ok(place);
        }
        [HttpPost]
        public async Task<ActionResult<Place>> Post(PlaceDto place)
        {
            var mapper = MapperConfig.Initialize();
            var placeModel = mapper.Map<Place>(place);
            await _placeServices.CreateAsync(placeModel);
            var filePath = await _fileServices.SavePlaceFile(place.Image!, placeModel.Id);
            placeModel.ImageUrl = filePath;
            await _placeServices.UpdateAsync(placeModel.Id, placeModel);
            return Ok(placeModel);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Place placeIn)
        {
            var place = _placeServices.GetAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            await _placeServices.UpdateAsync(id, placeIn);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var place = _placeServices.GetAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            await _placeServices.RemoveAsync(id);
            return NoContent();
        }
    }
}
