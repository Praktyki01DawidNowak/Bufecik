using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bufecik.Data;
using Bufecik.Models;

namespace Bufecik.Controllers
{
    public class KlientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KlientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Klients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Klient.Include(k => k.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Klients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Klient == null)
            {
                return NotFound();
            }

            var klient = await _context.Klient
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // GET: Klients/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Klients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Imie,Nazwisko,Telefon,UserID")] Klient klient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", klient.UserID);
            return View(klient);
        }

        // GET: Klients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Klient == null)
            {
                return NotFound();
            }

            var klient = await _context.Klient.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", klient.UserID);
            return View(klient);
        }

        // POST: Klients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Imie,Nazwisko,Telefon,UserID")] Klient klient)
        {
            if (id != klient.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlientExists(klient.ID))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", klient.UserID);
            return View(klient);
        }

        // GET: Klients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Klient == null)
            {
                return NotFound();
            }

            var klient = await _context.Klient
                .Include(k => k.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (klient == null)
            {
                return NotFound();
            }

            return View(klient);
        }

        // POST: Klients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Klient == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Klient'  is null.");
            }
            var klient = await _context.Klient.FindAsync(id);
            if (klient != null)
            {
                _context.Klient.Remove(klient);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlientExists(int id)
        {
          return (_context.Klient?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
