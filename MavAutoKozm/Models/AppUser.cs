using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MavAutoKozm.Models
{
    public class AppUser
    {
        public int ID { get; set; }

        [Display(Name = "Keresztnév")]
        public string LastName { get; set; }

        [Display(Name = "Vezetéknév")]
        public string FirstMidName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Telefonszám")]
        public string PhoneNumber { get; set; }

        //FK: regisztrált user-hez való kötés
        //9eac65ed-c99b-4087-8496-7b4fef719677
        [HiddenInput]
        public string AspNetUserId { get; set; }

        //navigációs property

        [Display(Name = "Járműveim")]
        public ICollection<Vehicle> Vehicles { get; set; }

        //tovább fejlesztési lehetőségek (pl: vevő értékelés)
    }
}
