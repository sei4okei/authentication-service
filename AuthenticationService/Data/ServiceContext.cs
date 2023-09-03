using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Data
{
    public class ServiceContext : IdentityDbContext
    {

        public ServiceContext(DbContextOptions<ServiceContext> options)
            : base(options)
        {

        }
    }
}
