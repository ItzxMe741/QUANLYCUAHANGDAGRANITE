using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUAHANGBANDAGRANITE.Models;
using Microsoft.AspNetCore.Hosting;

namespace CUAHANGBANDAGRANITE.Controllers
{
    public class DagranitesController : Controller
    {
        private readonly QlchdgraniteContext _context;
		private readonly IWebHostEnvironment webHostEnvironment;

		public DagranitesController(QlchdgraniteContext context, IWebHostEnvironment webHost)
        {
            _context = context;
			webHostEnvironment = webHost;
		}

        // GET: Dagranites
        public async Task<IActionResult> Index()
        {
            var qlchdgraniteContext = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation).Include(d => d.XuatxuidxuatxuNavigation);
            return View(await qlchdgraniteContext.ToListAsync());
        }

        // GET: Dagranites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Dagranites == null)
			{
				return NotFound();
			}

			var dagranite = await _context.Dagranites.FindAsync(id);
			if (dagranite == null)
			{
				return NotFound();
			}

			List<Noidungungdung> ungDungIds = _context.Noidungungdungs.ToList();

			var Daactive = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == false);
			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			ViewBag.ungDungIds = ungDungIds;
			TempData["Loaida"] = _context.Loaida.Where(x => x.Active == true).ToList();
			TempData["Xuatxus"] = _context.Xuatxus.Where(x => x.Active == true).ToList();
			TempData["Ungdungs"] = _context.Ungdungs.Where(x => x.Active == true).ToList();


			return View(dagranite);
		}


		private string TaoMa()
		{

			var macu = _context.Dagranites.OrderByDescending(x => x.Mada).FirstOrDefault(p => p.Mada.StartsWith($"DGRN"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Mada.Substring(4);
				int somamoi = int.Parse(somacu) + 1;
				return $"DGRN{somamoi:D4}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"DGRN0001";
			}
		}

		// GET: Dagranites/Create
		public IActionResult Create(int? Loctheoloai)
        {
			var Daactive = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == true);
			if (Loctheoloai != null)
			{
				Daactive = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
												  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == true && x.Loaidaidloai == Loctheoloai);
			}
			
			var Dadisable = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Loaida"] = _context.Loaida.Where(x => x.Active == true).ToList();
			TempData["Xuatxus"] = _context.Xuatxus.Where(x => x.Active == true).ToList();
			TempData["Ungdungs"] = _context.Ungdungs.Where(x => x.Active == true).ToList();

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			return View();
        }

        // POST: Dagranites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dagranite dagranite, IFormFile FrontImage, String AUngdung)
        {
			String[] ungDungIds = AUngdung.Split(',');

			foreach (var ungDungId in ungDungIds)
			{
				int idUngDung;
				if (int.TryParse(ungDungId, out idUngDung))
				{
					// Tạo một đối tượng Noidungungdung và lưu giá trị Ungdungidungdung
					var noidungungdung = new Noidungungdung
					{
						Ungdungidungdung = idUngDung,
						Active = true,
					};

					// Lưu noidungungdung vào cơ sở dữ liệu
					dagranite.Noidungungdungs.Add(noidungungdung);
				}

			}
			if (FrontImage != null && FrontImage.Length > 0)
			{
				string? uniqueFileName = null;
				// Lưu hình ảnh vào thư mục wwwroot/images
				string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
				uniqueFileName = Guid.NewGuid().ToString() + "-" + FrontImage.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					FrontImage.CopyTo(fileStream);
				}

				dagranite.Image = uniqueFileName;
			}
			if (ModelState.IsValid)
			{
				dagranite.Dientichbemat = (dagranite.Chieudai * dagranite.Chieurong) / 10000;
				dagranite.Active = true;
				_context.Add(dagranite);
				_context.SaveChanges();
				TempData["CreateSuccess"] = dagranite.Mada;
				return RedirectToAction(nameof(Create));
			}

			var Daactive = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == false);
			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Loaida"] = _context.Loaida.Where(x => x.Active == true).ToList();
			TempData["Xuatxus"] = _context.Xuatxus.Where(x => x.Active == true).ToList();

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;
			TempData["CreateFail"] = dagranite.Mada;

			return View(dagranite);
        }



        // GET: Dagranites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dagranites == null)
            {
                return NotFound();
            }

            var dagranite = await _context.Dagranites.FindAsync(id);
            if (dagranite == null)
            {
                return NotFound();
            }

			List<Noidungungdung> ungDungIds = _context.Noidungungdungs.Where(x => x.Dagraniteidda == id).ToList();

			var Daactive = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == false);
			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			ViewBag.ungDungIds = ungDungIds;
			TempData["Loaida"] = _context.Loaida.Where(x => x.Active == true).ToList();
			TempData["Xuatxus"] = _context.Xuatxus.Where(x => x.Active == true).ToList();
			TempData["Ungdungs"] = _context.Ungdungs.Where(x => x.Active == true).ToList();


			return View(dagranite);
        }

        // POST: Dagranites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Dagranite dagranite, IFormFile FrontImage, String AUngdung)
        {
            if (id != dagranite.Idda)
            {
                return NotFound();
            }

			String[] ungDungIds = AUngdung.Split(',');
			var noidungungdungsToDelete = _context.Noidungungdungs
												  .Where(n => n.Dagraniteidda == id)
												  .ToList();

			_context.Noidungungdungs.RemoveRange(noidungungdungsToDelete);

			if (FrontImage == null)
			{
				var datimtheoid = _context.Dagranites.Find(id);
				if (datimtheoid != null)
				{
					datimtheoid.Mada = dagranite.Mada;
					datimtheoid.Loaidaidloai = dagranite.Loaidaidloai;
					datimtheoid.Xuatxuidxuatxu = dagranite.Xuatxuidxuatxu;
					datimtheoid.Tenda = dagranite.Tenda;
					datimtheoid.Tengoikhac = dagranite.Tengoikhac;
					datimtheoid.Dvt = dagranite.Dvt;
					datimtheoid.Dientichbemat = (dagranite.Chieudai * dagranite.Chieurong)/10000;
					datimtheoid.Chieudai = dagranite.Chieudai;
					datimtheoid.Chieurong = dagranite.Chieurong;
					datimtheoid.Ghichu = dagranite.Ghichu;
					datimtheoid.Active = true;

					foreach (var ungDungId in ungDungIds)
					{
						int idUngDung;
						if (int.TryParse(ungDungId, out idUngDung))
						{
							var noidungungdung = new Noidungungdung
							{
								Ungdungidungdung = idUngDung,
								Active = true,
							};

							datimtheoid.Noidungungdungs.Add(noidungungdung);
						}

					}

					_context.Update(datimtheoid);
					_context.SaveChanges();
					TempData["EditSuccess"] = dagranite.Mada;
					return RedirectToAction(nameof(Create));
				}
			}

				if (ModelState.IsValid)
            {
                try
                {
					foreach (var ungDungId in ungDungIds)
					{
						int idUngDung;
						if (int.TryParse(ungDungId, out idUngDung))
						{
							// Tạo một đối tượng Noidungungdung và lưu giá trị Ungdungidungdung
							var noidungungdung = new Noidungungdung
							{
								Ungdungidungdung = idUngDung,
								Active = true,
							};

							// Lưu noidungungdung vào cơ sở dữ liệu
							dagranite.Noidungungdungs.Add(noidungungdung);
						}

					}

					if (FrontImage != null && FrontImage.Length > 0)
					{
						string uniqueFileName = null;
						// Lưu hình ảnh vào thư mục wwwroot/images
						string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img");
						uniqueFileName = Guid.NewGuid().ToString() + "-" + FrontImage.FileName;
						string filePath = Path.Combine(uploadsFolder, uniqueFileName);
						using (var fileStream = new FileStream(filePath, FileMode.Create))
						{
							FrontImage.CopyTo(fileStream);
						}

						dagranite.Image = uniqueFileName;
					}
					dagranite.Dientichbemat = (dagranite.Chieudai * dagranite.Chieurong)/10000;
					dagranite.Active= true;
					_context.Update(dagranite);
                    _context.SaveChanges();
					return RedirectToAction(nameof(Create));
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!DagraniteExists(dagranite.Idda))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
			var Daactive = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == true);
			var Dadisable = _context.Dagranites.Include(d => d.LoaidaidloaiNavigation)
											  .Include(d => d.XuatxuidxuatxuNavigation).Where(x => x.Active == false);
			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			TempData["Loaida"] = _context.Loaida.Where(x => x.Active == true).ToList();
			TempData["Xuatxus"] = _context.Xuatxus.Where(x => x.Active == true).ToList();
			TempData["EditFail"] = dagranite.Mada;
			return View(dagranite);
        }

        // GET: Dagranites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dagranites == null)
            {
                return NotFound();
            }

            var dagranite = await _context.Dagranites
                .Include(d => d.LoaidaidloaiNavigation)
                .Include(d => d.XuatxuidxuatxuNavigation)
                .FirstOrDefaultAsync(m => m.Idda == id);
            if (dagranite == null)
            {
                return NotFound();
            }

            return View(dagranite);
        }

        // POST: Dagranites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dagranites == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Dagranites'  is null.");
            }
            var dagranite = await _context.Dagranites.FindAsync(id);
            if (dagranite != null)
            {
                _context.Dagranites.Remove(dagranite);
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

			var da = await _context.Dagranites
				.FirstOrDefaultAsync(m => m.Idda == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;

			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Mada;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Dagranites
				.FirstOrDefaultAsync(m => m.Idda == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;

			await _context.SaveChangesAsync();

			TempData["AlertReturnMessage"] = da.Mada;

			return RedirectToAction(nameof(Create));
		}



		private bool DagraniteExists(int id)
        {
          return (_context.Dagranites?.Any(e => e.Idda == id)).GetValueOrDefault();
        }
    }
}
