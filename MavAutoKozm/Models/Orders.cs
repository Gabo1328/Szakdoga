using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MavAutoKozm.Models
{
    public class Orders
    {
        //PK
        public int Id { get; set; }

        //FK
        [ForeignKey("AppUserId")]
        [HiddenInput]
        public int AppUserId { get; set; }

        [ForeignKey("VehicleId")]
        public int VehicleId { get; set; }

        [Display(Name = "Kategória")]
        public int Category { get; set; }

        [Display(Name = "Külső tisztítás")]
        public bool Outer { get; set; }

        [Display(Name = "Belső tisztítás")]
        public bool Inner { get; set; }

        [Display(Name = "Polírozás")]
        public bool Polish { get; set; }

        [Display(Name = "Waxolás")]
        public bool Wax { get; set; }

        [Display(Name = "Kerámia bevonat")]
        public bool Ceramic { get; set; }

        [Display(Name = "Kavics védő fólia")]
        public bool Ppf { get; set; }
        
        [Display(Name = "Minőségi szint")]
        public int Quality { get; set; }

        [Display(Name = "Ár")]
        public int Price { get; set; }

        [Display(Name = "Megrendelési idő")]
        public DateTime OrderTime { get; set; }

        [Display(Name = "Végrehajtási idő")]
        public DateTime CompletedTime { get; set; }

        [Display(Name = "Állapot")]
        public int Progress { get; set; }

        //ToDo Utolső módosító
        //ToDo Utolsó módosítás ideje
    }
}
