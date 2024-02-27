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
    public class LoaithungsController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public LoaithungsController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Loaithungs
        public async Task<IActionResult> Index()
        {
            var qlchdgraniteContext = _context.Loaithungs.Include(l => l.DagraniteiddaNavigation).Include(l => l.QuycachidquycachNavigation);
            return View(await qlchdgraniteContext.ToListAsync());
        }

		public IActionResult Baocaotonkho()
		{

			var hangton = _context.Loaithungs.Where(x => x.Active == true).Sum(s => s.Sothungcon);
			ViewData["hangton"] = hangton;

			var soluongban = _context.Chitietxuats.Where(x => x.Active == true).Sum(s => s.Sothunggiaokh);
			ViewData["soluongban"] = soluongban;

			var Daactive = _context.Loaithungs
						   .Include(x => x.QuycachidquycachNavigation)
						   .Include(x => x.DagraniteiddaNavigation)
						   .Where(x => x.Active == true && x.Sothungcon != 0).ToList();
			var Dadisable = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;

			return View();
		}

		public IActionResult Baocaodoanhthu()
		{

			var soluongmua = _context.Chitietnhaps.Where(x => x.Active == true).Sum(s => s.Sothung);
			ViewData["soluongmua"] = soluongmua;

			var soluongban = _context.Chitietxuats.Where(x => x.Active == true).Sum(s => s.Sothunggiaokh);
			ViewData["soluongban"] = soluongban;

			var tongtienmuavao = _context.Chitietnhaps.Where(x => x.Active == true).Sum(x => (x.Dongianhap ?? 0) * (x.Sothung ?? 0));
			ViewData["tongtienmuavao"] = tongtienmuavao;

			var tongtienbanra = _context.Chitietxuats.Where(x => x.Active == true).Sum(x => (x.Dongiaxuat ?? 0) * (x.Sothunggiaokh ?? 0));
			ViewData["tongtienbanra"] = tongtienbanra;

			var Daactive = _context.Loaithungs
										.Include(x => x.QuycachidquycachNavigation)
										.Include(x => x.DagraniteiddaNavigation)
										.Include(x => x.Chitietnhaps)
										.Include(x => x.Chitietxuats)
										.Where(x => x.Active == true) // Thêm điều kiện lọc nếu cần
										.ToList();

			return View(Daactive);
		}

		// GET: Loaithungs/Details/5
		public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Loaithungs == null)
			{
				return NotFound();
			}

			var loaithung = await _context.Loaithungs.FindAsync(id);
			if (loaithung == null)
			{
				return NotFound();
			}
			//ViewData["Dagraniteidda"] = new SelectList(_context.Dagranites, "Idda", "Idda", loaithung.Dagraniteidda);
			//ViewData["Quycachidquycach"] = new SelectList(_context.Quycachdonggois, "Idquycach", "Idquycach", loaithung.Quycachidquycach);

			var Daactive = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Quycachdonggois"] = _context.Quycachdonggois.Where(x => x.Active == true).ToList();
			TempData["Dagranites"] = _context.Dagranites.Where(x => x.Active == true).ToList();

			return View(loaithung);
        }

		private string TaoMa()
		{

			var macu = _context.Loaithungs.OrderByDescending(x => x.Maloaithung).FirstOrDefault(p => p.Maloaithung.StartsWith($"QCD"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Maloaithung.Substring(3);
				int somamoi = int.Parse(somacu) + 1;
				return $"QCD{somamoi:D4}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"QCD0001";
			}
		}

		// GET: Loaithungs/Create
		public IActionResult Create()
        {
			//ViewData["Dagraniteidda"] = new SelectList(_context.Dagranites, "Idda", "Idda");
			//ViewData["Quycachidquycach"] = new SelectList(_context.Quycachdonggois, "Idquycach", "Idquycach");

			var Daactive = _context.Loaithungs
                                   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Quycachdonggois"] = _context.Quycachdonggois.Where(x => x.Active == true).ToList();
			TempData["Dagranites"] = _context.Dagranites.Where(x => x.Active == true).ToList();

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			return View();
        }

        // POST: Loaithungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idloaithung,Dagraniteidda,Quycachidquycach,Maloaithung,Tenloaithung,Dongiaban,Ghichu")] Loaithung loaithung)
        {
            var dagranite = _context.Dagranites.Where(x => x.Active == true && x.Idda == loaithung.Dagraniteidda).FirstOrDefault();
			var quycach = _context.Quycachdonggois.Where(x => x.Active == true && x.Idquycach == loaithung.Quycachidquycach).FirstOrDefault();

			if (ModelState.IsValid)
            {
                loaithung.Tenloaithung = dagranite.Tenda +" - "+ quycach.Tenquycach;
                loaithung.Sothungcon = 0;
                loaithung.Active = true;
                _context.Add(loaithung);
                await _context.SaveChangesAsync();
				TempData["CreateSuccess"] = loaithung.Maloaithung;
				return RedirectToAction(nameof(Create));
            }

			var Daactive = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Quycachdonggois"] = _context.Quycachdonggois.Where(x => x.Active == true).ToList();
			TempData["Dagranites"] = _context.Dagranites.Where(x => x.Active == true).ToList();

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			TempData["CreateFail"] = loaithung.Maloaithung;

			return View(loaithung);
        }

        // GET: Loaithungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loaithungs == null)
            {
                return NotFound();
            }

            var loaithung = await _context.Loaithungs.FindAsync(id);
            if (loaithung == null)
            {
                return NotFound();
            }
			//ViewData["Dagraniteidda"] = new SelectList(_context.Dagranites, "Idda", "Idda", loaithung.Dagraniteidda);
			//ViewData["Quycachidquycach"] = new SelectList(_context.Quycachdonggois, "Idquycach", "Idquycach", loaithung.Quycachidquycach);

			var Daactive = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Quycachdonggois"] = _context.Quycachdonggois.Where(x => x.Active == true).ToList();
			TempData["Dagranites"] = _context.Dagranites.Where(x => x.Active == true).ToList();

			return View(loaithung);
        }

        // POST: Loaithungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idloaithung,Dagraniteidda,Quycachidquycach,Maloaithung,Tenloaithung,Dongiaban,Ghichu")] Loaithung loaithung)
        {
            if (id != loaithung.Idloaithung)
            {
                return NotFound();
            }

			var dagranite = _context.Dagranites.Where(x => x.Active == true && x.Idda == loaithung.Dagraniteidda).FirstOrDefault();
			var quycach = _context.Quycachdonggois.Where(x => x.Active == true && x.Idquycach == loaithung.Quycachidquycach).FirstOrDefault();

			if (ModelState.IsValid)
            {
				try
				{
					if (loaithung.Sothungcon == null)
					{
						loaithung.Sothungcon = 0;
					}
					loaithung.Tenloaithung = dagranite.Tenda + " - " + quycach.Tenquycach;
					loaithung.Active = true;
					_context.Update(loaithung);
					TempData["EditSuccess"] = loaithung.Maloaithung;
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!LoaithungExists(loaithung.Idloaithung))
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
			//ViewData["Dagraniteidda"] = new SelectList(_context.Dagranites, "Idda", "Idda", loaithung.Dagraniteidda);
			//ViewData["Quycachidquycach"] = new SelectList(_context.Quycachdonggois, "Idquycach", "Idquycach", loaithung.Quycachidquycach);

			var Daactive = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Loaithungs
								   .Include(x => x.QuycachidquycachNavigation)
								   .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Quycachdonggois"] = _context.Quycachdonggois.Where(x => x.Active == true).ToList();
			TempData["Dagranites"] = _context.Dagranites.Where(x => x.Active == true).ToList();

			TempData["EditFail"] = loaithung.Maloaithung;

			return View(loaithung);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Loaithungs
				.FirstOrDefaultAsync(m => m.Idloaithung == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;

			TempData["DeleteSuccess"] = da.Maloaithung;
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Loaithungs
				.FirstOrDefaultAsync(m => m.Idloaithung == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;

			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Maloaithung;
			return RedirectToAction(nameof(Create));
		}

		// GET: Loaithungs/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loaithungs == null)
            {
                return NotFound();
            }

            var loaithung = await _context.Loaithungs
                .Include(l => l.DagraniteiddaNavigation)
                .Include(l => l.QuycachidquycachNavigation)
                .FirstOrDefaultAsync(m => m.Idloaithung == id);
            if (loaithung == null)
            {
                return NotFound();
            }

            return View(loaithung);
        }

        // POST: Loaithungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loaithungs == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Loaithungs'  is null.");
            }
            var loaithung = await _context.Loaithungs.FindAsync(id);
            if (loaithung != null)
            {
                _context.Loaithungs.Remove(loaithung);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaithungExists(int id)
        {
          return (_context.Loaithungs?.Any(e => e.Idloaithung == id)).GetValueOrDefault();
        }
    }
}
