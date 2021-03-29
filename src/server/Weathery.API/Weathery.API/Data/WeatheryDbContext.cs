using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weathery.API.Data.Models;

namespace Weathery.API.Data
{
    public class WeatheryDbContext : IdentityDbContext<User>
    {
        public WeatheryDbContext(DbContextOptions<WeatheryDbContext> options)
            : base(options)
        {
        }
    }
}
