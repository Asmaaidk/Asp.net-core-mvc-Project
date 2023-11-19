using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.Linq;

namespace Hotels.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShoppingController(ApplicationDbContext context) 
        {
            _context = context;
        }
        //[HttpPost]
        //public IActionResult Index(string city)
        //{
        //    var findHotels = _context.hotel.Where(x => x.City.Contains(city));
        //    ViewBag.hotels = findHotels;
        //    return View();
        //}

        public IActionResult Invoice(int id)
        {
            Decimal Tax = (15 / 100);
            var room = _context.rooms.SingleOrDefault(x => x.Id == id);
            var invoice = new Invoice()
            {
                IdRoom = room.Id,
                IdHotel = room.IdHotel,
                IdRoomDetailes = room.Id,
                DateInvoice = DateTime.Now.Date,
                Price = room.Price,
                Tax = (15 / 100),
                Discount = 0,
                Net = room.Price * Tax,
                Total = room.Price*1,
                DateFrom = DateTime.Now.Date,
                DateTo= DateTime.Now.Date,
                UserId = 0

            };
            _context.Add(invoice);
            _context.SaveChanges();
            return View(invoice);
        }
        public IActionResult Rooms(int id)
        {
            var rooms = _context.rooms.Where(x => x.IdHotel == id).ToList();
            return View(rooms);
        }
        public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();
            return View(hotel);
        }
    }
}
