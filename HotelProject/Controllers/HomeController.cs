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
        private static Order newOrder;

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
            int numOfPeople = numOfAdults + numOfKids;
            newOrder = new Order()
            {
                FromDate = fromDate,
                ToDate = toDate,
                NumOfAdults = numOfAdults,
                NumOfKids = numOfKids,
                NumOfInfants= numOfInfants,
            };
            IEnumerable<Room> avaliableRooms = new List<Room>();
            Dictionary<RoomType, int> avaliableRoomsTypes = new Dictionary<RoomType, int>();
            List<int> notAvaliableRoomsIds = new List<int>();
            //take all rooms that appear in order table in the period given
            var ordersInSameDates =  _context.Order.Where(o => (o.FromDate >= fromDate && o.FromDate < toDate) || (o.ToDate > fromDate && o.FromDate < fromDate)).ToList();
            if (ordersInSameDates != null && ordersInSameDates.Count() > 0)
            {
                foreach (var order in ordersInSameDates)
                {
                    var matchedRooms = _context.RoomsOrders.Where(o => o.OrderId == order.Id).ToList();
                    foreach (var room in matchedRooms)
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
                avaliableRooms = _context.Room.Where(r => !notAvaliableRoomsIds.Contains(r.Id)).ToList();
                if(avaliableRooms != null && avaliableRooms.Count() > 0)
                {
                    foreach (var avRoom in avaliableRooms)
                    {
                       // var type = _context.RoomType.FirstOrDefault(t => t.Id == avRoom.Type.Id);
                        if (avaliableRoomsTypes.ContainsKey(avRoom.Type))
                        {
                            avaliableRoomsTypes[avRoom.Type] +=1;
                        }
                        else
                        {
                            avaliableRoomsTypes.Add(avRoom.Type, 1);
                        }
                    }
                }
               
            }
            else
            {
                var allTypes = _context.RoomType.ToList();
                foreach (var type in allTypes)
                {
                    avaliableRoomsTypes.Add(type, type.Rooms.Count);
                }
            }
            if (avaliableRoomsTypes.Count() > 0)
            {
                avaliableRoomsTypes.OrderByDescending(t => t.Key.ExtraBeds);
                return View("SearchResults", avaliableRoomsTypes);
            }
            ViewBag.NoRoomFound = "Sorry, there are no avaliable rooms for the dates specified.";
            return View("Index");
        }

        public IActionResult RedirectToPayment(int id,Order order)
        {
            var chosenTypeRoom = _context.RoomType.FirstOrDefault(r => r.Id == id);
            if(chosenTypeRoom != null)
            {
                newOrder.TotalPrice = chosenTypeRoom.BasicPrice;
                return RedirectToAction("Payment", "Orders", newOrder);
            }
            return View("");
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
