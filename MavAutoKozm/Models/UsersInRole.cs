using System.ComponentModel.DataAnnotations;

namespace MavAutoKozm.Models
{
    public class UsersInRole
    {
        //Generálás idejére került felvételre
        //public int Id { get; set; } 

        public string Name { get; set; }

        [Key]
        public string UserId { get; set; }

        public bool Member { get; set; }
    }
}
