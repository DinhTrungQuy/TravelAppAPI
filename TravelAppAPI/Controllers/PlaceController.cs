using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAppAPI.Model;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController(PlaceServices placeServices) : ControllerBase
    {
        private readonly PlaceServices _placeServices = placeServices;

        [HttpGet]
        public async Task<ActionResult<Place>> Get()
        {

            return Ok(await _placeServices.GetAsync());
        }
        [HttpGet("{id:length(24)}", Name = "GetPlace")]
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
        public async Task<ActionResult<Place>> Post(Place place)
        {
            await _placeServices.CreateAsync(place);
            return CreatedAtRoute("GetPlace", new { id = place.Id.ToString() }, place);
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
