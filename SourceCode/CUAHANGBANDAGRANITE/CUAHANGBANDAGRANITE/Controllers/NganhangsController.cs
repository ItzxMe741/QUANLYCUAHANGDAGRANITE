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
    public class NganhangsController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public NganhangsController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Nganhangs
        public async Task<IActionResult> Index()
        {
              return _context.Nganhangs != null ? 
                          View(await _context.Nganhangs.ToListAsync()) :
                          Problem("Entity set 'QlchdgraniteContext.Nganhangs'  is null.");
        }

        // GET: Nganhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Nganhangs == null)
			{
				return NotFound();
			}

			var nganhang = await _context.Nganhangs.FindAsync(id);
			if (nganhang == null)
			{
				return NotFound();
			}

			var Daactive = _context.Nganhangs.Where(x => x.Active == true && x.Idnganhang != 1);
			var Dadisable = _context.Nganhangs.Where(x => x.Active == false && x.Idnganhang != 1);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			return View(nganhang);
        }

		private string TaoMa()
		{

			var macu = _context.Nganhangs.OrderByDescending(x => x.Manganhang).FirstOrDefault(p => p.Manganhang.StartsWith($"NH"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Manganhang.Substring(2);
				int somamoi = int.Parse(somacu) + 1;
				return $"NH{somamoi:D3}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"NH001";
			}
		}

		// GET: Nganhangs/Create
		public IActionResult Create()
        {
			var Daactive = _context.Nganhangs.Where(x => x.Active == true && x.Idnganhang != 1);
			var Dadisable = _context.Nganhangs.Where(x => x.Active == false && x.Idnganhang != 1);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;
			return View();
        }

        // POST: Nganhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idnganhang,Manganhang,Tennganhang,Ghichu")] Nganhang nganhang)
        {
            if (ModelState.IsValid)
            {
				nganhang.Active = true;
				_context.Add(nganhang);
                await _context.SaveChangesAsync();
				TempData["CreateSuccess"] = nganhang.Manganhang;
				return RedirectToAction(nameof(Create));
            }
			var Daactive = _context.Nganhangs.Where(x => x.Active == true && x.Idnganhang != 1);
			var Dadisable = _context.Nganhangs.Where(x => x.Active == false && x.Idnganhang != 1);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			TempData["CreateFail"] = nganhang.Manganhang;

			return View(nganhang);
        }

        // GET: Nganhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nganhangs == null)
            {
                return NotFound();
            }

            var nganhang = await _context.Nganhangs.FindAsync(id);
            if (nganhang == null)
            {
                return NotFound();
            }

			var Daactive = _context.Nganhangs.Where(x => x.Active == true && x.Idnganhang != 1);
			var Dadisable = _context.Nganhangs.Where(x => x.Active == false && x.Idnganhang != 1);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			return View(nganhang);
        }

        // POST: Nganhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idnganhang,Manganhang,Tennganhang,Ghichu")] Nganhang nganhang)
        {
            if (id != nganhang.Idnganhang)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    nganhang.Active = true;
                    _context.Update(nganhang);
					TempData["EditSuccess"] = nganhang.Manganhang;
					await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NganhangExists(nganhang.Idnganhang))
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

			var Daactive = _context.Nganhangs.Where(x => x.Active == true && x.Idnganhang != 1);
			var Dadisable = _context.Nganhangs.Where(x => x.Active == false && x.Idnganhang != 1);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["EditFail"] = nganhang.Manganhang;

			return View(nganhang);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Nganhangs
				.FirstOrDefaultAsync(m => m.Idnganhang == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;

			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Manganhang;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Nganhangs
				.FirstOrDefaultAsync(m => m.Idnganhang == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;

			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Manganhang;
			return RedirectToAction(nameof(Create));
		}

		// GET: Nganhangs/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nganhangs == null)
            {
                return NotFound();
            }

            var nganhang = await _context.Nganhangs
                .FirstOrDefaultAsync(m => m.Idnganhang == id);
            if (nganhang == null)
            {
                return NotFound();
            }

            return View(nganhang);
        }

        // POST: Nganhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nganhangs == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Nganhangs'  is null.");
            }
            var nganhang = await _context.Nganhangs.FindAsync(id);
            if (nganhang != null)
            {
                _context.Nganhangs.Remove(nganhang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NganhangExists(int id)
        {
          return (_context.Nganhangs?.Any(e => e.Idnganhang == id)).GetValueOrDefault();
        }
    }
}
