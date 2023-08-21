using MavAutoKozm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MavAutoKozm.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userMgr)
        {
            _roleManager = roleMgr;
            _userManager = userMgr;
        }

        public IActionResult Roles()    
        {
            //_userManager.GetUserId()
            return View(_roleManager.Roles);
        }

        //A view így is meghívható
        //public ViewResult Index() => View(roleManager.Roles);
       
    }
}
