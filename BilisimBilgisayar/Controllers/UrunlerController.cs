using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BilisimBilgisayar.Data;
using BilisimBilgisayar.Models;

namespace BilisimBilgisayar.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrunlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Urunler
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Urun.Include(u => u.kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Urunler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun
                .Include(u => u.kategori)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Urunler/Create
        public IActionResult Create()
        {
            ViewData["kategoriID"] = new SelectList(_context.Kategori, "ID", "KategoriAdi");
            return View();
        }

        // POST: Urunler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UrunAdi,UrunResimDosyasi,UrunTeknikBilgileri,UrunFiyat,kategoriID")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["kategoriID"] = new SelectList(_context.Kategori, "ID", "KategoriAdi", urun.kategoriID);
            return View(urun);
        }

        // GET: Urunler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["kategoriID"] = new SelectList(_context.Kategori, "ID", "KategoriAdi", urun.kategoriID);
            return View(urun);
        }

        // POST: Urunler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UrunAdi,UrunResimDosyasi,UrunTeknikBilgileri,UrunFiyat,kategoriID")] Urun urun)
        {
            if (id != urun.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.ID))
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
            ViewData["kategoriID"] = new SelectList(_context.Kategori, "ID", "KategoriAdi", urun.kategoriID);
            return View(urun);
        }

        // GET: Urunler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urun
                .Include(u => u.kategori)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var urun = await _context.Urun.FindAsync(id);
            _context.Urun.Remove(urun);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
            return _context.Urun.Any(e => e.ID == id);
        }
    }
}
