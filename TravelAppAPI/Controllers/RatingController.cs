using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAppAPI.Model;
using TravelAppAPI.Models;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController(RatingServices ratingServices, PlaceServices placeServices) : ControllerBase
    {

        private readonly RatingServices _ratingServices = ratingServices;
        private readonly PlaceServices _placeServices = placeServices;

        [HttpGet]
        public async Task<ActionResult<string>> GetRating(string placeId)
        {
            var ratingList = await _ratingServices.GetByPlaceIdAsync(placeId);
            if (ratingList == null)
            {
                return BadRequest("No rating found");
            }
            var rateValue = ratingList.Average(r => r.RatingValue);

            return Ok(rateValue.ToString());
        }
        [HttpPost]
        public async Task<ActionResult<Rating>> PostRating(Rating rating)
        {

            var ratingList = await _ratingServices.GetByPlaceIdAsync(rating.PlaceId);
            if (ratingList.Any(r => r.UserId == rating.UserId))
            {
                return BadRequest("User already rated this place");
            }
            ratingList.Add(rating);
            var rateValue = Math.Round(ratingList.Average(r => r.RatingValue), 1);

            await _ratingServices.InsertAsync(rating);
            await _placeServices.UpdateRating(rating.PlaceId, rateValue);
            return Ok(rating);
        }


    }
}
