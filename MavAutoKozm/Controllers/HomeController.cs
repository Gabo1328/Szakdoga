using MavAutoKozm.Data;
using MavAutoKozm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace MavAutoKozm.Controllers
{
    public class HomeController : Controller
    {
        //Elírás megelőzés
        private readonly string _felhasznaloId = "FelhasznaloId";
        private readonly string _elmentettIgenyek = "ElmentettIgenyek";
        private readonly MavAutoKozmDbContext _context; //Ezzel éri el az adatbázist
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MavAutoKozmDbContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException("context");
            }
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
                serviceSelectViewModel.Price = GetPrice(serviceSelectViewModel);
                //ToDo adatok elmentése
                HttpContext.Session.SetObject(_elmentettIgenyek,serviceSelectViewModel);
                //ToDo összegző oldalra navigálás
                return RedirectToAction(nameof(Summary));
            }
            //return View(serviceSelectViewModel); (ha az if visszakerül ez a sor is kell)
        }

        //3 /-el autómatikusan feljön a dokumnetálást és tool tippet adó információ

        /// <summary>
        /// Ár kiszámítása: 10.000 x kiválasztott szolgáltatás x minőségi szint +1 (1-2-3)
        /// </summary>
        /// <param name="selected_needs">a</param>
        /// <returns>A kiszámolt ár</returns>
        public int GetPrice(ServiceSelectViewModel selected_needs) 
        {
            int Price = 0;
            int Db = 0;
            if (selected_needs.Outer)
                Db++;
            if (selected_needs.Inner)
                Db++;
            if (selected_needs.Polish)
                Db++;
            if (selected_needs.Wax)
                Db++;
            if (selected_needs.Ceramic)
                Db++;
            if (selected_needs.Ppf)
                Db++;
            Price = Db * 10000 * (selected_needs.Quality + 1);
            return Price;
        }

        public IActionResult Summary()
        {
            var atadando_ertek = new SummaryViewModel();
            //atadando_ertek.Category = category;
            //Default értékek kiválasztása
            //atadando_ertek.Price = 2000;
            atadando_ertek.OrderTime = DateTime.Now; 
            atadando_ertek.CompletedTime = DateTime.Now.AddDays(3);
            var FelhasznaloId = HttpContext.Session.GetInt32(_felhasznaloId);
            //atadando_ertek.ID = FelhasznaloId.Value;
            atadando_ertek.ActualAppUser = _context?.AppUsers?.FirstOrDefault(v => v.ID == HttpContext.Session.GetInt32(_felhasznaloId));

            var elmentett_igenyek = HttpContext.Session.GetObject<ServiceSelectViewModel>(_elmentettIgenyek);
            atadando_ertek.Price = elmentett_igenyek.Price;
            atadando_ertek.SelectedVehicle = _context?.Vehicles?.FirstOrDefault(v => v.Id == elmentett_igenyek.SelectedVehicleId);
            atadando_ertek.SelectedServices = elmentett_igenyek;
            return View(atadando_ertek);
        }
        public async Task<IActionResult> SaveToDatabase()
        {
            var elmentett_igenyek = HttpContext.Session.GetObject<ServiceSelectViewModel>(_elmentettIgenyek);
            var order = new Orders() 
            {
                AppUserId = HttpContext.Session.GetInt32(_felhasznaloId).Value,
                Category = elmentett_igenyek.Category,
                Ceramic = elmentett_igenyek.Ceramic,
                Inner = elmentett_igenyek.Inner,
                Outer = elmentett_igenyek.Outer,
                Polish = elmentett_igenyek.Polish,
                Ppf = elmentett_igenyek.Ppf,
                Quality = elmentett_igenyek.Quality,
                VehicleId = elmentett_igenyek.SelectedVehicleId,
                Wax = elmentett_igenyek.Wax,
                Price = elmentett_igenyek.Price,
                //ToDo Progressből is Enum-ot csinálni folyamat modellezés miatt
                Progress = 0,                
                OrderTime = DateTime.Now,
                CompletedTime = DateTime.Now.AddDays(3)
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            ViewData["OrderId"] = $"VH-{DateTime.Now.Year}-{order.Id}";
            return View();
        }
        public IActionResult Megrendelesek()
        {
            var FelhasznaloId = HttpContext.Session.GetInt32(_felhasznaloId);
            var megrendelesek = _context.Orders.ToList();
            if(!User.IsInRole("Admin") && !User.IsInRole("Alkalmazott"))               
                megrendelesek = megrendelesek.Where(rendeles => rendeles.AppUserId == FelhasznaloId).ToList();

            return View(megrendelesek);
        }
        //ToDo Egyszerű vásárló csak a saját megrendelését tudja törölni
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'MavAutoKozmDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Megrendelesek));
        }
        public async Task<IActionResult> DetailsOrder(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            //--VehilceId-ból rendszám--
            //Context-el értük el az adatbázist
            //Vehicles-vel a táblát
            //FirstOrDefault legelső találatot adja vissza (ha nem talál eredmlnyt akkor null)
            //FirstOrDefault-ban egyesével végig megyünk a táblán
            //Ezeknek "n" lesz a neve az egyes elemeknek
            //Order.vehicleId tartalmazta a megrendeléshez csatolt jármű ID-ját
            //Azt kellett összehasonlítani hogy az egyes elemek közül melyiknek az ID-ja egyezik meg ezzel
            //Az egészet egy "jarmu" nevű változóba tettük bele
            //Ennek a változónak a NumberPlate propertiét jelenítjük meg az oldalon a vehicleId helyett
            //ViewData["VehicleNumberPlate"] zsebbe tettük bele a rendszámot
            //A cshtml oldalon @ViewData["VehicleNumberPlate"]-val vettük ki ezt az infót

            var jarmu =_context.Vehicles.FirstOrDefault(n => n.Id == order.VehicleId);
            ViewData["VehicleNumberPlate"] = jarmu?.NumberPlate;

            return View(order);
        }
    }
}