﻿namespace AuthenticationService.Models.DTOs
{
    public class RegisterDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
