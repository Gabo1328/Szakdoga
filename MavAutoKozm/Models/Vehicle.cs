using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MavAutoKozm.Models
{
    public class Vehicle
    {
        //PK
        public int Id { get; set; }

        //FK
        [ForeignKey("AppUserId")]
        [HiddenInput]
        public int AppUserId { get; set; }

        [Display(Name = "Márka")]
        public string Brand { get; set; }

        [Display(Name = "Modell")]
        public string Model { get; set; }

        [Display(Name = "Típus")]
        public string Type { get; set; }
        
        [Display(Name = "Szín")]
        public string Color { get; set; }
        
        [Display(Name = "Rendszám")]
        public string NumberPlate { get; set; }
    }
}
