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
        public DbSet<User> Users { get; set; }
       // public DbSet<Vehicle> Vehicles { get; set; }
    }
}