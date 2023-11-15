using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
		private readonly ApplicationDbContext _context; //object from framework
		public DashboardController(ApplicationDbContext context) //constructor
		{
			_context = context;
		}
		//send Email 
		//oarwspfoyyfnrmtg
		public async Task<string> SendEMail()
		{
			var msg = new MimeMessage();
			msg.From.Add(new MailboxAddress("Test Massage", "asmaa.idk@gmail.com"));
			msg.To.Add(MailboxAddress.Parse("assoma12@gmail.com"));
			msg.Subject = "Test Email from asp.net";
			msg.Body = new TextPart("plain")
			{
				Text = "Welcome"
			};

			using (var client = new SmtpClient())
			{
				try
				{
					client.Connect("smtp.gmail.com", 587);
					client.Authenticate("asmaa.idk@gmail.com", "axxzrqqvnbilczjs");
					await client.SendAsync(msg);
					client.Disconnect(true);
				}
				catch (Exception ex)
				{
					return ex.Message.ToString();
				}
				
			}
            return "ok";
        }
        
        [HttpPost]
		public IActionResult CreateNewHotel(Hotel hotel)
        {
			if(ModelState.IsValid)
			{
				_context.hotel.Add(hotel);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			var hotels = _context.hotel.ToList();
			return View("Index",hotels);
		}

		public IActionResult Delete(int id)
		{
			var hotelDelete = _context.hotel.SingleOrDefault(x => x.Id == id);
			_context.hotel.Remove(hotelDelete);
			_context.SaveChanges();
			TempData["del"] = "ok";
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
		public IActionResult Rooms()
		{
			var hotel = _context.hotel.ToList();
			ViewBag.Hotel = hotel;
			//ViewBag.currentuser = Request.Cookies["Username"];
			ViewBag.currentuser = HttpContext.Session.GetString("username");
			var room = _context.rooms.ToList();
			return View(room);
		}
		public IActionResult CreateNewRoom(Rooms room)
		{
			_context.rooms.Add(room);
			_context.SaveChanges();
			return RedirectToAction("Rooms");
		}
		public IActionResult DeleteRoom(int id)
		{
			var roomDelete = _context.rooms.SingleOrDefault(x => x.Id == id);
			_context.rooms.Remove(roomDelete);
			_context.SaveChanges();
			TempData["del"] = "ok";
			return RedirectToAction("Rooms");
		}
		public IActionResult EditRoom(int id)
		{
			var roomEdit = _context.rooms.SingleOrDefault(x => x.Id == id);
			return View(roomEdit);
		}

		public IActionResult UpdateRoom(Rooms room)
		{
			_context.rooms.Update(room);
			_context.SaveChanges();
			return RedirectToAction("Rooms");
		}
		public IActionResult RoomDetailes()
		{
			var hotel = _context.hotel.ToList();
			ViewBag.Hotel = hotel;
			var room = _context.rooms.ToList();
			ViewBag.room = room;
			ViewBag.currentuser = HttpContext.Session.GetString("username");
			var roomdetailes = _context.roomDetails.ToList();
			return View(roomdetailes);
		}
		public IActionResult DeleteDetailes(int id)
		{
			var detailsDelete = _context.roomDetails.SingleOrDefault(x => x.Id == id);
			_context.roomDetails.Remove(detailsDelete);
			_context.SaveChanges();
			TempData["del"] = "ok";
			return RedirectToAction("RoomDetailes");
		}
		public IActionResult EditDetailes(int id)
		{
			var detailsEdit = _context.roomDetails.SingleOrDefault(x => x.Id == id);
			return View(detailsEdit);
		}

		public IActionResult UpdateDetailes(RoomDetails details)
		{
			_context.roomDetails.Update(details);
			_context.SaveChanges();
			return RedirectToAction("RoomDetailes");
		}
		
		public IActionResult AddDetailes(RoomDetails details)
		{
			_context.roomDetails.Add(details);
			_context.SaveChanges();
			return RedirectToAction("RoomDetailes");
		}

        

        [Authorize]
		public IActionResult Index()
        {
			var currentuser = HttpContext.User.Identity.Name;
			ViewBag.currentuser = currentuser;

			//cookies
			//CookieOptions option = new CookieOptions();
			//option.Expires = DateTime.Now.AddMinutes(20);
			//Response.Cookies.Append("Username", currentuser,option);

			//session
			HttpContext.Session.SetString("username", currentuser);
            var hotel = _context.hotel.ToList();

                return View(hotel);
        }
    }
}
