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
    public class KategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategoriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kategorias
        public async Task<IActionResult> Index()
        {
              return _context.Kategoria != null ? 
                          View(await _context.Kategoria.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Kategoria'  is null.");
        }

        // GET: Kategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kategoria == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategoria
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return View(kategoria);
        }

        // GET: Kategorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazwa")] Kategoria kategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategoria);
        }

        // GET: Kategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kategoria == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategoria.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }
            return View(kategoria);
        }

        // POST: Kategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazwa")] Kategoria kategoria)
        {
            if (id != kategoria.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriaExists(kategoria.ID))
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
            return View(kategoria);
        }

        // GET: Kategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kategoria == null)
            {
                return NotFound();
            }

            var kategoria = await _context.Kategoria
                .FirstOrDefaultAsync(m => m.ID == id);
            if (kategoria == null)
            {
                return NotFound();
            }

            return View(kategoria);
        }

        // POST: Kategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kategoria == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kategoria'  is null.");
            }
            var kategoria = await _context.Kategoria.FindAsync(id);
            if (kategoria != null)
            {
                _context.Kategoria.Remove(kategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriaExists(int id)
        {
          return (_context.Kategoria?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
