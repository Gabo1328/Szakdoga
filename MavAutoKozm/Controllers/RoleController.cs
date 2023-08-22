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

        //Edit függvénnyel egyszerű módon jelenítjük meg a view-t: =>
        //A View függvénynek átadjuk a megjeleníteni kívánt UsersInRole listát.
        //A _usermanagers.Users a weboldalra regisztrált összes user-t tartalmazza(táblanév:AspNetUsers)
        //Végig gyalogolunk ezen a listán most épp a select utasítással
        //A select azt csinálja hogy az adott lista elemeket felhasználhatom további értékadásra
        //A new UsersInRole a végig gyaloglás során minden egyes listaelemen végig fog futni
        //usersInRole 3 property-ét a végiggyaloglás során az x-nek nevezett adott listaelemből feltöltjük(Minden Member jelenleg "true" próbaként)
        //Annyi elemű lista lesz a UsersInRole ahány eleme van a Users-nek
        //a Select kifejezetten erre való: --Egy adott listán végig menve azokból valamilyen információt kiszedve teljesen új osztáy listát hozol létre--
        public ViewResult Edit_Old() => View(_userManager.Users.Select(x => new UsersInRole { Name = x.UserName, UserId = x.Id, Member = true }));
        //new List<UsersInRole>());

        public async Task<IActionResult> Edit(string id)
        {
            var lista = new List<UsersInRole>();
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                //Első megoldás
                
                foreach (var item in _userManager.Users)
                {                   
                    var member = await _userManager.IsInRoleAsync(item, role.Name);  
                    var listaelem = new UsersInRole { Name = item.UserName, UserId = item.Id, Member = member };
                    lista.Add(listaelem);
                }

                //Második megoldás
                //var lista2 = _userManager.Users.Select(x => new UsersInRole
                //{
                //    Name = x.UserName,
                //    UserId = x.Id,
                //    Member = _userManager.IsInRoleAsync(x, role.Name).Result
                //});
            }
            return View(lista);
            
        }

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
