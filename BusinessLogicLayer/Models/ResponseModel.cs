﻿namespace BusinessLogicLayer.Models
{
    public class ResponseModel
    {
        public string Code { get; set; } = "";
        public string Action { get; set; } = "";
        public string? Error { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? User { get; set; }
    }
}
