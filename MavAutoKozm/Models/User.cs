namespace MavAutoKozm.Models
{
    public class User
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        //FK: regisztrált user-hez való kötés
        //9eac65ed-c99b-4087-8496-7b4fef719677
        public string AspNetUserId { get; set; }

        //navigációs property
        public ICollection<Vehicle> Vehicles { get; set; }

        //tovább fejlesztési lehetőségek (pl: vevő értékelés)
    }
}
