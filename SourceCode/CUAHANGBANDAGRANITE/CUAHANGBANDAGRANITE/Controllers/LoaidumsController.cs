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
    public class LoaidumsController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public LoaidumsController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Loaidums
        public async Task<IActionResult> Index()
        {
              return _context.Loaida != null ? 
                          View(await _context.Loaida.ToListAsync()) :
                          Problem("Entity set 'QlchdgraniteContext.Loaida'  is null.");
        }

        // GET: Loaidums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loaida == null)
            {
                return NotFound();
            }

            var loaidum = await _context.Loaida
                .FirstOrDefaultAsync(m => m.Idloai == id);
            if (loaidum == null)
            {
                return NotFound();
            }

            return View(loaidum);
        }

		private string TaoMa()
		{

			var macu = _context.Loaida.OrderByDescending(x => x.Maloai).FirstOrDefault(p => p.Maloai.StartsWith($"XX"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Maloai.Substring(2);
				int somamoi = int.Parse(somacu) + 1;
				return $"LD{somamoi:D2}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"LD01";
			}
		}

		// GET: Loaidums/Create
		public IActionResult Create()
        {
			var Daactive = _context.Loaida.Where(x => x.Active == true);
			var Dadisable = _context.Loaida.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;
			return View();
        }

        // POST: Loaidums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idloai,Maloai,Tenloai")] Loaidum loaidum)
        {
            if (ModelState.IsValid)
            {
                loaidum.Active = true;
                _context.Add(loaidum);
                await _context.SaveChangesAsync();
				TempData["CreateSuccess"] = loaidum.Maloai;
				return RedirectToAction(nameof(Create));
            }

			var Daactive = _context.Loaida.Where(x => x.Active == true);
			var Dadisable = _context.Loaida.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			TempData["CreateFail"] = loaidum.Maloai;

			return View(loaidum);
        }

        // GET: Loaidums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loaida == null)
            {
                return NotFound();
            }

			var Daactive = _context.Loaida.Where(x => x.Active == true);
			var Dadisable = _context.Loaida.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			var loaidum = await _context.Loaida.FindAsync(id);
            if (loaidum == null)
            {
                return NotFound();
            }
            return View(loaidum);
        }

        // POST: Loaidums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idloai,Maloai,Tenloai")] Loaidum loaidum)
        {
            if (id != loaidum.Idloai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    loaidum.Active = true;
                    _context.Update(loaidum);
					await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaidumExists(loaidum.Idloai))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["EditSuccess"] = loaidum.Maloai;
				return RedirectToAction(nameof(Create));
            }

			var Daactive = _context.Loaida.Where(x => x.Active == true);
			var Dadisable = _context.Loaida.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["EditFail"] = loaidum.Maloai;
			return View(loaidum);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Loaida
				.FirstOrDefaultAsync(m => m.Idloai == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;

			TempData["DeleteSuccess"] = da.Maloai;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Loaida
				.FirstOrDefaultAsync(m => m.Idloai == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;

			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Maloai;
			return RedirectToAction(nameof(Create));
		}

		// GET: Loaidums/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loaida == null)
            {
                return NotFound();
            }

            var loaidum = await _context.Loaida
                .FirstOrDefaultAsync(m => m.Idloai == id);
            if (loaidum == null)
            {
                return NotFound();
            }

            return View(loaidum);
        }

        // POST: Loaidums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loaida == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Loaida'  is null.");
            }
            var loaidum = await _context.Loaida.FindAsync(id);
            if (loaidum != null)
            {
                _context.Loaida.Remove(loaidum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaidumExists(int id)
        {
          return (_context.Loaida?.Any(e => e.Idloai == id)).GetValueOrDefault();
        }
    }
}
