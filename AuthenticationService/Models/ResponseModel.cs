﻿namespace AuthenticationService.Models
{
    public class ResponseModel
    {
        public string Code { get; set; } = "";
        public string Action { get; set; } = "";
        public string? Error { get; set; }
    }
}