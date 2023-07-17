namespace MavAutoKozm.Models
{
    public class User
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        //navigációs property
        public ICollection<Vehicle> Vehicles { get; set; }

        //tovább fejlesztési lehetőségek (pl: vevő értékelés)

    }
}
