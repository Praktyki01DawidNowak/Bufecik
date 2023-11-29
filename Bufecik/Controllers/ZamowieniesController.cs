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
    public class ZamowieniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZamowieniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zamowienies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Zamowienie.Include(z => z.Klient).Include(z => z.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zamowienies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zamowienie == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienie
                .Include(z => z.Klient)
                .Include(z => z.Status)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }

        // GET: Zamowienies/Create
        public IActionResult Create()
        {
            ViewData["KlientID"] = new SelectList(_context.Klient, "ID", "ID");
            ViewData["StatusID"] = new SelectList(_context.Status, "ID", "ID");
            return View();
        }

        // POST: Zamowienies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DataZ,KlientID,StatusID")] Zamowienie zamowienie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zamowienie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlientID"] = new SelectList(_context.Klient, "ID", "ID", zamowienie.KlientID);
            ViewData["StatusID"] = new SelectList(_context.Status, "ID", "ID", zamowienie.StatusID);
            return View(zamowienie);
        }

        // GET: Zamowienies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zamowienie == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienie.FindAsync(id);
            if (zamowienie == null)
            {
                return NotFound();
            }
            ViewData["KlientID"] = new SelectList(_context.Klient, "ID", "ID", zamowienie.KlientID);
            ViewData["StatusID"] = new SelectList(_context.Status, "ID", "ID", zamowienie.StatusID);
            return View(zamowienie);
        }

        // POST: Zamowienies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DataZ,KlientID,StatusID")] Zamowienie zamowienie)
        {
            if (id != zamowienie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zamowienie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZamowienieExists(zamowienie.ID))
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
            ViewData["KlientID"] = new SelectList(_context.Klient, "ID", "ID", zamowienie.KlientID);
            ViewData["StatusID"] = new SelectList(_context.Status, "ID", "ID", zamowienie.StatusID);
            return View(zamowienie);
        }

        // GET: Zamowienies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zamowienie == null)
            {
                return NotFound();
            }

            var zamowienie = await _context.Zamowienie
                .Include(z => z.Klient)
                .Include(z => z.Status)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zamowienie == null)
            {
                return NotFound();
            }

            return View(zamowienie);
        }

        // POST: Zamowienies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zamowienie == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Zamowienie'  is null.");
            }
            var zamowienie = await _context.Zamowienie.FindAsync(id);
            if (zamowienie != null)
            {
                _context.Zamowienie.Remove(zamowienie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZamowienieExists(int id)
        {
          return (_context.Zamowienie?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
