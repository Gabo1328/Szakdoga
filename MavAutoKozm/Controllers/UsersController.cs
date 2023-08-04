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

namespace MavAutoKozm.Controllers
{
    public class UsersController : Controller
    {
        private readonly string _felhasznaloId = "FelhasznaloId";
        private readonly MavAutoKozmDbContext _context;

        public UsersController(MavAutoKozmDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var FelhasznaloId = HttpContext.Session.GetInt32(_felhasznaloId);
            if (FelhasznaloId==null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Details", new { id = FelhasznaloId });
            }           

            //Todo dolgozó belépésénél ez kell majd:
            return _context.AppUsers != null ? 
                          View(await _context.AppUsers.ToListAsync()) :
                          Problem("Entity set 'MavAutoKozmDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AppUsers.Include(v => v.Vehicles)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {                      
            return View();
        }

        // POST: Users/Create
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
                
                _context.AppUsers.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AppUsers.FindAsync(id);//Include(v => v.Vehicles)
            if (user == null)
            {
                return NotFound();
            }
            user.Vehicles = new List<Vehicle>();
            return View(user);
        }

        // POST: Users/Edit/5
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

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppUsers == null)
            {
                return Problem("Entity set 'MavAutoKozmDbContext.AppUsers'  is null.");
            }
            var user = await _context.AppUsers.FindAsync(id);
            if (user != null)
            {
                _context.AppUsers.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.AppUsers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
