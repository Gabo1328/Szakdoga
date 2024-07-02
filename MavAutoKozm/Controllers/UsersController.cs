using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MavAutoKozm.Data;
using MavAutoKozm.Models;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MavAutoKozm.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly string _felhasznaloId = "FelhasznaloId";
        //private readonly MavAutoKozmDbContext _context;
        private readonly IMavAutoKozmRepository _context; //Ezzel éri el az adatbázist

        private UserManager<IdentityUser> _userManager;

        public UsersController(IMavAutoKozmRepository context, UserManager<IdentityUser> userMgr)
        {
            _context = context;
            _userManager = userMgr;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (User != null)
            {
                //Dolgozó vagy Adminnak teljes lista jelenik meg
                if (User.IsInRole("Admin") || User.IsInRole("Alkalmazott"))
                    return _context.AppUsers != null ?
                              View(_context.AppUsers.ToList()) :
                              Problem("Entity set 'MavAutoKozmDbContext.Users'  is null.");
            }
            //Ha nincs felhasználója akkor létrehozunk egyet
            //Ha van akkor meg a Details-be küldjük
            var FelhasznaloId = HttpContext?.Session.GetInt32(_felhasznaloId);
            if (FelhasznaloId==null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Details", new { id = FelhasznaloId });
            }
        }

        /// <summary>
        /// Felhasználók részletei
        /// </summary>
        /// <param name="id">userId</param>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            //var user = await _context.AppUsers.Include(v => v.Vehicles)
            //   .FirstOrDefaultAsync(m => m.ID == id);  "Az Include-al egyszerűen hozzáadtuk a User.hez a jármű táblát"
              
            var user = _context.AppUsers.FirstOrDefault(m => m.ID == id);
            
            if (user == null)
            {
                return NotFound();
            }

            user.Vehicles =_context.Vehicles.FindAll(v => v.AppUserId == id);

            return View(user);
        }

        /// <summary>
        /// Felhasználók létrehozása
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {                      
            return View();
        }

        /// <summary>
        /// Felhasználók létrehozása
        /// </summary>
        /// <param name="user">userId</param>
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,Email,PhoneNumber,AspNetUserId")] AppUser user)
        {
            //if (ModelState.IsValid)           
            {
                user.AspNetUserId = User.Claims.FirstOrDefault
                (x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                
                _context.AppUsersAdd(user);
                
                HttpContext.Session.SetInt32(_felhasznaloId, user.ID);

                return RedirectToAction("Details", new { id = user.ID });
            }
            return View(user);
        }

        /// <summary>
        /// Felhasználók szerkesztése
        /// </summary>
        /// <param name="id">userId</param>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var user = _context.AppUsers.Find(x => x.ID == id);//Include(v => v.Vehicles)
            if (user == null)
            {
                return NotFound();
            }
            user.Vehicles = new List<Vehicle>();
            return View(user);
        }

        /// <summary>
        /// Felhasználók szerkesztése
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,Email,PhoneNumber,AspNetUserId")] AppUser user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.AppUsersUpdate(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        /// <summary>
        /// Felhasználók törlése
        /// </summary>
        /// <param name="id">userId</param>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var user = _context.AppUsers
                .FirstOrDefault(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        /// <summary>
        /// Felhasználók törlése
        /// </summary>
        /// <param name="id">userId</param>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppUsers == null)
            {
                return Problem("Entity set 'MavAutoKozmDbContext.AppUsers'  is null.");
            }
            var user = _context.AppUsers.Find(x => x.ID == id);
            if (user != null)
            {
                _context.AppUsersDelete(user);
            }
            
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ChangeRole(int? appuserid) 
        {
            var appuser = _context.AppUsers.Find(x => x.ID == appuserid);
            var result = await _userManager.AddToRoleAsync(_userManager.FindByIdAsync(appuser.AspNetUserId).Result, "Alkalmazott");
            return RedirectToAction("Roles","Role");
        }

        public bool UserExists(int id)
        {
          return (_context.AppUsers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
