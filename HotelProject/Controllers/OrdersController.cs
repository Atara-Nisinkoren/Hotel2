using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelProject.Data;
using HotelProject.Models;
using SQLitePCL;
using Microsoft.AspNetCore.Http;

namespace HotelProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly HotelProjectContext _context;

        public OrdersController(HotelProjectContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpGet]
        public IActionResult Payment(Order order)
        {
            //List<int> rooms = HttpContext.Session.Get("rooms").Select(x => (int)x).ToList();
            RoomsOrders room = new RoomsOrders();
            var namRoomsFromSesion = HttpContext.Session.GetInt32("numRooms");
            for (int i = 0; i < namRoomsFromSesion; i++)
            {
                room.RoomId = Convert.ToInt32(HttpContext.Session.GetInt32("room" + i));
                if(order.Rooms == null)
                    order.Rooms = new List<RoomsOrders>();
                order.Rooms.Add(room);
            }
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(Order order,
                                                 //customer's details
                                                 Client client,
                                                //string ID, string Name, string PhoneNumber, string Address, string Email
                                                //payment's details
                                                //string creditValidity, 
                                                string cardNumber, 
                                                string expiryMonth, string expiryYear,
                                                string cvv, string idCredit, string numberOfPayment
                                                )
        {
            order.Id = 0;
            //check if client did order in the past.
            var existsClient = _context.Client.FirstOrDefault(c => c.ID == client.ID);
            //if is new client, add him to the system.
            if (existsClient == null)
            {
                _context.Client.Add(client);
                _context.SaveChanges();
                existsClient = client;
            }
            bool success = true;//should use payment paramters for perform payment. now ignore it.
            if (success)
            {
                //enter thr order to DB
                order.Client = existsClient;
                _context.Order.Add(order);
                _context.SaveChanges();
                RoomsOrders roomOr = new RoomsOrders();
                var namRoomsFromSesion = HttpContext.Session.GetInt32("numRooms");
                for (int i = 0; i < namRoomsFromSesion; i++)
                {
                    roomOr.RoomId = Convert.ToInt32(HttpContext.Session.GetInt32("room" + i));
                    roomOr.OrderId = order.Id;
                    roomOr.Order = order;
                    roomOr.Room = _context.Room.FirstOrDefault(r => r.Id == roomOr.RoomId);

                    _context.RoomsOrders.Add(roomOr);
                    _context.SaveChanges();
                    //order.Rooms.Add(room);
                }
                ViewBag.OrderDone = "(:הזמנתך התקבלה בהצלחה. מחכים לראות אותך";
               // return RedirectToAction("Index", "Home");
            }
            else {
                ViewBag.OrderFailed = "מצטערים הזמנה נכשלה. אנא צרו קשר עם שירות לקוחות";
            }
            return View(order);
        }


        //public ActionResult PaymentAction(Order order,
        //                                 //customer's details
        //                                 string id, string firstName, string lastName, string mail, string telPhone,
        //                                 //payment's details
        //                                 string ticketNumber, string creditValidity, string cvv, string idCredit, string numberPayment)
        //{
        //    //address? orders?
        //    Client client = new Client { ID = id, Name = firstName + " " + lastName, Email = mail, PhoneNumber = telPhone, };
        //    //check if client did order in the past.
        //    var existsClient = await _context.Client.FindAsync(id);
        //    //if is new client, add him to the system.
        //    if (existsClient == null)
        //    {

        //        _context.Add(client);
        //    }
        //    bool success = true;//should use payment paramters for perform payment. now ignore it.
        //    if (success)
        //    {
        //        //enter thr order to DB
        //        order.Client = client;
        //        _context.Add(order);
        //    }

        //    return View(order);
        //}


        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FromDate,ToDate,NumOfAdults,NumOfKids,NumOfInfants,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FromDate,ToDate,NumOfAdults,NumOfKids,NumOfInfants,TotalPrice")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
