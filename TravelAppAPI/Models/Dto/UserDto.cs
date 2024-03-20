﻿namespace TravelAppAPI.Models.Dto
{
    public class UserDto
    {
        public string Id { get; set; } = String.Empty;
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty; 
        public string Role { get; set; } = String.Empty;
        public string Fullname { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;

        public IFormFile? Image { get; set; }
    }
}
