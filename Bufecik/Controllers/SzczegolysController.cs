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
    public class SzczegolysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SzczegolysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Szczegolys
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Szczegoly.Include(s => s.Kanapka).Include(s => s.Zamowienie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Szczegolys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Szczegoly == null)
            {
                return NotFound();
            }

            var szczegoly = await _context.Szczegoly
                .Include(s => s.Kanapka)
                .Include(s => s.Zamowienie)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (szczegoly == null)
            {
                return NotFound();
            }

            return View(szczegoly);
        }

        // GET: Szczegolys/Create
        public IActionResult Create()
        {
            ViewData["KanapkaID"] = new SelectList(_context.Kanapka, "ID", "ID");
            ViewData["ZamowienieID"] = new SelectList(_context.Set<Zamowienie>(), "ID", "ID");
            return View();
        }

        // POST: Szczegolys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Ilosc,KanapkaID,ZamowienieID")] Szczegoly szczegoly)
        {
            if (ModelState.IsValid)
            {
                _context.Add(szczegoly);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KanapkaID"] = new SelectList(_context.Kanapka, "ID", "ID", szczegoly.KanapkaID);
            ViewData["ZamowienieID"] = new SelectList(_context.Set<Zamowienie>(), "ID", "ID", szczegoly.ZamowienieID);
            return View(szczegoly);
        }

        // GET: Szczegolys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Szczegoly == null)
            {
                return NotFound();
            }

            var szczegoly = await _context.Szczegoly.FindAsync(id);
            if (szczegoly == null)
            {
                return NotFound();
            }
            ViewData["KanapkaID"] = new SelectList(_context.Kanapka, "ID", "ID", szczegoly.KanapkaID);
            ViewData["ZamowienieID"] = new SelectList(_context.Set<Zamowienie>(), "ID", "ID", szczegoly.ZamowienieID);
            return View(szczegoly);
        }

        // POST: Szczegolys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Ilosc,KanapkaID,ZamowienieID")] Szczegoly szczegoly)
        {
            if (id != szczegoly.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(szczegoly);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SzczegolyExists(szczegoly.ID))
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
            ViewData["KanapkaID"] = new SelectList(_context.Kanapka, "ID", "ID", szczegoly.KanapkaID);
            ViewData["ZamowienieID"] = new SelectList(_context.Set<Zamowienie>(), "ID", "ID", szczegoly.ZamowienieID);
            return View(szczegoly);
        }

        // GET: Szczegolys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Szczegoly == null)
            {
                return NotFound();
            }

            var szczegoly = await _context.Szczegoly
                .Include(s => s.Kanapka)
                .Include(s => s.Zamowienie)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (szczegoly == null)
            {
                return NotFound();
            }

            return View(szczegoly);
        }

        // POST: Szczegolys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Szczegoly == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Szczegoly'  is null.");
            }
            var szczegoly = await _context.Szczegoly.FindAsync(id);
            if (szczegoly != null)
            {
                _context.Szczegoly.Remove(szczegoly);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SzczegolyExists(int id)
        {
          return (_context.Szczegoly?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
