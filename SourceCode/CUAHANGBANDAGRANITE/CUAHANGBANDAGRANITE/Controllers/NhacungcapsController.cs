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
    public class NhacungcapsController : Controller
    {
        private readonly QlchdgraniteContext _context;
		private readonly IWebHostEnvironment webHostEnvironment;


		public NhacungcapsController(QlchdgraniteContext context, IWebHostEnvironment webHost)
        {
            _context = context;
			webHostEnvironment = webHost;
		}

        // GET: Nhacungcaps
        public async Task<IActionResult> Index()
        {
            var qlchdgraniteContext = _context.Nhacungcaps.Include(n => n.TaikhoanidtaikhoanNavigation);
            return View(await qlchdgraniteContext.ToListAsync());
        }

        // GET: Nhacungcaps/Details/5
        public IActionResult Details(int? id)
        {
			if (id == null || _context.Nhacungcaps == null)
			{
				return NotFound();
			}

			var nhacungcap = _context.Nhacungcaps.Include(x => x.TaikhoanidtaikhoanNavigation).Where(x => x.Idncc == id).FirstOrDefault();
			if (nhacungcap == null)
			{
				return NotFound();
			}

			var Daactive = _context.Nhacungcaps.Where(x => x.Active == true);
			var Dadisable = _context.Nhacungcaps.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			return View(nhacungcap);
        }

		private string TaoMa()
		{

			var macu = _context.Nhacungcaps.OrderByDescending(x => x.Mancc).FirstOrDefault(p => p.Mancc.StartsWith($"NCC"));

			if (macu != null)
			{
				// Tách lấy số phiếu hiện tại
				string somacu = macu.Mancc.Substring(3);
				int somamoi = int.Parse(somacu) + 1;
				return $"NCC{somamoi:D4}";
			}
			else
			{
				// Nếu không có phiếu cùng ngày, bắt đầu từ 1
				return $"NCC0001";
			}
		}

		// GET: Nhacungcaps/Create
		public IActionResult Create()
        {
			//ViewData["Taikhoanidtaikhoan"] = new SelectList(_context.Taikhoans, "Idtaikhoan", "Idtaikhoan");
			var Daactive = _context.Nhacungcaps.Where(x => x.Active == true);
			var Dadisable = _context.Nhacungcaps.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;
			return View();
        }

        // POST: Nhacungcaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Nhacungcap nhacungcap, string Username, String Password, IFormFile FrontImage)
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

				nhacungcap.Image = uniqueFileName;
			}

			if (ModelState.IsValid)
			{

				nhacungcap.Active = true;
				_context.Add(nhacungcap);
				_context.SaveChanges();
				TempData["CreateSuccess"] = nhacungcap.Mancc;
				return RedirectToAction(nameof(Create));
			}

			var Daactive = _context.Nhacungcaps.Where(x => x.Active == true);
			var Dadisable = _context.Nhacungcaps.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			string mamoi = TaoMa();
			ViewData["idtd"] = mamoi;

			TempData["CreateFail"] = nhacungcap.Mancc;

			return View(nhacungcap);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Nhacungcaps
				.FirstOrDefaultAsync(m => m.Idncc == id);
			var tk = await _context.Taikhoans
						.FirstOrDefaultAsync(t => t.Idtaikhoan == da.Taikhoanidtaikhoan);

			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;
			tk.Active = false;
			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Mancc;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Nhacungcaps
				.FirstOrDefaultAsync(m => m.Idncc == id);
			var tk = await _context.Taikhoans
						.FirstOrDefaultAsync(t => t.Idtaikhoan == da.Taikhoanidtaikhoan);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;
			tk.Active = true;
			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Mancc;
			return RedirectToAction(nameof(Create));
		}


		// GET: Nhacungcaps/Edit/5
		public IActionResult Edit(int? id)
        {
            if (id == null || _context.Nhacungcaps == null)
            {
                return NotFound();
            }

			var nhacungcap = _context.Nhacungcaps.Include(x => x.TaikhoanidtaikhoanNavigation).Where(x => x.Idncc == id).FirstOrDefault();
			if (nhacungcap == null)
            {
                return NotFound();
            }

			var Daactive = _context.Nhacungcaps.Where(x => x.Active == true);
			var Dadisable = _context.Nhacungcaps.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;


			return View(nhacungcap);
        }

        // POST: Nhacungcaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string Username, String Password, IFormFile FrontImage, Nhacungcap nhacungcap)
        {
            if (id != nhacungcap.Idncc)
            {
                return NotFound();
            }

			if (FrontImage == null)
			{
				var ncctheoid = _context.Nhacungcaps.Find(id);
				if (ncctheoid != null)
				{
					ncctheoid.Mancc = nhacungcap.Mancc;
					ncctheoid.Tenncc = nhacungcap.Tenncc;
					ncctheoid.Sdt = nhacungcap.Sdt;
					ncctheoid.Email = nhacungcap.Email;
					ncctheoid.Masothue = nhacungcap.Masothue;
					ncctheoid.Diachi = nhacungcap.Diachi;
					ncctheoid.Ghichu = nhacungcap.Ghichu;
					ncctheoid.Active = true;

					_context.Update(ncctheoid);
					_context.SaveChanges();
					TempData["EditSuccess"] = nhacungcap.Mancc;
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

						nhacungcap.Image = uniqueFileName;
					}
					nhacungcap.Active = true;
					_context.Update(nhacungcap);
					_context.SaveChanges();
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhacungcapExists(nhacungcap.Idncc))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["EditSuccess"] = nhacungcap.Mancc;
				return RedirectToAction(nameof(Create));
            }

			var ltaikhoan = _context.Taikhoans.Where(x => x.Idtaikhoan == nhacungcap.Taikhoanidtaikhoan).FirstOrDefault();

			string Username1 = ltaikhoan.Tendangnhap;
			string Password1 = ltaikhoan.Matkhau;

			ViewData["Username"] = Username1;
			ViewData["Password"] = Password1;

			var Daactive = _context.Nhacungcaps.Where(x => x.Active == true);
			var Dadisable = _context.Nhacungcaps.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["EditFail"] = nhacungcap.Mancc;

			return View(nhacungcap);
        }

        // GET: Nhacungcaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nhacungcaps == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcaps
                .Include(n => n.TaikhoanidtaikhoanNavigation)
                .FirstOrDefaultAsync(m => m.Idncc == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // POST: Nhacungcaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nhacungcaps == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Nhacungcaps'  is null.");
            }
            var nhacungcap = await _context.Nhacungcaps.FindAsync(id);
            if (nhacungcap != null)
            {
                _context.Nhacungcaps.Remove(nhacungcap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhacungcapExists(int id)
        {
          return (_context.Nhacungcaps?.Any(e => e.Idncc == id)).GetValueOrDefault();
        }
    }
}
