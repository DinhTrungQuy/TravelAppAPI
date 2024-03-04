﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAppAPI.Models;
using TravelAppAPI.Sevices;

namespace TravelAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController(WishlistServices wishlistServices) : ControllerBase
    {
        private readonly WishlistServices _wishlistServices = wishlistServices;
        [HttpGet]
        public async Task<ActionResult<Wishlist>> Get()
        {
            return Ok(await _wishlistServices.GetAsync());
        }
        [HttpGet("{id:length(24)}", Name = "GetWishlist")]
        public async Task<ActionResult<Wishlist>> Get(string id)
        {
            var wishlist = await _wishlistServices.GetAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            return Ok(wishlist);
        }
        [HttpPost]
        public async Task<ActionResult<Wishlist>> Post(Wishlist wishlist)
        {
            await _wishlistServices.CreateAsync(wishlist);
            return CreatedAtRoute("GetWishlist", new { id = wishlist.Id.ToString() }, wishlist);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Wishlist wishlistIn)
        {
            var wishlist = _wishlistServices.GetAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            await _wishlistServices.UpdateAsync(id, wishlistIn);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var wishlist = _wishlistServices.GetAsync(id);
            if (wishlist == null)
            {
                return NotFound();
            }
            await _wishlistServices.RemoveAsync(id);
            return NoContent();
        }
    }

}