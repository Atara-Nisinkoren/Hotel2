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
        private static IEnumerable<Room> avaliableRooms;
        private static int numRooms;
        private static Dictionary<int, double> roomTypeTotal;

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
            //Filling in order details
            newOrder = new Order()
            {
                FromDate = fromDate,
                ToDate = toDate,
                NumOfAdults = numOfAdults,
                NumOfKids = numOfKids,
                NumOfInfants= numOfInfants,
            };
            avaliableRooms = new List<Room>();
            Dictionary<RoomType, int> avaliableRoomsTypes = new Dictionary<RoomType, int>();
            List<int> notAvaliableRoomsIds = new List<int>();

            //take all rooms that appear in order table in the period given
            var ordersInSameDates =  _context.Order.Where(o => (o.FromDate >= fromDate && o.FromDate < toDate) || (o.ToDate > fromDate && o.FromDate < fromDate)).ToList();
            if (ordersInSameDates != null && ordersInSameDates.Count() > 0)
            {
                //Create a list of unavailable rooms
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
                //Create a list of available rooms
                avaliableRooms = _context.Room.Where(r => !notAvaliableRoomsIds.Contains(r.Id)).ToList();
                if(avaliableRooms != null && avaliableRooms.Count() > 0)
                {
                    foreach (var avRoom in avaliableRooms)
                    {
                        var allTypes = _context.RoomType.ToList();

                        //var type = allTypes.FirstOrDefault(t => t.Rooms.Contains(avRoom));
                        //avaliableRooms.FirstOrDefault(t => t.Id == avRoom.Id).Type = type;

                        //Create a dictionary of available Typesrooms and count of available rooms in this typeRoom
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
                else
                {
                    //הודעת שגיאה למשתמש במקרה שאין חדרים פנויים בתאריך שנבחר
                    ViewBag.NoRoomFound = "Sorry, there are no avaliable rooms for the dates specified.";
                    ViewData["Error2"] = "אין חדרים פנויים בתאריכים הנבחרים. נא לבחור תאריכים אחרים";
                    return View("Index");
                }
               
            }
            else
            {
                //במידה וכל החדרים פנויים בתאריכים שנבחרו
                var allTypes = _context.RoomType.ToList();
                avaliableRooms = _context.Room.ToList();
                foreach (var type in allTypes)
                {
                    avaliableRoomsTypes.Add(type, type.Rooms.Count);
                }
            }
            if (avaliableRoomsTypes.Count() > 0)
            {
                
                avaliableRoomsTypes.OrderByDescending(t => t.Key.ExtraBeds);
                return View("SearchResults", SearchResultslogic(avaliableRoomsTypes));
            }
            //הודעת שגיאה למשתמש במקרה שאין חדרים פנויים בתאריך שנבחר
            ViewBag.NoRoomFound = "Sorry, there are no avaliable rooms for the dates specified.";
            return View("Index");
        }

        public Dictionary<RoomType, int> SearchResultslogic(Dictionary<RoomType, int> roomTypes)
        {
            Dictionary<RoomType, int> OrderOptionsResult = new Dictionary<RoomType, int>();
            int numBadsForRoom, kidsForRoom, numOfnights;
            double totalPrice, priceForKid, priceFor1Adult;
            //int numOfPeople = numOfAdults + numOfKids;
            foreach (var roomType in roomTypes)
            {
                numBadsForRoom = roomType.Key.ExtraBeds + 2;
                //מספר חדרים לפי 2 מבוגרים בחדר
                numRooms = newOrder.NumOfAdults % 2 == 0 ? newOrder.NumOfAdults / 2 : (newOrder.NumOfAdults / 2) + 1;
                //במידה ויש ילדים בהזמנה
                if (newOrder.NumOfKids > 0)
                {
                    kidsForRoom = newOrder.NumOfKids % numRooms == 0 ? newOrder.NumOfKids / numRooms : newOrder.NumOfKids / numRooms + 1;
                    if (kidsForRoom <= numBadsForRoom-2)
                    {
                        if (numRooms <= roomType.Value)
                        {
                            priceForKid = roomType.Key.BasicPrice / 3;
                            priceFor1Adult = priceForKid * 2;
                            numOfnights = (newOrder.ToDate - newOrder.FromDate).Days;
                            totalPrice = newOrder.NumOfAdults % 2 == 0 ? numRooms * roomType.Key.BasicPrice : (numRooms-1) * roomType.Key.BasicPrice + priceFor1Adult;
                            totalPrice += (newOrder.NumOfKids * priceForKid);
                            //הוספת תשלום ביטוח לתינוקות
                            totalPrice += newOrder.NumOfInfants * 40;
                            totalPrice *= numOfnights;
                            roomType.Key.BasicPrice = totalPrice;
                            if (roomTypeTotal == null)
                                roomTypeTotal = new Dictionary<int, double>();
                            roomTypeTotal.Add(roomType.Key.Id, totalPrice);
                            OrderOptionsResult.Add(roomType.Key, numRooms);
                        }
                    }
                    else
                    {
                        kidsForRoom = newOrder.NumOfKids - (numRooms * roomType.Key.ExtraBeds);
                        int numRoomsKids = kidsForRoom % numBadsForRoom == 0 ? kidsForRoom / numBadsForRoom : kidsForRoom / numBadsForRoom + 1;
                        numRooms += numRoomsKids;
                        if (numRooms <= roomType.Value)
                        {
                            double pricekidsLastRoom = (roomType.Key.BasicPrice / numBadsForRoom) * (kidsForRoom % numBadsForRoom);
                            priceForKid = roomType.Key.BasicPrice / 3;
                            priceFor1Adult = priceForKid * 2;
                            numOfnights = (newOrder.ToDate - newOrder.FromDate).Days;
                            if (kidsForRoom % numBadsForRoom == 0)
                            {
                                totalPrice = newOrder.NumOfAdults % 2 == 0 ? numRooms * roomType.Key.BasicPrice : (numRooms - 1) * roomType.Key.BasicPrice + priceFor1Adult;

                            }
                            else
                            {
                                totalPrice = pricekidsLastRoom;
                                totalPrice += newOrder.NumOfAdults % 2 == 0 ? (numRooms - 1) * roomType.Key.BasicPrice : (numRooms - 2) * roomType.Key.BasicPrice + priceFor1Adult;
                            }
                            totalPrice += ((newOrder.NumOfKids - kidsForRoom) * priceForKid);
                            //הוספת תשלום ביטוח לתינוקות
                            totalPrice += newOrder.NumOfInfants * 40;
                            totalPrice *= numOfnights;
                            roomType.Key.BasicPrice = totalPrice;
                            if (roomTypeTotal == null)
                                roomTypeTotal = new Dictionary<int, double>();
                            roomTypeTotal.Add(roomType.Key.Id, totalPrice);
                            OrderOptionsResult.Add(roomType.Key, numRooms);
                        }
                    }
                }
                //במידה ואין ילדים בהזמנה
                else
                {
                    if (numRooms <= roomType.Value)
                    {
                        priceForKid = roomType.Key.BasicPrice / 3;
                        priceFor1Adult = priceForKid * 2;
                        numOfnights = (newOrder.ToDate - newOrder.FromDate).Days;
                        totalPrice = newOrder.NumOfAdults % 2 == 0 ? numRooms * roomType.Key.BasicPrice : (numRooms - 1) * roomType.Key.BasicPrice + priceFor1Adult;
                        //הוספת תשלום ביטוח לתינוקות
                        totalPrice += newOrder.NumOfInfants * 40; 
                        totalPrice *= numOfnights;
                        roomType.Key.BasicPrice = totalPrice;
                        if (roomTypeTotal == null)
                            roomTypeTotal = new Dictionary<int, double>();
                            roomTypeTotal.Add(roomType.Key.Id, totalPrice);
                        OrderOptionsResult.Add(roomType.Key, numRooms);
                    }

                }
            }
            return OrderOptionsResult;
        }

        public IActionResult RedirectToPayment(int id, Order order)
        {
            var chosenTypeRoom = _context.RoomType.FirstOrDefault(r => r.Id == id); //OrderOptionsResult.Keys.FirstOrDefault(rt => rt.Id == id);
            if (chosenTypeRoom != null)
            {
                var TypeAvaliableRooms = avaliableRooms.Where(r => chosenTypeRoom.Id == r.Type.Id).ToList();

                if (TypeAvaliableRooms != null)
                {
                    HttpContext.Session.SetInt32("numRooms", numRooms);
                    for (int i = 0; i < numRooms; i++)
                    {
                        HttpContext.Session.SetInt32("room"+i, TypeAvaliableRooms[i].Id);
                        //RoomsOrders ro = new RoomsOrders();
                        //newOrder.Id = 999999;
                        //ro.OrderId = newOrder.Id;
                        //ro.RoomId = TypeAvaliableRooms[i].Id;
                        //ro.Order = newOrder;
                        //ro.Room = TypeAvaliableRooms[i];
                        //if (newOrder.Rooms == null)
                        //    newOrder.Rooms = new List<RoomsOrders>();
                        //newOrder.Rooms.Add(ro);
                    }
                    newOrder.TotalPrice = roomTypeTotal[chosenTypeRoom.Id];
                    return RedirectToAction("Payment", "Orders", newOrder);
                }
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
