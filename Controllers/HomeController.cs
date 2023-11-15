using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hotels.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreateHotelRecord(Hotel hotel) { 
            _context.hotel.Add(hotel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        
        }
        public IActionResult Edit(int id)
        {
            var hotelEdit = _context.hotel.SingleOrDefault(x => x.Id == id);
            return View(hotelEdit);
        }

        public IActionResult Update(Hotel hotel)
        {
            _context.hotel.Update(hotel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public IActionResult Delete(int id) {
            var hotelDelete = _context.hotel.SingleOrDefault(x => x.Id == id);
            _context.hotel.Remove(hotelDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");      
        }

        [HttpPost]
        public IActionResult Index(string city)
        {
            var findHotels = _context.hotel.Where(x => x.City.Contains(city));
            if (findHotels ==  null)
            {
                var hotel = _context.hotel.ToList();

                return View(hotel);
            }
            else
            {
                var hotel = findHotels.ToList();
                return View(hotel);
            }

            
        }
        public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();
            
            return View(hotel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}