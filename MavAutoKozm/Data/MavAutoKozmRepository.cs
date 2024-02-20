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
        public void VehiclesUpdate(Vehicle update);
        public void VehiclesDelete(Vehicle delete);
        public void VehiclesAdd(Vehicle add);
        public void AppUsersAdd(AppUser add);
        public void AppUsersDelete(AppUser delete);
        public void AppUsersUpdate(AppUser update);







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

        public void VehiclesUpdate(Vehicle update)
        {
            _dbContext.Vehicles.Update(update);
            _dbContext.SaveChanges();
        }

        public void VehiclesDelete(Vehicle delete)
        {
            _dbContext.Vehicles.Remove(delete);
            _dbContext.SaveChanges();
        }

        public void VehiclesAdd(Vehicle add)
        {
            _dbContext.Vehicles.Add(add);
            _dbContext.SaveChanges();
        }
        public void AppUsersAdd(AppUser add)
        {
            _dbContext.AppUsers.Add(add);
            _dbContext.SaveChanges();
        }
        public void AppUsersUpdate(AppUser update)
        {
            _dbContext.AppUsers.Update(update);
            _dbContext.SaveChanges();
        }
        public void AppUsersDelete(AppUser delete)
        {
            _dbContext.AppUsers.Remove(delete);
            _dbContext.SaveChanges();
        }


    }
}
