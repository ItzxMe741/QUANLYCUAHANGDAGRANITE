using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUAHANGBANDAGRANITE.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace CUAHANGBANDAGRANITE.Controllers
{
    public class NhanviensController : Controller
    {
        private readonly QlchdgraniteContext _context;
		private readonly IWebHostEnvironment webHostEnvironment;

		public NhanviensController(QlchdgraniteContext context, IWebHostEnvironment webHost)
        {
            _context = context;
			webHostEnvironment = webHost;
		}

        // GET: Nhanviens
        public async Task<IActionResult> Index()
        {
            var qlchdgraniteContext = _context.Nhanviens.Include(n => n.TaikhoanidtaikhoanNavigation);
            return View(await qlchdgraniteContext.ToListAsync());
        }

        // GET: Nhanviens/Details/5
        public IActionResult Details(int? id)
        {
			if (id == null || _context.Nhanviens == null)
			{
				return NotFound();
			}

			var nhanvien = _context.Nhanviens.Include(x => x.TaikhoanidtaikhoanNavigation).Where(x => x.Idnhanvien == id).FirstOrDefault();
			if (nhanvien == null)
			{
				return NotFound();
			}

			var taikhoan = _context.Taikhoans.Where(x => x.Idtaikhoan == nhanvien.Taikhoanidtaikhoan).FirstOrDefault();

			string Username = taikhoan.Tendangnhap;
			string Password = taikhoan.Matkhau;

			ViewData["Username"] = Username;
			ViewData["Password"] = Password;

			var Daactive = _context.Nhanviens.Where(x => x.Active == true);
			var Dadisable = _context.Nhanviens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			return View(nhanvien);
		}

        // GET: Nhanviens/Create
        public IActionResult Create()
        {
            //ViewData["Taikhoanidtaikhoan"] = new SelectList(_context.Taikhoans, "Idtaikhoan", "Idtaikhoan");
			var Daactive = _context.Nhanviens.Where(x => x.Active == true);
			var Dadisable = _context.Nhanviens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			return View();
        }

        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Nhanvien nhanvien,string Username, String Password, IFormFile FrontImage)
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

				nhanvien.Image = uniqueFileName;
			}

			var check = _context.Nhanviens.FirstOrDefault(s => s.Manhanvien == nhanvien.Manhanvien);
			var checktk = _context.Taikhoans.FirstOrDefault(s => s.Tendangnhap == Username);
            if (check == null && checktk == null)
            {
				var taikhoan = new Taikhoan
				{
					Tendangnhap = Username,
					Matkhau = Password,
					Vaitro = "1",
					Active = true,
				};

				_context.Add(taikhoan);
				_context.SaveChanges();
				nhanvien.Active = true;
				nhanvien.Taikhoanidtaikhoan = taikhoan.Idtaikhoan;
				_context.Add(nhanvien);
				_context.SaveChanges();
				TempData["CreateSuccess"] = nhanvien.Manhanvien;
				return RedirectToAction(nameof(Create));
			}

            if(check != null)
            {
				TempData["AlertMessage"] = " This " + nhanvien.Manhanvien + " already exists.";
			} else if (checktk != null)
			{
				TempData["AlertMessagetk"] = " This " + Username + " already exists.";
			}
            else
            {
				TempData["AlertMessagetk"] = " This " + nhanvien.Manhanvien+ "&" + Username + " already exists.";

			}


			var Daactive = _context.Nhanviens.Where(x => x.Active == true);
			var Dadisable = _context.Nhanviens.Where(x => x.Active == false);

			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["CreateFail"] = nhanvien.Manhanvien;

			return View(nhanvien);

		}

        // GET: Nhanviens/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = _context.Nhanviens.Include(x => x.TaikhoanidtaikhoanNavigation).Where(x =>x.Idnhanvien == id).FirstOrDefault();
            if (nhanvien == null)
            {
                return NotFound();
            }

            var taikhoan = _context.Taikhoans.Where(x => x.Idtaikhoan == nhanvien.Taikhoanidtaikhoan).FirstOrDefault();

			string Username = taikhoan.Tendangnhap;
			string Password = taikhoan.Matkhau;

			ViewData["Username"] = Username;
			ViewData["Password"] = Password;

			var Daactive = _context.Nhanviens.Where(x => x.Active == true);
			var Dadisable = _context.Nhanviens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;
			return View(nhanvien);
        }

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string Username, String Password, IFormFile FrontImage, Nhanvien nhanvien)
        {
            if (id != nhanvien.Idnhanvien)
            {
                return NotFound();
            }

			Taikhoan taikhoan = _context.Taikhoans.Find(nhanvien.Taikhoanidtaikhoan);
			if (taikhoan != null)
			{
				taikhoan.Tendangnhap = Username;
				taikhoan.Matkhau = Password;
				taikhoan.Vaitro = "1";
				taikhoan.Active = true;
				_context.Update(taikhoan);
			}

			if (FrontImage == null)
			{
				var nvtheoid = _context.Nhanviens.Find(id);
				if (nvtheoid != null)
				{
					nvtheoid.Manhanvien = nhanvien.Manhanvien;
					nvtheoid.Tennhanvien = nhanvien.Tennhanvien;
					nvtheoid.Sdt = nhanvien.Sdt;
					nvtheoid.Email = nhanvien.Email;
					nvtheoid.Ngaysinh = nhanvien.Ngaysinh;
					nvtheoid.Active = true;

					_context.Update(nvtheoid);
					_context.SaveChanges();
					TempData["EditSuccess"] = nhanvien.Manhanvien;
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

						nhanvien.Image = uniqueFileName;
					}
                    nhanvien.Active= true;
					_context.Update(nhanvien);
					TempData["EditSuccess"] = nhanvien.Manhanvien;
					_context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.Idnhanvien))
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

			var ltaikhoan = _context.Taikhoans.Where(x => x.Idtaikhoan == nhanvien.Taikhoanidtaikhoan).FirstOrDefault();

			string Username1 = ltaikhoan.Tendangnhap;
			string Password1 = ltaikhoan.Matkhau;

			ViewData["Username"] = Username1;
			ViewData["Password"] = Password1;

			var Daactive = _context.Nhanviens.Where(x => x.Active == true);
			var Dadisable = _context.Nhanviens.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			//ViewData["Taikhoanidtaikhoan"] = new SelectList(_context.Taikhoans, "Idtaikhoan", "Idtaikhoan", nhanvien.Taikhoanidtaikhoan);
			TempData["EditFail"] = nhanvien.Manhanvien;

			return View(nhanvien);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Nhanviens
				.FirstOrDefaultAsync(m => m.Idnhanvien == id);
			var tk = await _context.Taikhoans
						.FirstOrDefaultAsync(t => t.Idtaikhoan == da.Taikhoanidtaikhoan);

			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;
			tk.Active = false;
			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Manhanvien;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Nhanviens
				.FirstOrDefaultAsync(m => m.Idnhanvien == id);
			var tk = await _context.Taikhoans
						.FirstOrDefaultAsync(t => t.Idtaikhoan == da.Taikhoanidtaikhoan);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = true;
			tk.Active = true;
			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Manhanvien;
			return RedirectToAction(nameof(Create));
		}


		// GET: Nhanviens/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .Include(n => n.TaikhoanidtaikhoanNavigation)
                .FirstOrDefaultAsync(m => m.Idnhanvien == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nhanviens == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Nhanviens'  is null.");
            }
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien != null)
            {
                _context.Nhanviens.Remove(nhanvien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(int id)
        {
          return (_context.Nhanviens?.Any(e => e.Idnhanvien == id)).GetValueOrDefault();
        }
    }
}
