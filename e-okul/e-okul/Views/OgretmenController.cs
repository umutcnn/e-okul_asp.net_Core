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
    public class OgretmenController : Controller
    {
        private readonly OkulContext _context;

        public OgretmenController(OkulContext context)
        {
            _context = context;
        }

        // GET: Ogretmen
        public async Task<IActionResult> Index()
        {
              return _context.Ogretmen != null ? 
                          View(await _context.Ogretmen.ToListAsync()) :
                          Problem("Entity set 'OkulContext.Ogretmen'  is null.");
        }

        // GET: Ogretmen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ogretmen == null)
            {
                return NotFound();
            }

            var ogretman = await _context.Ogretmen
                .FirstOrDefaultAsync(m => m.OgretmenId == id);
            if (ogretman == null)
            {
                return NotFound();
            }

            return View(ogretman);
        }

        // GET: Ogretmen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ogretmen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OgretmenId,Adi,Soyadi")] Ogretman ogretman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogretman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ogretman);
        }

        // GET: Ogretmen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ogretmen == null)
            {
                return NotFound();
            }

            var ogretman = await _context.Ogretmen.FindAsync(id);
            if (ogretman == null)
            {
                return NotFound();
            }
            return View(ogretman);
        }

        // POST: Ogretmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OgretmenId,Adi,Soyadi")] Ogretman ogretman)
        {
            if (id != ogretman.OgretmenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogretman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgretmanExists(ogretman.OgretmenId))
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
            return View(ogretman);
        }

        // GET: Ogretmen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ogretmen == null)
            {
                return NotFound();
            }

            var ogretman = await _context.Ogretmen
                .FirstOrDefaultAsync(m => m.OgretmenId == id);
            if (ogretman == null)
            {
                return NotFound();
            }

            return View(ogretman);
        }

        // POST: Ogretmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ogretmen == null)
            {
                return Problem("Entity set 'OkulContext.Ogretmen'  is null.");
            }
            var ogretman = await _context.Ogretmen.FindAsync(id);
            if (ogretman != null)
            {
                _context.Ogretmen.Remove(ogretman);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgretmanExists(int id)
        {
          return (_context.Ogretmen?.Any(e => e.OgretmenId == id)).GetValueOrDefault();
        }
    }
}
