using AuthenticationService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data
{
    public class ServiceContext : IdentityDbContext<User>
    {

        public ServiceContext(DbContextOptions<ServiceContext> options)
            : base(options)
        {

        }
    }
}
