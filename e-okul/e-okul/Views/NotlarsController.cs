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
    public class NotlarsController : Controller
    {
        private readonly OkulContext _context;

        public NotlarsController(OkulContext context)
        {
            _context = context;
        }

        // GET: Notlars
        public async Task<IActionResult> Index()
        {
            var okulContext = _context.Notlars.Include(n => n.Ders).Include(n => n.Harf).Include(n => n.Ogr).Include(n => n.Ogretmen);
            return View(await okulContext.ToListAsync());
        }

        // GET: Notlars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notlars == null)
            {
                return NotFound();
            }

            var notlar = await _context.Notlars
                .Include(n => n.Ders)
                .Include(n => n.Harf)
                .Include(n => n.Ogr)
                .Include(n => n.Ogretmen)
                .FirstOrDefaultAsync(m => m.NotId == id);
            if (notlar == null)
            {
                return NotFound();
            }

            return View(notlar);
        }

        // GET: Notlars/Create
        public IActionResult Create()
        {
            ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId");
            ViewData["HarfId"] = new SelectList(_context.Harves, "HarfId", "HarfId");
            ViewData["OgrId"] = new SelectList(_context.Ogrencis, "OgrId", "OgrId");
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmen, "OgretmenId", "OgretmenId");
            return View();
        }

        // POST: Notlars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NotId,OgrId,DersId,OgretmenId,AraSinav,Final,Ortalama,HarfId")] Notlar notlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId", notlar.DersId);
            ViewData["HarfId"] = new SelectList(_context.Harves, "HarfId", "HarfId", notlar.HarfId);
            ViewData["OgrId"] = new SelectList(_context.Ogrencis, "OgrId", "OgrId", notlar.OgrId);
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmen, "OgretmenId", "OgretmenId", notlar.OgretmenId);
            return View(notlar);
        }

        // GET: Notlars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notlars == null)
            {
                return NotFound();
            }

            var notlar = await _context.Notlars.FindAsync(id);
            if (notlar == null)
            {
                return NotFound();
            }
            ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId", notlar.DersId);
            ViewData["HarfId"] = new SelectList(_context.Harves, "HarfId", "HarfId", notlar.HarfId);
            ViewData["OgrId"] = new SelectList(_context.Ogrencis, "OgrId", "OgrId", notlar.OgrId);
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmen, "OgretmenId", "OgretmenId", notlar.OgretmenId);
            return View(notlar);
        }

        // POST: Notlars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NotId,OgrId,DersId,OgretmenId,AraSinav,Final,Ortalama,HarfId")] Notlar notlar)
        {
            if (id != notlar.NotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotlarExists(notlar.NotId))
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
            ViewData["DersId"] = new SelectList(_context.Ders, "DersId", "DersId", notlar.DersId);
            ViewData["HarfId"] = new SelectList(_context.Harves, "HarfId", "HarfId", notlar.HarfId);
            ViewData["OgrId"] = new SelectList(_context.Ogrencis, "OgrId", "OgrId", notlar.OgrId);
            ViewData["OgretmenId"] = new SelectList(_context.Ogretmen, "OgretmenId", "OgretmenId", notlar.OgretmenId);
            return View(notlar);
        }

        // GET: Notlars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notlars == null)
            {
                return NotFound();
            }

            var notlar = await _context.Notlars
                .Include(n => n.Ders)
                .Include(n => n.Harf)
                .Include(n => n.Ogr)
                .Include(n => n.Ogretmen)
                .FirstOrDefaultAsync(m => m.NotId == id);
            if (notlar == null)
            {
                return NotFound();
            }

            return View(notlar);
        }

        // POST: Notlars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notlars == null)
            {
                return Problem("Entity set 'OkulContext.Notlars'  is null.");
            }
            var notlar = await _context.Notlars.FindAsync(id);
            if (notlar != null)
            {
                _context.Notlars.Remove(notlar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotlarExists(int id)
        {
          return (_context.Notlars?.Any(e => e.NotId == id)).GetValueOrDefault();
        }
    }
}
