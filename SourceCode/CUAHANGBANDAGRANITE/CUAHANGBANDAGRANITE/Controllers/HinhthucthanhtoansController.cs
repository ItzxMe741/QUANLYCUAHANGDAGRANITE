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
    public class HinhthucthanhtoansController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public HinhthucthanhtoansController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Hinhthucthanhtoans
        public async Task<IActionResult> Index()
        {
            var qlchdgraniteContext = _context.Hinhthucthanhtoans.Include(h => h.NganhangidnganhangNavigation);
            return View(await qlchdgraniteContext.ToListAsync());
        }

        // GET: Hinhthucthanhtoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Hinhthucthanhtoans == null)
			{
				return NotFound();
			}

			var hinhthucthanhtoan = await _context.Hinhthucthanhtoans.FindAsync(id);
			if (hinhthucthanhtoan == null)
			{
				return NotFound();
			}
			//ViewData["Nganhangidnganhang"] = new SelectList(_context.Nganhangs, "Idnganhang", "Idnganhang", hinhthucthanhtoan.Nganhangidnganhang);

			var Daactive = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Nganhangs"] = _context.Nganhangs.Where(x => x.Active == true).ToList();

			return View(hinhthucthanhtoan);
        }

		private string TaoMa()
		{

			var macu = _context.Hinhthucthanhtoans.OrderByDescending(x => x.Mahttt).FirstOrDefault(p => p.Mahttt.StartsWith($"HTTT"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Mahttt.Substring(4);
				int somamoi = int.Parse(somacu) + 1;
				return $"HTTT{somamoi:D3}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"HTTT001";
			}
		}

		// GET: Hinhthucthanhtoans/Create
		public IActionResult Create()
        {
			//ViewData["Nganhangidnganhang"] = new SelectList(_context.Nganhangs, "Idnganhang", "Idnganhang");
			var Daactive = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Nganhangs"] = _context.Nganhangs.Where(x => x.Active == true).ToList();

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			return View();
        }

        // POST: Hinhthucthanhtoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idhttt,Nganhangidnganhang,Mahttt,Tenhttt,Ghichu")] Hinhthucthanhtoan hinhthucthanhtoan)
        {
            if (ModelState.IsValid)
            {
                hinhthucthanhtoan.Active = true;
                _context.Add(hinhthucthanhtoan);
                await _context.SaveChangesAsync();
				TempData["CreateSuccess"] = hinhthucthanhtoan.Mahttt;
				return RedirectToAction(nameof(Create));
            }
			//ViewData["Nganhangidnganhang"] = new SelectList(_context.Nganhangs, "Idnganhang", "Idnganhang", hinhthucthanhtoan.Nganhangidnganhang);
			var Daactive = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Nganhangs"] = _context.Nganhangs.Where(x => x.Active == true).ToList();

			TempData["CreateFail"] = hinhthucthanhtoan.Mahttt;

			return View(hinhthucthanhtoan);
        }

        // GET: Hinhthucthanhtoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hinhthucthanhtoans == null)
            {
                return NotFound();
            }

            var hinhthucthanhtoan = await _context.Hinhthucthanhtoans.FindAsync(id);
            if (hinhthucthanhtoan == null)
            {
                return NotFound();
            }
			//ViewData["Nganhangidnganhang"] = new SelectList(_context.Nganhangs, "Idnganhang", "Idnganhang", hinhthucthanhtoan.Nganhangidnganhang);

			var Daactive = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Nganhangs"] = _context.Nganhangs.Where(x => x.Active == true).ToList();

			return View(hinhthucthanhtoan);
        }

        // POST: Hinhthucthanhtoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idhttt,Nganhangidnganhang,Mahttt,Tenhttt,Ghichu")] Hinhthucthanhtoan hinhthucthanhtoan)
        {
            if (id != hinhthucthanhtoan.Idhttt)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    hinhthucthanhtoan.Active = true;
                    _context.Update(hinhthucthanhtoan);
					TempData["EditSuccess"] = hinhthucthanhtoan.Mahttt;
					await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HinhthucthanhtoanExists(hinhthucthanhtoan.Idhttt))
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
			//ViewData["Nganhangidnganhang"] = new SelectList(_context.Nganhangs, "Idnganhang", "Idnganhang", hinhthucthanhtoan.Nganhangidnganhang);
			var Daactive = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Nganhangs"] = _context.Nganhangs.Where(x => x.Active == true).ToList();

			TempData["EditFail"] = hinhthucthanhtoan.Mahttt;

			return View(hinhthucthanhtoan);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Hinhthucthanhtoans
				.FirstOrDefaultAsync(m => m.Idhttt == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;
			TempData["DeleteSuccess"] = da.Mahttt;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Hinhthucthanhtoans
				.FirstOrDefaultAsync(m => m.Idhttt == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;

			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Mahttt;
			return RedirectToAction(nameof(Create));
		}

		// GET: Hinhthucthanhtoans/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hinhthucthanhtoans == null)
            {
                return NotFound();
            }

            var hinhthucthanhtoan = await _context.Hinhthucthanhtoans
                .Include(h => h.NganhangidnganhangNavigation)
                .FirstOrDefaultAsync(m => m.Idhttt == id);
            if (hinhthucthanhtoan == null)
            {
                return NotFound();
            }

            return View(hinhthucthanhtoan);
        }

        // POST: Hinhthucthanhtoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hinhthucthanhtoans == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Hinhthucthanhtoans'  is null.");
            }
            var hinhthucthanhtoan = await _context.Hinhthucthanhtoans.FindAsync(id);
            if (hinhthucthanhtoan != null)
            {
                _context.Hinhthucthanhtoans.Remove(hinhthucthanhtoan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HinhthucthanhtoanExists(int id)
        {
          return (_context.Hinhthucthanhtoans?.Any(e => e.Idhttt == id)).GetValueOrDefault();
        }
    }
}
