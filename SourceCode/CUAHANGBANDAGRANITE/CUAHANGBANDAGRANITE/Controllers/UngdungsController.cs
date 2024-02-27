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
    public class UngdungsController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public UngdungsController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Ungdungs
        public async Task<IActionResult> Index()
        {
              return _context.Ungdungs != null ? 
                          View(await _context.Ungdungs.ToListAsync()) :
                          Problem("Entity set 'QlchdgraniteContext.Ungdungs'  is null.");
        }

        // GET: Ungdungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ungdungs == null)
            {
                return NotFound();
            }

            var ungdung = await _context.Ungdungs
                .FirstOrDefaultAsync(m => m.Idungdung == id);
            if (ungdung == null)
            {
                return NotFound();
            }

            return View(ungdung);
        }

		private string TaoMa()
		{

			var macu = _context.Ungdungs.OrderByDescending(x => x.Maungdung).FirstOrDefault(p => p.Maungdung.StartsWith($"UD"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Maungdung.Substring(2);
				int somamoi = int.Parse(somacu) + 1;
				return $"UD{somamoi:D2}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"UD01";
			}
		}

		// GET: Ungdungs/Create
		public IActionResult Create()
        {
			var Daactive = _context.Ungdungs.Where(x => x.Active == true);
			var Dadisable = _context.Ungdungs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;
			return View();
        }

        // POST: Ungdungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idungdung,Maungdung,Tenungdung")] Ungdung ungdung)
        {
            if (ModelState.IsValid)
            {
                ungdung.Active = true;
                _context.Add(ungdung);
                await _context.SaveChangesAsync();
				TempData["CreateSuccess"] = ungdung.Maungdung;
				return RedirectToAction(nameof(Index));
            }

			var Daactive = _context.Ungdungs.Where(x => x.Active == true);
			var Dadisable = _context.Ungdungs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;
			TempData["CreateFail"] = ungdung.Maungdung;
			return View(ungdung);
        }

        // GET: Ungdungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ungdungs == null)
            {
                return NotFound();
            }

			var Daactive = _context.Ungdungs.Where(x => x.Active == true);
			var Dadisable = _context.Ungdungs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			var ungdung = await _context.Ungdungs.FindAsync(id);
            if (ungdung == null)
            {
                return NotFound();
            }
            return View(ungdung);
        }

        // POST: Ungdungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idungdung,Maungdung,Tenungdung")] Ungdung ungdung)
        {
            if (id != ungdung.Idungdung)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ungdung.Active = true;
                    _context.Update(ungdung);
					TempData["EditSuccess"] = ungdung.Maungdung;
					await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UngdungExists(ungdung.Idungdung))
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
			TempData["EditFail"] = ungdung.Maungdung;
			return View(ungdung);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Ungdungs
				.FirstOrDefaultAsync(m => m.Idungdung == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;

			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Maungdung;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Ungdungs
				.FirstOrDefaultAsync(m => m.Idungdung == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;

			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Maungdung;
			return RedirectToAction(nameof(Create));
		}

		// GET: Ungdungs/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ungdungs == null)
            {
                return NotFound();
            }

            var ungdung = await _context.Ungdungs
                .FirstOrDefaultAsync(m => m.Idungdung == id);
            if (ungdung == null)
            {
                return NotFound();
            }

            return View(ungdung);
        }

        // POST: Ungdungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ungdungs == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Ungdungs'  is null.");
            }
            var ungdung = await _context.Ungdungs.FindAsync(id);
            if (ungdung != null)
            {
                _context.Ungdungs.Remove(ungdung);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UngdungExists(int id)
        {
          return (_context.Ungdungs?.Any(e => e.Idungdung == id)).GetValueOrDefault();
        }
    }
}
