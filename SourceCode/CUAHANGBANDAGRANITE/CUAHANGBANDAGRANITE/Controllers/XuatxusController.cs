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
    public class XuatxusController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public XuatxusController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Xuatxus
        public async Task<IActionResult> Index()
        {
              return _context.Xuatxus != null ? 
                          View(await _context.Xuatxus.ToListAsync()) :
                          Problem("Entity set 'QlchdgraniteContext.Xuatxus'  is null.");
        }

        // GET: Xuatxus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Xuatxus == null)
            {
                return NotFound();
            }

            var xuatxu = await _context.Xuatxus
                .FirstOrDefaultAsync(m => m.Idxuatxu == id);
            if (xuatxu == null)
            {
                return NotFound();
            }

            return View(xuatxu);
        }

		private string TaoMa()
		{

			var macu = _context.Xuatxus.OrderByDescending(x => x.Maxuatxu).FirstOrDefault(p => p.Maxuatxu.StartsWith($"XX"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Maxuatxu.Substring(2);
				int somamoi = int.Parse(somacu) + 1;
				return $"XX{somamoi:D2}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"XX01";
			}
		}


		// GET: Xuatxus/Create
		public IActionResult Create()
        {
			var Daactive = _context.Xuatxus.Where(x => x.Active == true);
			var Dadisable = _context.Xuatxus.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
            ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			return View();
        }

        // POST: Xuatxus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idxuatxu,Maxuatxu,Xuatxu1")] Xuatxu xuatxu)
        {
            if (ModelState.IsValid)
            {
                xuatxu.Active = true;
                _context.Add(xuatxu);
                await _context.SaveChangesAsync();
				TempData["CreateSuccess"] = xuatxu.Maxuatxu;
				return RedirectToAction(nameof(Create));
            }
			var Daactive = _context.Xuatxus.Where(x => x.Active == true);
			var Dadisable = _context.Xuatxus.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
            TempData["CreateFail"] = xuatxu.Maxuatxu;
			return View(xuatxu);
        }

        // GET: Xuatxus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Xuatxus == null)
            {
                return NotFound();
            }
			var Daactive = _context.Xuatxus.Where(x => x.Active == true);
			var Dadisable = _context.Xuatxus.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			var xuatxu = await _context.Xuatxus.FindAsync(id);
            if (xuatxu == null)
            {
                return NotFound();
            }
            return View(xuatxu);
        }



		// POST: Xuatxus/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Xuatxu xuatxu)
        {
            if (id != xuatxu.Idxuatxu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    xuatxu.Active = true;
                    _context.Update(xuatxu);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XuatxuExists(xuatxu.Idxuatxu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["EditSuccess"] = xuatxu.Maxuatxu;
				return RedirectToAction(nameof(Create));
            }
			var Daactive = _context.Xuatxus.Where(x => x.Active == true);
			var Dadisable = _context.Xuatxus.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["EditFail"] = xuatxu.Maxuatxu;
			return View(xuatxu);
        }

        // GET: Xuatxus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Xuatxus == null)
            {
                return NotFound();
            }

            var xuatxu = await _context.Xuatxus
                .FirstOrDefaultAsync(m => m.Idxuatxu == id);
            if (xuatxu == null)
            {
                return NotFound();
            }

            return View(xuatxu);
        }

        // POST: Xuatxus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Xuatxus == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Xuatxus'  is null.");
            }
            var xuatxu = await _context.Xuatxus.FindAsync(id);
            if (xuatxu != null)
            {
                _context.Xuatxus.Remove(xuatxu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Xuatxus
				.FirstOrDefaultAsync(m => m.Idxuatxu == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;
			TempData["DeleteSuccess"] = da.Maxuatxu;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Xuatxus
				.FirstOrDefaultAsync(m => m.Idxuatxu == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;

			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Maxuatxu;
			return RedirectToAction(nameof(Create));
		}


		private bool XuatxuExists(int id)
        {
          return (_context.Xuatxus?.Any(e => e.Idxuatxu == id)).GetValueOrDefault();
        }
    }
}
