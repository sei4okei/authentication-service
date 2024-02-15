namespace AuthenticationService.Models
{
    public class StatusModel
    {
        public string Code { get; set; } = "";
        public string Action { get; set; } = "";
        public string? Error { get; set; }
        public string? Role { get; set; }
        public string? User { get; set; }
    }
}
