using MavAutoKozm.Controllers;
using MavAutoKozm.Models;
using Microsoft.EntityFrameworkCore;

namespace MavAutoKozm.Data
{
    public interface IMavAutoKozmRepository
    {
        List<AppUser> AppUsers { get;}
        List<Vehicle> Vehicles { get;}
        List<Orders> Orders { get;}


        public void OrdersDelete(Orders delete);
        public void OrdersAdd(Orders add);

        void Save();

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

        public List<AppUser> AppUsers => _dbContext.AppUsers.ToList();
        public List<Vehicle> Vehicles => _dbContext.Vehicles.ToList();
        public List<Orders> Orders => _dbContext.Orders.ToList();

        public void Save() 
        {
            _dbContext.SaveChanges();
        }

        public void OrdersDelete(Orders delete)
        {
            _dbContext.Orders.Remove(delete);
            _dbContext.SaveChanges();
        }

        public void OrdersAdd(Orders add)
        {
            _dbContext.Orders.Add(add);
            _dbContext.SaveChanges();
        }

    }
}
