using MavAutoKozm.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MavAutoKozm.Data
{
    public class MavAutoKozmDbContext : DbContext
    {
        public MavAutoKozmDbContext(DbContextOptions<MavAutoKozmDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        //public DbSet<MavAutoKozm.Models.Orders>? Orders { get; set; }
    }
}