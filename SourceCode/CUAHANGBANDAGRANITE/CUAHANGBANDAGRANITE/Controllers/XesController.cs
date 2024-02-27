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
    public class XesController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public XesController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Xes
        public async Task<IActionResult> Index()
        {
              return _context.Xes != null ? 
                          View(await _context.Xes.ToListAsync()) :
                          Problem("Entity set 'QlchdgraniteContext.Xes'  is null.");
        }

		private string TaoMa()
		{

			var macu = _context.Xes.OrderByDescending(x => x.Maxe).FirstOrDefault(p => p.Maxe.StartsWith($"XE"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Maxe.Substring(2);
				int somamoi = int.Parse(somacu) + 1;
				return $"XE{somamoi:D3}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"XE001";
			}
		}


		// GET: Xes/Details/5
		public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Xes == null)
			{
				return NotFound();
			}

			var xe = await _context.Xes.FindAsync(id);
			if (xe == null)
			{
				return NotFound();
			}
			var Daactive = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["Donvivanchuyens"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();

			return View(xe);
        }

        // GET: Xes/Create
        public IActionResult Create()
        {
			var Daactive = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			TempData["Donvivanchuyens"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();

			return View();
        }

        // POST: Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idxe,Donvivanchuyeniddvvc,Maxe,Tenxe,Trongtai,Sokhoi,Ghichu")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                xe.Active = true;
                _context.Add(xe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
			var Daactive = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Donvivanchuyens"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			return View(xe);
        }

        // GET: Xes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
			var Daactive = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["Donvivanchuyens"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();

			return View(xe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idxe,Donvivanchuyeniddvvc,Maxe,Tenxe,Trongtai,Sokhoi,Ghichu")] Xe xe)
        {
            if (id != xe.Idxe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    xe.Active = true;
                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.Idxe))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Create));
            }
			var Daactive = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Xes.Include(x => x.DonvivanchuyeniddvvcNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["Donvivanchuyens"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();

			return View(xe);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Xes
				.FirstOrDefaultAsync(m => m.Idxe == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Xes
				.FirstOrDefaultAsync(m => m.Idxe == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Create));
		}

		// GET: Xes/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .FirstOrDefaultAsync(m => m.Idxe == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Xes == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Xes'  is null.");
            }
            var xe = await _context.Xes.FindAsync(id);
            if (xe != null)
            {
                _context.Xes.Remove(xe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
          return (_context.Xes?.Any(e => e.Idxe == id)).GetValueOrDefault();
        }
    }
}
