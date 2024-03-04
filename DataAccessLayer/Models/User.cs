using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class User : IdentityUser
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
