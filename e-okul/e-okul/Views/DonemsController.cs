using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_okul.Models;

namespace e_okul.Views
{
    public class DonemsController : Controller
    {
        private readonly OkulContext _context;

        public DonemsController(OkulContext context)
        {
            _context = context;
        }

        // GET: Donems
        public async Task<IActionResult> Index()
        {
            var okulContext = _context.Donems.Include(d => d.Ogr);
            return View(await okulContext.ToListAsync());
        }

        // GET: Donems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Donems == null)
            {
                return NotFound();
            }

            var donem = await _context.Donems
                .Include(d => d.Ogr)
                .FirstOrDefaultAsync(m => m.DonemId == id);
            if (donem == null)
            {
                return NotFound();
            }

            return View(donem);
        }

        // GET: Donems/Create
        public IActionResult Create()
        {
            ViewData["OgrId"] = new SelectList(_context.Ogrencis, "OgrId", "OgrId");
            return View();
        }

        // POST: Donems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonemId,OgrId,DonemSayisi,TopAkts,TopDers,Dno")] Donem donem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OgrId"] = new SelectList(_context.Ogrencis, "OgrId", "OgrId", donem.OgrId);
            return View(donem);
        }

        // GET: Donems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Donems == null)
            {
                return NotFound();
            }

            var donem = await _context.Donems.FindAsync(id);
            if (donem == null)
            {
                return NotFound();
            }
            ViewData["OgrId"] = new SelectList(_context.Ogrencis, "OgrId", "OgrId", donem.OgrId);
            return View(donem);
        }

        // POST: Donems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonemId,OgrId,DonemSayisi,TopAkts,TopDers,Dno")] Donem donem)
        {
            if (id != donem.DonemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonemExists(donem.DonemId))
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
            ViewData["OgrId"] = new SelectList(_context.Ogrencis, "OgrId", "OgrId", donem.OgrId);
            return View(donem);
        }

        // GET: Donems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Donems == null)
            {
                return NotFound();
            }

            var donem = await _context.Donems
                .Include(d => d.Ogr)
                .FirstOrDefaultAsync(m => m.DonemId == id);
            if (donem == null)
            {
                return NotFound();
            }

            return View(donem);
        }

        // POST: Donems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Donems == null)
            {
                return Problem("Entity set 'OkulContext.Donems'  is null.");
            }
            var donem = await _context.Donems.FindAsync(id);
            if (donem != null)
            {
                _context.Donems.Remove(donem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonemExists(int id)
        {
          return (_context.Donems?.Any(e => e.DonemId == id)).GetValueOrDefault();
        }
    }
}
