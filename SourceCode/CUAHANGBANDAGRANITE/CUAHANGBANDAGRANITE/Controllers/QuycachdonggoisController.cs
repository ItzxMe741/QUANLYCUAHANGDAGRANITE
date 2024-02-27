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
    public class QuycachdonggoisController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public QuycachdonggoisController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Quycachdonggois
        public async Task<IActionResult> Index()
        {
              return _context.Quycachdonggois != null ? 
                          View(await _context.Quycachdonggois.ToListAsync()) :
                          Problem("Entity set 'QlchdgraniteContext.Quycachdonggois'  is null.");
        }

        // GET: Quycachdonggois/Details/5
        public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Quycachdonggois == null)
			{
				return NotFound();
			}

			var quycachdonggoi = await _context.Quycachdonggois.FindAsync(id);
			if (quycachdonggoi == null)
			{
				return NotFound();
			}
			var Daactive = _context.Quycachdonggois.Where(x => x.Active == true);
			var Dadisable = _context.Quycachdonggois.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			return View(quycachdonggoi);
        }

		private string TaoMa()
		{

			var macu = _context.Quycachdonggois.OrderByDescending(x => x.Maquycach).FirstOrDefault(p => p.Maquycach.StartsWith($"QCDG"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Maquycach.Substring(4);
				int somamoi = int.Parse(somacu) + 1;
				return $"QCDG{somamoi:D4}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"QCDG0001";
			}
		}



		// GET: Quycachdonggois/Create
		public IActionResult Create()
        {
			var Daactive = _context.Quycachdonggois.Where(x => x.Active == true);
			var Dadisable = _context.Quycachdonggois.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;
			return View();
        }

        // POST: Quycachdonggois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idquycach,Maquycach,Tenquycach,Dientichbemat,Khoiluong,Sovien,Ghichu")] Quycachdonggoi quycachdonggoi)
        {
            if (ModelState.IsValid)
            {
                quycachdonggoi.Active = true;
                _context.Add(quycachdonggoi);
                await _context.SaveChangesAsync();
				TempData["CreateSuccess"] = quycachdonggoi.Maquycach;
				return RedirectToAction(nameof(Create));
            }
			var Daactive = _context.Quycachdonggois.Where(x => x.Active == true);
			var Dadisable = _context.Quycachdonggois.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["CreateFail"] = quycachdonggoi.Maquycach;
			return View(quycachdonggoi);
        }

        // GET: Quycachdonggois/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quycachdonggois == null)
            {
                return NotFound();
            }

            var quycachdonggoi = await _context.Quycachdonggois.FindAsync(id);
            if (quycachdonggoi == null)
            {
                return NotFound();
            }
			var Daactive = _context.Quycachdonggois.Where(x => x.Active == true);
			var Dadisable = _context.Quycachdonggois.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			return View(quycachdonggoi);
        }

        // POST: Quycachdonggois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idquycach,Maquycach,Tenquycach,Dientichbemat,Khoiluong,Sovien,Ghichu")] Quycachdonggoi quycachdonggoi)
        {
            if (id != quycachdonggoi.Idquycach)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    quycachdonggoi.Active = true;
                    _context.Update(quycachdonggoi);
					TempData["EditSuccess"] = quycachdonggoi.Maquycach;
					await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuycachdonggoiExists(quycachdonggoi.Idquycach))
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
			var Daactive = _context.Quycachdonggois.Where(x => x.Active == true);
			var Dadisable = _context.Quycachdonggois.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["EditFail"] = quycachdonggoi.Maquycach;
			return View(quycachdonggoi);
        }

        // GET: Quycachdonggois/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quycachdonggois == null)
            {
                return NotFound();
            }

            var quycachdonggoi = await _context.Quycachdonggois
                .FirstOrDefaultAsync(m => m.Idquycach == id);
            if (quycachdonggoi == null)
            {
                return NotFound();
            }

            return View(quycachdonggoi);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Quycachdonggois
				.FirstOrDefaultAsync(m => m.Idquycach == id);

			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;
			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Maquycach;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Quycachdonggois
				.FirstOrDefaultAsync(m => m.Idquycach == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;
			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Maquycach;
			return RedirectToAction(nameof(Create));
		}

		// POST: Quycachdonggois/Delete/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quycachdonggois == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Quycachdonggois'  is null.");
            }
            var quycachdonggoi = await _context.Quycachdonggois.FindAsync(id);
            if (quycachdonggoi != null)
            {
                _context.Quycachdonggois.Remove(quycachdonggoi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuycachdonggoiExists(int id)
        {
          return (_context.Quycachdonggois?.Any(e => e.Idquycach == id)).GetValueOrDefault();
        }
    }
}
