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
        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Roles");
                else
                    ViewData["Hibauzenet"] = $"Nem sikerült létrehozni a Szerepkört! ({result.Errors.First()?.Description})";
            }
            return View();
        }

        public ViewResult Edit() => View(_userManager.Users.Select(x => new UsersInRole { Name = x.UserName, UserId = x.Id, Member = true }));
        //new List<UsersInRole>());

        public async Task<IActionResult> DeleteRole(string id)
        {
            if (_roleManager == null)
            {
                return Problem("Role is null.");
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction(nameof(Roles));
        }
    }
}
