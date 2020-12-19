using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelProject.Data;
using HotelProject.Models;

namespace HotelProject.Controllers
{
    public class RoomsOrdersController : Controller
    {
        private readonly HotelProjectContext _context;

        public RoomsOrdersController(HotelProjectContext context)
        {
            _context = context;
        }

        // GET: RoomsOrders
        public async Task<IActionResult> Index()
        {
            var hotelProjectContext = _context.RoomsOrders.Include(r => r.Order).Include(r => r.Room);
            return View(await hotelProjectContext.ToListAsync());
        }

        // GET: RoomsOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomsOrders = await _context.RoomsOrders
                .Include(r => r.Order)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (roomsOrders == null)
            {
                return NotFound();
            }

            return View(roomsOrders);
        }

        // GET: RoomsOrders/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "ClientID");
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id");
            return View();
        }

        // POST: RoomsOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,OrderId")] RoomsOrders roomsOrders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomsOrders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "ClientID", roomsOrders.OrderId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", roomsOrders.RoomId);
            return View(roomsOrders);
        }

        // GET: RoomsOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomsOrders = await _context.RoomsOrders.FindAsync(id);
            if (roomsOrders == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "ClientID", roomsOrders.OrderId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", roomsOrders.RoomId);
            return View(roomsOrders);
        }

        // POST: RoomsOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,OrderId")] RoomsOrders roomsOrders)
        {
            if (id != roomsOrders.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomsOrders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsOrdersExists(roomsOrders.RoomId))
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
            ViewData["OrderId"] = new SelectList(_context.Order, "Id", "ClientID", roomsOrders.OrderId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Id", roomsOrders.RoomId);
            return View(roomsOrders);
        }

        // GET: RoomsOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomsOrders = await _context.RoomsOrders
                .Include(r => r.Order)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (roomsOrders == null)
            {
                return NotFound();
            }

            return View(roomsOrders);
        }

        // POST: RoomsOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomsOrders = await _context.RoomsOrders.FindAsync(id);
            _context.RoomsOrders.Remove(roomsOrders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomsOrdersExists(int id)
        {
            return _context.RoomsOrders.Any(e => e.RoomId == id);
        }
    }
}
