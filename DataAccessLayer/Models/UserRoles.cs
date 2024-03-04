using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class UserRoles : IdentityRole
    {
        public const string Admin = "admin";
        public const string User = "user";
    }
}
