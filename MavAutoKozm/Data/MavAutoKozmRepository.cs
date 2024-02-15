using MavAutoKozm.Controllers;
using MavAutoKozm.Models;
using Microsoft.EntityFrameworkCore;

namespace MavAutoKozm.Data
{
    public interface IMavAutoKozmRepository
    {
        IEnumerable<AppUser> AppUsers { get;}
        IEnumerable<Vehicle> Vehicles { get;}
        IEnumerable<Orders> Orders { get;}


        //void AddAppUser(AppUser AppUsers);

        //void EditAppUser(AppUser AppUsers);

        //void RemoveAppUser(AppUser AppUsers);
    }

    public class MavAutoKozmRepository : IMavAutoKozmRepository
    {
        private readonly MavAutoKozmDbContext _dbContext; //Ezzel éri el az adatbázist
        public MavAutoKozmRepository(MavAutoKozmDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public IEnumerable<AppUser> AppUsers => _dbContext.AppUsers.ToList();
        public IEnumerable<Vehicle> Vehicles => _dbContext.Vehicles.ToList();
        public IEnumerable<Orders> Orders => _dbContext.Orders.ToList();

    }
}
