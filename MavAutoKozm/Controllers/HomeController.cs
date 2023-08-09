using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MavAutoKozm.Controllers
{
    public class HomeController : Controller
    {
        //Elírás megelőzés
        private readonly string _felhasznaloId = "FelhasznaloId";
        private readonly string _elmentettIgenyek = "ElmentettIgenyek";
        private readonly MavAutoKozmDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MavAutoKozmDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var AspNetUserId = User.Claims.FirstOrDefault
             (x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            var Felhasznalo = _context.AppUsers.FirstOrDefault(x => x.AspNetUserId == AspNetUserId);

            if (Felhasznalo != null)
                HttpContext.Session.SetInt32(_felhasznaloId, Felhasznalo.ID);
            else
                HttpContext.Session.Remove(_felhasznaloId);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult CategorySelect()
        {
            //Korábban elmentett igények törlése a sessionből(ServiceSelect nulláról induljon)
            HttpContext.Session.SetObject(_elmentettIgenyek, null);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //érték átadás
        public IActionResult ServiceSelect(int category)
        {
            var atadando_ertek = HttpContext.Session.GetObject<ServiceSelectViewModel>(_elmentettIgenyek);
            if (atadando_ertek == null)
            {
                atadando_ertek = new ServiceSelectViewModel();
                atadando_ertek.Category = category;
            }
            atadando_ertek.Vehicles = _context.Vehicles.Where(v=>v.AppUserId == HttpContext.Session.GetInt32(_felhasznaloId)).ToList();
            //Default értékek kiválasztása
            //atadando_ertek.Inner = true;
            return View(atadando_ertek);
        }

        // POST: ServiceSelectViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ServiceSelect([Bind("Category,Outer,Inner,Polish,Wax,Ceramic,Ppf,Quality,SelectedVehicleId")] ServiceSelectViewModel serviceSelectViewModel)
        {
            //if (ModelState.IsValid)
            {
                //ToDo adatok elmentése
                HttpContext.Session.SetObject(_elmentettIgenyek,serviceSelectViewModel);
                //ToDo összegző oldalra navigálás
                return RedirectToAction(nameof(Summary));
            }
            return View(serviceSelectViewModel);
        }

        public IActionResult Summary()
        {
            var atadando_ertek = new SummaryViewModel();
            //atadando_ertek.Category = category;
            //Default értékek kiválasztása
            atadando_ertek.Price = 2000;
            atadando_ertek.OrderTime = DateTime.Now; 
            atadando_ertek.CompletedTime = DateTime.Now.AddDays(3);
            var FelhasznaloId = HttpContext.Session.GetInt32(_felhasznaloId);
            //atadando_ertek.ID = FelhasznaloId.Value;
            atadando_ertek.ActualAppUser = _context.AppUsers.FirstOrDefault(v => v.ID == HttpContext.Session.GetInt32(_felhasznaloId));
            var elmentett_igenyek = HttpContext.Session.GetObject<ServiceSelectViewModel>(_elmentettIgenyek);
            atadando_ertek.SelectedVehicle = _context.Vehicles.FirstOrDefault(v => v.Id == elmentett_igenyek.SelectedVehicleId);
            atadando_ertek.SelectedServices = elmentett_igenyek;
            return View(atadando_ertek);
        }
        public IActionResult SaveToDatabase()
        {
            //_context.Add();
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            return View();
        }
    }
}