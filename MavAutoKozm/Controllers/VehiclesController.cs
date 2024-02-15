using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Authorization;

namespace MavAutoKozm.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly string _felhasznaloId= "FelhasznaloId";
        //private readonly MavAutoKozmDbContext _context;
        private readonly IMavAutoKozmRepository _context; //Ezzel éri el az adatbázist


        public VehiclesController(IMavAutoKozmRepository context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Alkalmazott"))
                return _context.Vehicles != null ?
                          View(_context.Vehicles.ToList()) :
                          Problem("Entity set 'MavAutoKozmDbContext.Vehicles'  is null.");


            var FelhasznaloId = HttpContext.Session.GetInt32(_felhasznaloId);

            return _context.Vehicles != null ?
                          View(_context.Vehicles.Where(jarmu => jarmu.AppUserId == FelhasznaloId).ToList()) :
                          Problem("Entity set 'MavAutoKozmDbContext.Vehicles'  is null.");
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle =_context.Vehicles
                .FirstOrDefault(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            var vehicle = new Vehicle();
            vehicle.AppUserId = HttpContext.Session.GetInt32(_felhasznaloId).Value; //Figyelem ez Null is lehet
            return View(vehicle);
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppUserId,Brand,Model,Type,Color,NumberPlate")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
               
                _context.VehiclesAdd(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);

        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = _context.Vehicles.Find(x => x.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppUserId,Brand,Model,Type,Color,NumberPlate")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.VehiclesUpdate(vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = _context.Vehicles
                .FirstOrDefault(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'MavAutoKozmDbContext.Vehicles'  is null.");
            }
            var vehicle = _context.Vehicles.Find(v => v.Id == id);
            if (vehicle != null)
            {
                _context.VehiclesDelete(vehicle);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return (_context.Vehicles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
