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
    public class HarvesController : Controller
    {
        private readonly OkulContext _context;

        public HarvesController(OkulContext context)
        {
            _context = context;
        }

        // GET: Harves
        public async Task<IActionResult> Index()
        {
              return _context.Harves != null ? 
                          View(await _context.Harves.ToListAsync()) :
                          Problem("Entity set 'OkulContext.Harves'  is null.");
        }

        // GET: Harves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Harves == null)
            {
                return NotFound();
            }

            var harf = await _context.Harves
                .FirstOrDefaultAsync(m => m.HarfId == id);
            if (harf == null)
            {
                return NotFound();
            }

            return View(harf);
        }

        // GET: Harves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Harves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HarfId,Harf1,Katsayi,Ortalama")] Harf harf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(harf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(harf);
        }

        // GET: Harves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Harves == null)
            {
                return NotFound();
            }

            var harf = await _context.Harves.FindAsync(id);
            if (harf == null)
            {
                return NotFound();
            }
            return View(harf);
        }

        // POST: Harves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HarfId,Harf1,Katsayi,Ortalama")] Harf harf)
        {
            if (id != harf.HarfId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(harf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HarfExists(harf.HarfId))
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
            return View(harf);
        }

        // GET: Harves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Harves == null)
            {
                return NotFound();
            }

            var harf = await _context.Harves
                .FirstOrDefaultAsync(m => m.HarfId == id);
            if (harf == null)
            {
                return NotFound();
            }

            return View(harf);
        }

        // POST: Harves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Harves == null)
            {
                return Problem("Entity set 'OkulContext.Harves'  is null.");
            }
            var harf = await _context.Harves.FindAsync(id);
            if (harf != null)
            {
                _context.Harves.Remove(harf);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HarfExists(int id)
        {
          return (_context.Harves?.Any(e => e.HarfId == id)).GetValueOrDefault();
        }
    }
}
