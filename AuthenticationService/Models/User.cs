using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Models
{
    public class User : IdentityUser
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
