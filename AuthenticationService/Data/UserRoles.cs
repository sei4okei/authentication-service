using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Data
{
    public class UserRoles : IdentityRole
    {
        public const string Admin = "admin";
        public const string User = "user";
    }
}
