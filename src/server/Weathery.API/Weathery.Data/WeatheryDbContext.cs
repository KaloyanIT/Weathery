using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weathery.Data.Models;

namespace Weathery.Data
{
    public class WeatheryDbContext : IdentityDbContext<User>
    {
        public WeatheryDbContext(DbContextOptions<WeatheryDbContext> options)
            : base(options)
        {
        }
    }
}
