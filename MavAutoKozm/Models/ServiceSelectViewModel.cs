using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MavAutoKozm.Models
{
    public class ServiceSelectViewModel
    {
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

        [Display(Name = "Melyik járművét választja ki?")]
        public int SelectedVehicleId { get; set; }

        public int Price { get; set; }

        public List<Vehicle> Vehicles { get; set; }
    }
}
