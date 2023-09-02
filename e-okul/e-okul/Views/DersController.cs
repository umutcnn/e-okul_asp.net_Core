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
    public class DersController : Controller
    {
        private readonly OkulContext _context;

        public DersController(OkulContext context)
        {
            _context = context;
        }

        // GET: Ders
        public async Task<IActionResult> Index()
        {
            var okulContext = _context.Ders.Include(d => d.Ogretmen);
            return View(await okulContext.ToListAsync());
        }

        // GET: Ders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ders == null)
            {
                return NotFound();
            }

            var der = await _context.Ders
                .Include(d => d.Ogretmen)
                .FirstOrDefaultAsync(m => m.DersId == id);
            if (der == null)
            {
                return NotFound();
            }

            return View(der);
        }

        // GET: Ders/Create
        public IActionResult Create()
        {
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmen, "OgretmenId", "OgretmenId");
            return View();
        }

        // POST: Ders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DersId,DersAdi,Akts,Kredi,HaftalikSaati,OgretmenId")] Der der)
        {
            if (ModelState.IsValid)
            {
                _context.Add(der);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmen, "OgretmenId", "OgretmenId", der.OgretmenId);
            return View(der);
        }

        // GET: Ders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ders == null)
            {
                return NotFound();
            }

            var der = await _context.Ders.FindAsync(id);
            if (der == null)
            {
                return NotFound();
            }
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmen, "OgretmenId", "OgretmenId", der.OgretmenId);
            return View(der);
        }

        // POST: Ders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DersId,DersAdi,Akts,Kredi,HaftalikSaati,OgretmenId")] Der der)
        {
            if (id != der.DersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(der);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DerExists(der.DersId))
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
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmen, "OgretmenId", "OgretmenId", der.OgretmenId);
            return View(der);
        }

        // GET: Ders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ders == null)
            {
                return NotFound();
            }

            var der = await _context.Ders
                .Include(d => d.Ogretmen)
                .FirstOrDefaultAsync(m => m.DersId == id);
            if (der == null)
            {
                return NotFound();
            }

            return View(der);
        }

        // POST: Ders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ders == null)
            {
                return Problem("Entity set 'OkulContext.Ders'  is null.");
            }
            var der = await _context.Ders.FindAsync(id);
            if (der != null)
            {
                _context.Ders.Remove(der);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DerExists(int id)
        {
          return (_context.Ders?.Any(e => e.DersId == id)).GetValueOrDefault();
        }
    }
}
