using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class ServiceContext : IdentityDbContext<User>
    {

        public ServiceContext(DbContextOptions<ServiceContext> options)
            : base(options)
        {

        }
    }
}
