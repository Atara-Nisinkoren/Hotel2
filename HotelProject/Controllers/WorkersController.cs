using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelProject.Data;
using HotelProject.Models;
using Microsoft.AspNetCore.Http;

namespace HotelProject.Controllers
{
    public class WorkersController : Controller
    {
        private readonly HotelProjectContext _context;

        public WorkersController(HotelProjectContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Logout()
        {

            HttpContext.Session.Remove("Name");
            {
                return RedirectToAction("Index" , "Home");
            }
            return View(await _context.Worker.ToListAsync());
        }
        // GET: Workers
        public async Task<IActionResult> Index()
        {


            //if (HttpContext.Session.GetString("Name") == null)
            //{
            //    return RedirectToAction(nameof(Login));
            //}
            return View(await _context.Worker.ToListAsync());
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // GET: Workers/Create
        public IActionResult Login()
        {
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,WorkerId,Name,PhoneNumber,Email,WorkerType")] Worker worker)
        {
            var q = from a in _context.Worker
                    where worker.Name == a.Name &&
                          worker.WorkerId == a.WorkerId
                    select a;

            if (q.Count() > 0)
            {
                HttpContext.Session.SetString("Name", q.First().Name);
                return RedirectToAction("Create", "RoomTypes");
                return RedirectToAction("Delete", "RoomTypes");
                return RedirectToAction("Edit", "RoomTypes");
                return RedirectToAction("Create", "Rooms");
                return RedirectToAction("Delete", "Rooms");
                return RedirectToAction("Edit", "RoomTypes");
                //return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Error"] = "Worker does not exist!";
            }
            return View(worker);
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WorkerId,Name,PhoneNumber,Email,WorkerType")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(worker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(worker);
        }
        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WorkerId,Name,PhoneNumber,Email,WorkerType")] Worker worker)
        {
            if (id != worker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(worker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(worker.Id))
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
            return View(worker);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker
                .FirstOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worker = await _context.Worker.FindAsync(id);
            _context.Worker.Remove(worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(int id)
        {
            return _context.Worker.Any(e => e.Id == id);
        }
    }
}
