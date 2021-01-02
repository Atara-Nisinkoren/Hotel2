using HotelProject.Data;
using HotelProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HotelProjectContext _context;

        public HomeController(ILogger<HomeController> logger, HotelProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult events()
        {
            return View();
        }
        public IActionResult Exstaservices()
        {
            return View();
        }

        public IActionResult aboutUs()
        {
            return View();
        }
        public IActionResult Search(DateTime fromDate, DateTime toDate, int numOfAdults, int numOfKids,int numOfInfants)
        {
            //if(fromDate == new DateTime() || toDate == new DateTime() || numOfAdults == 0)
            //{
            //    ViewBag.EnterDetails = "You must specify dates and number of people!";
            //    return View("Index");
            //}
            IEnumerable<Room> avaliableRooms = new List<Room>();
            List<int> notAvaliableRoomsIds = new List<int>();
            //take all rooms that appear in order table in the period given
            var ordersInSameDates =  _context.Order.Where(o => (o.FromDate >= fromDate && o.FromDate < toDate) || (o.ToDate > fromDate && o.FromDate < fromDate));
            if (ordersInSameDates != null && ordersInSameDates.Count() > 0)
            {
                foreach (var order in ordersInSameDates)
                {
                    foreach (var room in order.Rooms)
                    {
                        if (!notAvaliableRoomsIds.Contains(room.RoomId))
                        {
                            notAvaliableRoomsIds.Add(room.RoomId);
                        }
                    }
                }
            }
            if(notAvaliableRoomsIds.Count > 0)
            {
                avaliableRooms = _context.Room.Where(r => !notAvaliableRoomsIds.Contains(r.Id));
            }
            else
            {
                avaliableRooms = _context.Room.ToList();
            }
            if(avaliableRooms.Count() > 0)
            {
                return View("SearchResults",avaliableRooms);
            }
            ViewBag.NoRoomFound = "Sorry, there are no avaliable rooms for the dates specified.";
            return View("Index");
        }
        //[HttpPost]
        //public IActionResult Search()
        //{
        //    RedirectToAction("","",);
        //}
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
