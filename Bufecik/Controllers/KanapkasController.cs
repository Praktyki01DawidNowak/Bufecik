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
    public class KanapkasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KanapkasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kanapkas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Kanapka.Include(k => k.Kategoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Kanapkas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kanapka == null)
            {
                return NotFound();
            }

            var kanapka = await _context.Kanapka
                .Include(k => k.Kategoria)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kanapka == null)
            {
                return NotFound();
            }

            return View(kanapka);
        }

        // GET: Kanapkas/Create
        public IActionResult Create()
        {
            ViewData["KategoriaID"] = new SelectList(_context.Set<Kategoria>(), "ID", "ID");
            return View();
        }

        // POST: Kanapkas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazwa,Cena,Skladniki,Zdjecie,KategoriaID")] Kanapka kanapka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kanapka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriaID"] = new SelectList(_context.Set<Kategoria>(), "ID", "ID", kanapka.KategoriaID);
            return View(kanapka);
        }

        // GET: Kanapkas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kanapka == null)
            {
                return NotFound();
            }

            var kanapka = await _context.Kanapka.FindAsync(id);
            if (kanapka == null)
            {
                return NotFound();
            }
            ViewData["KategoriaID"] = new SelectList(_context.Set<Kategoria>(), "ID", "ID", kanapka.KategoriaID);
            return View(kanapka);
        }

        // POST: Kanapkas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazwa,Cena,Skladniki,Zdjecie,KategoriaID")] Kanapka kanapka)
        {
            if (id != kanapka.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kanapka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KanapkaExists(kanapka.ID))
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
            ViewData["KategoriaID"] = new SelectList(_context.Set<Kategoria>(), "ID", "ID", kanapka.KategoriaID);
            return View(kanapka);
        }

        // GET: Kanapkas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kanapka == null)
            {
                return NotFound();
            }

            var kanapka = await _context.Kanapka
                .Include(k => k.Kategoria)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kanapka == null)
            {
                return NotFound();
            }

            return View(kanapka);
        }

        // POST: Kanapkas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kanapka == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kanapka'  is null.");
            }
            var kanapka = await _context.Kanapka.FindAsync(id);
            if (kanapka != null)
            {
                _context.Kanapka.Remove(kanapka);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KanapkaExists(int id)
        {
          return (_context.Kanapka?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
