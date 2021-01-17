using HotelProject.Data;
using HotelProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        public IActionResult Search(DateTime fromDate, DateTime toDate, int numOfAdults, int numOfKids, int numOfInfants)
        {
            if (fromDate < DateTime.Now || toDate <= fromDate || numOfAdults == 0)
            {
                ViewBag.EnterDetails = "חובה להזין תאריכים תקינים ולבחור לפחות מבוגר אחד בהזמנה";
                return View("Index");
            }
            //Filling in order details
            Order newOrder = new Order()
            {
                FromDate = fromDate,
                ToDate = toDate,
                NumOfAdults = numOfAdults,
                NumOfKids = numOfKids,
                NumOfInfants = numOfInfants,
            };
            HttpContext.Session.SetString("newOrder", JsonConvert.SerializeObject(newOrder));
            IEnumerable<Room> avaliableRooms = new List<Room>();
            Dictionary<int, string> avaliableRoomsTypes = new Dictionary<int, string>();
            List<int> notAvaliableRoomsIds = new List<int>();

            //take all rooms that appear in order table in the period given
            var ordersInSameDates = _context.Order.Where(o => (o.FromDate >= fromDate && o.FromDate < toDate) || (o.ToDate > fromDate && o.FromDate < fromDate)).ToList();
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
            if (notAvaliableRoomsIds.Count > 0)
            {
                //Create a list of available rooms
                avaliableRooms = _context.Room.Where(r => !notAvaliableRoomsIds.Contains(r.Id)).ToList();
                if (avaliableRooms != null && avaliableRooms.Count() > 0)
                {
                    foreach (var avRoom in avaliableRooms)
                    {
                        var allTypes = _context.RoomType.ToList();

                        //var type = allTypes.FirstOrDefault(t => t.Rooms.Contains(avRoom));
                        //avaliableRooms.FirstOrDefault(t => t.Id == avRoom.Id).Type = type;

                        //Create a dictionary of available Typesrooms and count of available rooms in this typeRoom
                        if (avaliableRoomsTypes.ContainsKey(avRoom.Type.Id))
                        {
                            avaliableRoomsTypes[avRoom.Type.Id] += "," + avRoom.Id.ToString();
                        }
                        else
                        {
                            avaliableRoomsTypes.Add(avRoom.Type.Id, avRoom.Id.ToString());
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
                    avaliableRoomsTypes.Add(type.Id, "All");
                }
            }
            if (avaliableRoomsTypes.Count() > 0)
            {
                return View("SearchResults", SearchResultslogic(avaliableRoomsTypes));
            }
            //הודעת שגיאה למשתמש במקרה שאין חדרים פנויים בתאריך שנבחר
            ViewBag.NoRoomFound = "Sorry, there are no avaliable rooms for the dates specified.";
            return View("Index");
        }

        public Dictionary<RoomType, int> SearchResultslogic(Dictionary<int, string> roomTypes)
        {
            Dictionary<int, double> roomTypeTotal = null;
            var orderStr = HttpContext.Session.GetString("newOrder");
            Order newOrder = JsonConvert.DeserializeObject<Order>(orderStr);
            Dictionary<RoomType, int> OrderOptionsResult = new Dictionary<RoomType, int>();
            int numBadsForRoom, kidsForRoom, numOfnights;
            double totalPrice, priceForKid, priceFor1Adult;
            foreach (var roomType in roomTypes)
            {
                var currentType = _context.RoomType.FirstOrDefault(ty => ty.Id == roomType.Key);
                int numOfAvRooms = roomType.Value == "All" ? currentType.Rooms.Count() : roomType.Value.Count(s => s == ',') + 1;
                if (currentType == null) { throw new Exception("Problem with finding rooms."); }
                numBadsForRoom = currentType.ExtraBeds + 2;
                //מספר חדרים לפי 2 מבוגרים בחדר
                int numRooms = newOrder.NumOfAdults % 2 == 0 ? newOrder.NumOfAdults / 2 : (newOrder.NumOfAdults / 2) + 1;
                //במידה ויש ילדים בהזמנה
                if (newOrder.NumOfKids > 0)
                {
                    kidsForRoom = newOrder.NumOfKids % numRooms == 0 ? newOrder.NumOfKids / numRooms : newOrder.NumOfKids / numRooms + 1;
                    if (kidsForRoom <= numBadsForRoom - 2)
                    {
                        if (numRooms <= numOfAvRooms)
                        {
                            priceForKid = currentType.BasicPrice / 3;
                            priceFor1Adult = priceForKid * 2;
                            numOfnights = (newOrder.ToDate - newOrder.FromDate).Days;
                            totalPrice = newOrder.NumOfAdults % 2 == 0 ? numRooms * currentType.BasicPrice : (numRooms - 1) * currentType.BasicPrice + priceFor1Adult;
                            totalPrice += (newOrder.NumOfKids * priceForKid);
                            //הוספת תשלום ביטוח לתינוקות
                            totalPrice += newOrder.NumOfInfants * 40;
                            totalPrice *= numOfnights;
                            currentType.BasicPrice = totalPrice;
                            if (roomTypeTotal == null)
                                roomTypeTotal = new Dictionary<int, double>();
                            roomTypeTotal.Add(currentType.Id, totalPrice);
                            OrderOptionsResult.Add(currentType, numRooms);
                        }
                    }
                    else
                    {
                        kidsForRoom = newOrder.NumOfKids - (numRooms * currentType.ExtraBeds);
                        int numRoomsKids = kidsForRoom % numBadsForRoom == 0 ? kidsForRoom / numBadsForRoom : kidsForRoom / numBadsForRoom + 1;
                        numRooms += numRoomsKids;
                        if (numRooms <= numOfAvRooms)
                        {
                            double pricekidsLastRoom = (currentType.BasicPrice / numBadsForRoom) * (kidsForRoom % numBadsForRoom);
                            priceForKid = currentType.BasicPrice / 3;
                            priceFor1Adult = priceForKid * 2;
                            numOfnights = (newOrder.ToDate - newOrder.FromDate).Days;
                            if (kidsForRoom % numBadsForRoom == 0)
                            {
                                totalPrice = newOrder.NumOfAdults % 2 == 0 ? numRooms * currentType.BasicPrice : (numRooms - 1) * currentType.BasicPrice + priceFor1Adult;

                            }
                            else
                            {
                                totalPrice = pricekidsLastRoom;
                                totalPrice += newOrder.NumOfAdults % 2 == 0 ? (numRooms - 1) * currentType.BasicPrice : (numRooms - 2) * currentType.BasicPrice + priceFor1Adult;
                            }
                            totalPrice += ((newOrder.NumOfKids - kidsForRoom) * priceForKid);
                            //הוספת תשלום ביטוח לתינוקות
                            totalPrice += newOrder.NumOfInfants * 40;
                            totalPrice *= numOfnights;
                            currentType.BasicPrice = totalPrice;
                            if (roomTypeTotal == null)
                                roomTypeTotal = new Dictionary<int, double>();
                            roomTypeTotal.Add(currentType.Id, totalPrice);
                            OrderOptionsResult.Add(currentType, numRooms);
                        }
                    }
                }
                //במידה ואין ילדים בהזמנה
                else
                {
                    if (numRooms <= numOfAvRooms)
                    {
                        priceForKid = currentType.BasicPrice / 3;
                        priceFor1Adult = priceForKid * 2;
                        numOfnights = (newOrder.ToDate - newOrder.FromDate).Days;
                        totalPrice = newOrder.NumOfAdults % 2 == 0 ? numRooms * currentType.BasicPrice : (numRooms - 1) * currentType.BasicPrice + priceFor1Adult;
                        //הוספת תשלום ביטוח לתינוקות
                        totalPrice += newOrder.NumOfInfants * 40;
                        totalPrice *= numOfnights;
                        currentType.BasicPrice = totalPrice;
                        if (roomTypeTotal == null)
                            roomTypeTotal = new Dictionary<int, double>();
                        roomTypeTotal.Add(currentType.Id, totalPrice);
                        OrderOptionsResult.Add(currentType, numRooms);
                    }

                }
            }
            HttpContext.Session.SetString("roomTypeTotal", JsonConvert.SerializeObject(roomTypeTotal));
            HttpContext.Session.SetString("avaliableRoomsTypes", JsonConvert.SerializeObject(roomTypes));
            return OrderOptionsResult;
        }

        public IActionResult RedirectToPayment(int id, int numRooms)
        {
            var orderStr = HttpContext.Session.GetString("newOrder");
            Order newOrder = JsonConvert.DeserializeObject<Order>(orderStr);
            var avaliableRoomsStr = HttpContext.Session.GetString("avaliableRoomsTypes");
            Dictionary<int, string> avaliableRooms = JsonConvert.DeserializeObject<Dictionary<int, string>>(avaliableRoomsStr);
            var roomTypeTotalStr = HttpContext.Session.GetString("roomTypeTotal");
            Dictionary<int, double> roomTypeTotal = JsonConvert.DeserializeObject<Dictionary<int, double>>(roomTypeTotalStr);
            var chosenTypeRoom = _context.RoomType.FirstOrDefault(r => r.Id == id); //OrderOptionsResult.Keys.FirstOrDefault(rt => rt.Id == id);
            if (chosenTypeRoom != null)
            {
                var TypeAvaliableRooms = avaliableRooms.FirstOrDefault(r => chosenTypeRoom.Id == r.Key).Value;
                List<Room> rooms = new List<Room>();
                if (TypeAvaliableRooms != null)
                {
                    HttpContext.Session.SetInt32("numRooms", numRooms);
                    if (TypeAvaliableRooms == "All")
                    {
                        rooms = _context.Room.Where(r => r.Type == chosenTypeRoom).Take(numRooms).ToList();
                    }
                    else
                    {
                        string[] roomsNumbers = TypeAvaliableRooms.Split(',');
                        rooms = _context.Room.Where(r => r.Type == chosenTypeRoom && roomsNumbers.Contains(r.Id.ToString())).Take((int)numRooms).ToList();
                    }
                    string allRoomNumbers = "";
                    for (int i = 0; i < rooms.Count; i++)
                    {
                        if (allRoomNumbers == "") allRoomNumbers += rooms[i].Id.ToString();
                        else allRoomNumbers += "," + rooms[i].Id.ToString();
                        //HttpContext.Session.SetInt32("room" + i, rooms[i].Id);
                        //RoomsOrders ro = new RoomsOrders();
                        //ro.RoomId = rooms[i].Id;
                        //ro.Order = newOrder;
                        //ro.Room = rooms[i];
                        //if (newOrder.Rooms == null)
                        //    newOrder.Rooms = new List<RoomsOrders>();
                        //newOrder.Rooms.Add(ro);
                    }
                    HttpContext.Session.SetString("roomsNumbers", allRoomNumbers);
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
