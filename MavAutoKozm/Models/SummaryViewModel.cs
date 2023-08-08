using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MavAutoKozm.Models
{
    public class SummaryViewModel
    {
        public AppUser ActualAppUser { get; set; }
        public ServiceSelectViewModel SelectedServices { get; set; }

        [Display(Name = "Ár")]
        public int Price { get; set; }

        [Display(Name = "Megrendelési idő")]
        public DateTime OrderTime { get; set; }

        [Display(Name = "Végrehajtási idő")]
        public DateTime CompletedTime { get; set; }

        //ToDo Utolső módosító
        //ToDo Utolsó módosítás ideje
    }
}
