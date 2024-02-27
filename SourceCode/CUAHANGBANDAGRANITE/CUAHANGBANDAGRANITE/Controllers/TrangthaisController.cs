using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUAHANGBANDAGRANITE.Models;

namespace CUAHANGBANDAGRANITE.Controllers
{
    public class TrangthaisController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public TrangthaisController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Trangthais
        public async Task<IActionResult> Index()
        {
              return _context.Trangthais != null ? 
                          View(await _context.Trangthais.ToListAsync()) :
                          Problem("Entity set 'QlchdgraniteContext.Trangthais'  is null.");
        }

        // GET: Trangthais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trangthais == null)
            {
                return NotFound();
            }

            var trangthai = await _context.Trangthais
                .FirstOrDefaultAsync(m => m.Idtt == id);
            if (trangthai == null)
            {
                return NotFound();
            }

            return View(trangthai);
        }

        // GET: Trangthais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trangthais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idtt,Matt,Tentt,Active")] Trangthai trangthai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trangthai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trangthai);
        }

        // GET: Trangthais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trangthais == null)
            {
                return NotFound();
            }

            var trangthai = await _context.Trangthais.FindAsync(id);
            if (trangthai == null)
            {
                return NotFound();
            }
            return View(trangthai);
        }

        // POST: Trangthais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idtt,Matt,Tentt,Active")] Trangthai trangthai)
        {
            if (id != trangthai.Idtt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trangthai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangthaiExists(trangthai.Idtt))
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
            return View(trangthai);
        }

        // GET: Trangthais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trangthais == null)
            {
                return NotFound();
            }

            var trangthai = await _context.Trangthais
                .FirstOrDefaultAsync(m => m.Idtt == id);
            if (trangthai == null)
            {
                return NotFound();
            }

            return View(trangthai);
        }

        // POST: Trangthais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trangthais == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Trangthais'  is null.");
            }
            var trangthai = await _context.Trangthais.FindAsync(id);
            if (trangthai != null)
            {
                _context.Trangthais.Remove(trangthai);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangthaiExists(int id)
        {
          return (_context.Trangthais?.Any(e => e.Idtt == id)).GetValueOrDefault();
        }
    }
}
