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
    public class KhachhangsController : Controller
    {
        private readonly QlchdgraniteContext _context;
		private readonly IWebHostEnvironment webHostEnvironment;

		public KhachhangsController(QlchdgraniteContext context, IWebHostEnvironment webHost)
        {
            _context = context;
			webHostEnvironment = webHost;
		}

        // GET: Khachhangs
        public async Task<IActionResult> Index()
        {
            var qlchdgraniteContext = _context.Khachhangs.Include(k => k.TaikhoanidtaikhoanNavigation);
            return View(await qlchdgraniteContext.ToListAsync());
        }

        // GET: Khachhangs/Details/5
        public IActionResult Details(int? id)
        {
			if (id == null || _context.Khachhangs == null)
			{
				return NotFound();
			}

			var khachhang = _context.Khachhangs.Include(x => x.TaikhoanidtaikhoanNavigation).Where(x => x.Idkhachhang == id).FirstOrDefault();
			if (khachhang == null)
			{
				return NotFound();
			}

			var Daactive = _context.Khachhangs.Where(x => x.Active == true);
			var Dadisable = _context.Khachhangs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			return View(khachhang);
		}

		private string TaoMa()
		{

			var macu = _context.Khachhangs.OrderByDescending(x => x.Makhachhang).FirstOrDefault(p => p.Makhachhang.StartsWith($"KH"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Makhachhang.Substring(2);
				int somamoi = int.Parse(somacu) + 1;
				return $"KH{somamoi:D4}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"KH0001";
			}
		}

		// GET: Khachhangs/Create
		public IActionResult Create()
        {
            //ViewData["Taikhoanidtaikhoan"] = new SelectList(_context.Taikhoans, "Idtaikhoan", "Idtaikhoan");
			var Daactive = _context.Khachhangs.Where(x => x.Active == true);
			var Dadisable = _context.Khachhangs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			return View();
        }

		// POST: Khachhangs/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Khachhang khachhang, IFormFile FrontImage)
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

				khachhang.Image = uniqueFileName;
			}

			if (ModelState.IsValid)
			{
				khachhang.Active = true;
				_context.Add(khachhang);
				_context.SaveChanges();
				TempData["CreateSuccess"] = khachhang.Makhachhang;
				return RedirectToAction(nameof(Create));
			}
			

			var Daactive = _context.Khachhangs.Where(x => x.Active == true);
			var Dadisable = _context.Khachhangs.Where(x => x.Active == false);

			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			TempData["CreateFail"] = khachhang.Makhachhang;

			return View(khachhang);
		}

        // GET: Khachhangs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

			var khachhang = _context.Khachhangs.Include(x => x.TaikhoanidtaikhoanNavigation).Where(x => x.Idkhachhang == id).FirstOrDefault();
			if (khachhang == null)
            {
                return NotFound();
            }

			var Daactive = _context.Khachhangs.Where(x => x.Active == true);
			var Dadisable = _context.Khachhangs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			return View(khachhang);
        }

        // POST: Khachhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string Username, String Password, IFormFile FrontImage, Khachhang khachhang)
        {
            if (id != khachhang.Idkhachhang)
            {
                return NotFound();
            }


			if (FrontImage == null)
			{
				var khtheoid = _context.Khachhangs.Find(id);
				if (khtheoid != null)
				{
					khtheoid.Makhachhang = khachhang.Makhachhang;
					khtheoid.Tenkhachhang = khachhang.Tenkhachhang;
					khtheoid.Sdt = khachhang.Sdt;
					khtheoid.Email = khachhang.Email;
					khtheoid.Ngaysinh = khachhang.Ngaysinh;
					khtheoid.Masothue = khachhang.Masothue;
					khtheoid.Diachi = khachhang.Diachi;
					khtheoid.Active = true;

					_context.Update(khtheoid);
					_context.SaveChanges();
					TempData["EditSuccess"] = khachhang.Makhachhang;
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

						khachhang.Image = uniqueFileName;
					}
					khachhang.Active = true;
					_context.Update(khachhang);
					_context.SaveChanges();
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachhangExists(khachhang.Idkhachhang))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["EditSuccess"] = khachhang.Makhachhang;
				return RedirectToAction(nameof(Create));
            }

			var Daactive = _context.Khachhangs.Where(x => x.Active == true);
			var Dadisable = _context.Khachhangs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["EditFail"] = khachhang.Makhachhang;

			return View(khachhang);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Khachhangs
				.FirstOrDefaultAsync(m => m.Idkhachhang == id);
			var tk = await _context.Taikhoans
						.FirstOrDefaultAsync(t => t.Idtaikhoan == da.Taikhoanidtaikhoan);

			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;
			tk.Active = false;
			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Makhachhang;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Khachhangs
				.FirstOrDefaultAsync(m => m.Idkhachhang == id);
			var tk = await _context.Taikhoans
						.FirstOrDefaultAsync(t => t.Idtaikhoan == da.Taikhoanidtaikhoan);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;
			tk.Active = true;
			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Makhachhang;
			return RedirectToAction(nameof(Create));
		}



		// GET: Khachhangs/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .Include(k => k.TaikhoanidtaikhoanNavigation)
                .FirstOrDefaultAsync(m => m.Idkhachhang == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }

        // POST: Khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Khachhangs == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Khachhangs'  is null.");
            }
            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang != null)
            {
                _context.Khachhangs.Remove(khachhang);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachhangExists(int id)
        {
          return (_context.Khachhangs?.Any(e => e.Idkhachhang == id)).GetValueOrDefault();
        }
    }
}
