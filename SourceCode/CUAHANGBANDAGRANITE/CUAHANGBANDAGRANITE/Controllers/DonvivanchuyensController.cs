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
    public class DonvivanchuyensController : Controller
    {
        private readonly QlchdgraniteContext _context;
		private readonly IWebHostEnvironment webHostEnvironment;

		public DonvivanchuyensController(QlchdgraniteContext context, IWebHostEnvironment webHost)
        {
            _context = context;
			webHostEnvironment = webHost;
		}

        // GET: Donvivanchuyens
        public async Task<IActionResult> Index()
        {
            var qlchdgraniteContext = _context.Donvivanchuyens.Include(d => d.TaikhoanidtaikhoanNavigation);
            return View(await qlchdgraniteContext.ToListAsync());
        }

        // GET: Donvivanchuyens/Details/5
        public IActionResult Details(int? id)
        {
			if (id == null || _context.Donvivanchuyens == null)
			{
				return NotFound();
			}

			var donvivanchuyen = _context.Donvivanchuyens.Include(x => x.TaikhoanidtaikhoanNavigation).Where(x => x.Iddvvc == id).FirstOrDefault();
			if (donvivanchuyen == null)
			{
				return NotFound();
			}

			var Daactive = _context.Donvivanchuyens.Where(x => x.Active == true);
			var Dadisable = _context.Donvivanchuyens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			return View(donvivanchuyen);
		}

		private string TaoMa()
		{

			var macu = _context.Donvivanchuyens.OrderByDescending(x => x.Madvvc).FirstOrDefault(p => p.Madvvc.StartsWith($"DVVC"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Madvvc.Substring(4);
				int somamoi = int.Parse(somacu) + 1;
				return $"DVVC{somamoi:D4}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"DVVC0001";
			}
		}

		// GET: Donvivanchuyens/Create
		public IActionResult Create()
        {
			var Daactive = _context.Donvivanchuyens.Where(x => x.Active == true);
			var Dadisable = _context.Donvivanchuyens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			return View();
        }

        // POST: Donvivanchuyens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Donvivanchuyen donvivanchuyen, string Username, String Password, IFormFile FrontImage)
        {
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

				donvivanchuyen.Image = uniqueFileName;
			}

			if (ModelState.IsValid)
			{
				donvivanchuyen.Active = true;
				_context.Add(donvivanchuyen);
				_context.SaveChanges();
				TempData["CreateSuccess"] = donvivanchuyen.Madvvc;
				return RedirectToAction(nameof(Create));
			}

			var Daactive = _context.Donvivanchuyens.Where(x => x.Active == true);
			var Dadisable = _context.Donvivanchuyens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			TempData["CreateFail"] = donvivanchuyen.Madvvc;

			return View(donvivanchuyen);
        }

        // GET: Donvivanchuyens/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Donvivanchuyens == null)
            {
                return NotFound();
            }

			var donvivanchuyen = _context.Donvivanchuyens.Include(x => x.TaikhoanidtaikhoanNavigation).Where(x => x.Iddvvc == id).FirstOrDefault();
			if (donvivanchuyen == null)
            {
                return NotFound();
            }

			var Daactive = _context.Donvivanchuyens.Where(x => x.Active == true);
			var Dadisable = _context.Donvivanchuyens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			return View(donvivanchuyen);
        }

        // POST: Donvivanchuyens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string Username, String Password, IFormFile FrontImage, Donvivanchuyen donvivanchuyen)
        {
            if (id != donvivanchuyen.Iddvvc)
            {
                return NotFound();
            }

			if (FrontImage == null)
			{
				var dvvctheoid = _context.Donvivanchuyens.Find(id);
				if (dvvctheoid != null)
				{
					dvvctheoid.Madvvc = donvivanchuyen.Madvvc;
					dvvctheoid.Tendvvc = donvivanchuyen.Tendvvc;
					dvvctheoid.Sdt = donvivanchuyen.Sdt;
					dvvctheoid.Email = donvivanchuyen.Email;
					dvvctheoid.Masothue = donvivanchuyen.Masothue;
					dvvctheoid.Diachi = donvivanchuyen.Diachi;
					dvvctheoid.Ghichu = donvivanchuyen.Ghichu;
					dvvctheoid.Active = true;

					_context.Update(dvvctheoid);
					_context.SaveChanges();
					TempData["EditSuccess"] = donvivanchuyen.Madvvc;
					return RedirectToAction(nameof(Create));
				}
			}

			if (ModelState.IsValid)
            {
                try
                {
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

						donvivanchuyen.Image = uniqueFileName;
					}
					donvivanchuyen.Active = true;
					_context.Update(donvivanchuyen);
					_context.SaveChanges();
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonvivanchuyenExists(donvivanchuyen.Iddvvc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["EditSuccess"] = donvivanchuyen.Madvvc;
				return RedirectToAction(nameof(Create));
            }
			//ViewData["Taikhoanidtaikhoan"] = new SelectList(_context.Taikhoans, "Idtaikhoan", "Idtaikhoan", donvivanchuyen.Taikhoanidtaikhoan);

			var Daactive = _context.Donvivanchuyens.Where(x => x.Active == true);
			var Dadisable = _context.Donvivanchuyens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["EditFail"] = donvivanchuyen.Madvvc;

			return View(donvivanchuyen);
        }


		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Donvivanchuyens
				.FirstOrDefaultAsync(m => m.Iddvvc == id);
			var tk = await _context.Taikhoans
						.FirstOrDefaultAsync(t => t.Idtaikhoan == da.Taikhoanidtaikhoan);

			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;
			tk.Active = false;
			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Madvvc;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Donvivanchuyens
				.FirstOrDefaultAsync(m => m.Iddvvc == id);
			var tk = await _context.Taikhoans
						.FirstOrDefaultAsync(t => t.Idtaikhoan == da.Taikhoanidtaikhoan);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;
			tk.Active = true;
			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Madvvc;
			return RedirectToAction(nameof(Create));
		}


		// GET: Donvivanchuyens/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Donvivanchuyens == null)
            {
                return NotFound();
            }

            var donvivanchuyen = await _context.Donvivanchuyens
                .Include(d => d.TaikhoanidtaikhoanNavigation)
                .FirstOrDefaultAsync(m => m.Iddvvc == id);
            if (donvivanchuyen == null)
            {
                return NotFound();
            }

            return View(donvivanchuyen);
        }

        // POST: Donvivanchuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Donvivanchuyens == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Donvivanchuyens'  is null.");
            }
            var donvivanchuyen = await _context.Donvivanchuyens.FindAsync(id);
            if (donvivanchuyen != null)
            {
                _context.Donvivanchuyens.Remove(donvivanchuyen);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonvivanchuyenExists(int id)
        {
          return (_context.Donvivanchuyens?.Any(e => e.Iddvvc == id)).GetValueOrDefault();
        }
    }
}
