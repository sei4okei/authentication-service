namespace AuthenticationService.Models.DTOs
{
    public class ResponseDTO
    {
        public string Code { get; set; } = "";
        public string Action { get; set; } = "";
        public string? Error { get; set; }
    }
}
