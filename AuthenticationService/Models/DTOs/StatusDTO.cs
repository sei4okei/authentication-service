namespace AuthenticationService.Models.DTOs
{
    public class StatusDTO
    {
        public string Code { get; set; } = "";
        public string Action { get; set; } = "";
        public string? Error { get; set; }
        public string? Role { get; set; }
        public string? User { get; set; }
    }
}
