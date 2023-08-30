using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data
{
    public class ServiceContext : IdentityDbContext/*<UserModel, UserRoles,  string>*/
    {
        //public DbSet<UserModel> User { get; set; }

        public ServiceContext(DbContextOptions<ServiceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
