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
    public class OgrencisController : Controller
    {
        private readonly OkulContext _context;

        public OgrencisController(OkulContext context)
        {
            _context = context;
        }

        // GET: Ogrencis
        public async Task<IActionResult> Index()
        {
            var okulContext = _context.Ogrencis.Include(o => o.OgrBolum);
            return View(await okulContext.ToListAsync());
        }

        // GET: Ogrencis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ogrencis == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrencis
                .Include(o => o.OgrBolum)
                .FirstOrDefaultAsync(m => m.OgrId == id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        // GET: Ogrencis/Create
        public IActionResult Create()
        {
            ViewData["OgrBolumId"] = new SelectList(_context.Bolums, "BolumId", "BolumId");
            return View();
        }

        // POST: Ogrencis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OgrId,OgrNo,OgrAdi,OgrSoyadi,OgrMail,OgrBolumId,Gno")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ogrenci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OgrBolumId"] = new SelectList(_context.Bolums, "BolumId", "BolumId", ogrenci.OgrBolumId);
            return View(ogrenci);
        }

        // GET: Ogrencis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ogrencis == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrencis.FindAsync(id);
            if (ogrenci == null)
            {
                return NotFound();
            }
            ViewData["OgrBolumId"] = new SelectList(_context.Bolums, "BolumId", "BolumId", ogrenci.OgrBolumId);
            return View(ogrenci);
        }

        // POST: Ogrencis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OgrId,OgrNo,OgrAdi,OgrSoyadi,OgrMail,OgrBolumId,Gno")] Ogrenci ogrenci)
        {
            if (id != ogrenci.OgrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ogrenci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgrenciExists(ogrenci.OgrId))
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
            ViewData["OgrBolumId"] = new SelectList(_context.Bolums, "BolumId", "BolumId", ogrenci.OgrBolumId);
            return View(ogrenci);
        }

        // GET: Ogrencis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ogrencis == null)
            {
                return NotFound();
            }

            var ogrenci = await _context.Ogrencis
                .Include(o => o.OgrBolum)
                .FirstOrDefaultAsync(m => m.OgrId == id);
            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        // POST: Ogrencis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ogrencis == null)
            {
                return Problem("Entity set 'OkulContext.Ogrencis'  is null.");
            }
            var ogrenci = await _context.Ogrencis.FindAsync(id);
            if (ogrenci != null)
            {
                _context.Ogrencis.Remove(ogrenci);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OgrenciExists(int id)
        {
          return (_context.Ogrencis?.Any(e => e.OgrId == id)).GetValueOrDefault();
        }
    }
}
