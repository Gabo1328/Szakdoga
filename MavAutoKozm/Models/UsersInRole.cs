using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public class UserInRoleList : PageModel
    {
        [BindProperty]
        public List<UsersInRole> list { get; set; }

        public int Valami { get; set; }

        public ActionResult OnPost([FromForm] UserInRoleList lista) 
        { 
            return Page(); 
        }
    }
}
