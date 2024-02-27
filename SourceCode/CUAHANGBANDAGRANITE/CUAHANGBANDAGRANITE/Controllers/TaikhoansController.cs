using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUAHANGBANDAGRANITE.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CUAHANGBANDAGRANITE.Controllers
{
    public class TaikhoansController : Controller
    {
        private readonly QlchdgraniteContext _context;

		private readonly ILogger<TaikhoansController> _logger;
		public TaikhoansController(QlchdgraniteContext context, ILogger<TaikhoansController> logger)
		{
			_context = context;
			_logger = logger;
		}

        // GET: Taikhoans
        public async Task<IActionResult> Index()
        {
              return _context.Taikhoans != null ? 
                          View(await _context.Taikhoans.ToListAsync()) :
                          Problem("Entity set 'QlchdgraniteContext.Taikhoans'  is null.");
        }

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			if (HttpContext.User.Identity.Name == null)
				return View("Login");
			else
			{
				if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith("/"))
				{
					returnUrl = "/Home/Index";
				}
				return Redirect(returnUrl);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Login(Taikhoan taiKhoan, string returnUrl)
		{
			Taikhoan a = _context.Taikhoans
				.FirstOrDefault(x => x.Tendangnhap == taiKhoan.Tendangnhap);
			if (a == null)
			{
				return Redirect("Login");
			}
			if (a.Matkhau.Equals(taiKhoan.Matkhau))
			{
				await SignInUser(a);

				if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith("/"))
				{
					TempData["LoginSuccess"] = "Username or Password wrong!!!";
					returnUrl = "/Home/Index";
				}
				return Redirect(returnUrl);
			}
			else
			{
				TempData["LoginFailed"] = "Username or Password wrong!!!";
				return RedirectToAction("Login");
			}
		}
		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login");
		}
		private async Task SignInUser(Taikhoan accounts)
		{
			Taikhoan user = _context.Taikhoans.Where(x => x.Idtaikhoan == accounts.Idtaikhoan).FirstOrDefault();
			//Account user = context.Account.Where(x => x.Username == accounts.Username).FirstOrDefault();

			var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.Idtaikhoan.ToString()),
			new Claim(ClaimTypes.Role, user.Vaitro.ToString())
		};

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity));
		}


		// GET: Taikhoans/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(m => m.Idtaikhoan == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // GET: Taikhoans/Create
        public IActionResult Create()
        {

			var Daactive = _context.Taikhoans.Where(x => x.Active == true);
			var Dadisable = _context.Taikhoans.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			return View();
        }

        // POST: Taikhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Taikhoan taikhoan)
        {
			var checktk = _context.Taikhoans.FirstOrDefault(s => s.Tendangnhap == taikhoan.Tendangnhap);
            if (checktk == null)
            {
                if (ModelState.IsValid)
                {
                    taikhoan.Active = true;
                    _context.Add(taikhoan);
                    _context.SaveChanges();
					TempData["CreateSuccess"] = taikhoan.Tendangnhap;
					return RedirectToAction(nameof(Create));
                }
            }
            else
            {
				TempData["AlertMessagetk"] = " This " + taikhoan.Tendangnhap + " already exists.";
			}



			var Daactive = _context.Khachhangs.Where(x => x.Active == true);
			var Dadisable = _context.Khachhangs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["CreateFail"] = taikhoan.Tendangnhap;

			return View(taikhoan);
        }

        // GET: Taikhoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound();
            }

			var Daactive = _context.Taikhoans.Where(x => x.Active == true);
			var Dadisable = _context.Taikhoans.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			return View(taikhoan);
        }

        // POST: Taikhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Taikhoan taikhoan)
        {
            if (id != taikhoan.Idtaikhoan)
            {
                return NotFound();
            }

			var ltaikhoan = _context.Taikhoans.FirstOrDefault(x => x.Idtaikhoan == taikhoan.Idtaikhoan);

			if (ModelState.IsValid)
            {
                try
                {
					ltaikhoan.Tendangnhap = taikhoan.Tendangnhap;
					ltaikhoan.Matkhau = taikhoan.Matkhau;
                    _context.Update(ltaikhoan);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaikhoanExists(taikhoan.Idtaikhoan))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["EditSuccess"] = taikhoan.Tendangnhap;
				return RedirectToAction(nameof(Create));
            }

			var Daactive = _context.Khachhangs.Where(x => x.Active == true);
			var Dadisable = _context.Khachhangs.Where(x => x.Active == false);


			ViewBag.Daactive = Daactive;
			ViewBag.Dadisable = Dadisable;

			TempData["EditFail"] = taikhoan.Tendangnhap;

			return View(taikhoan);
        }

        // GET: Taikhoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Taikhoans == null)
            {
                return NotFound();
            }

            var taikhoan = await _context.Taikhoans
                .FirstOrDefaultAsync(m => m.Idtaikhoan == id);
            if (taikhoan == null)
            {
                return NotFound();
            }

            return View(taikhoan);
        }

        // POST: Taikhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Taikhoans == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Taikhoans'  is null.");
            }
            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan != null)
            {
                _context.Taikhoans.Remove(taikhoan);
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

			var da = await _context.Taikhoans
	                    .FirstOrDefaultAsync(m => m.Idtaikhoan == id);

			if (da == null)
			{
				return NotFound();
			}

			var checknv = await _context.Nhanviens
						.FirstOrDefaultAsync(t => t.Taikhoanidtaikhoan == da.Idtaikhoan);

			var checkdvvc = await _context.Donvivanchuyens
						.FirstOrDefaultAsync(x => x.Taikhoanidtaikhoan == da.Idtaikhoan);

			var checkncc = await _context.Nhacungcaps
						.FirstOrDefaultAsync(y => y.Taikhoanidtaikhoan == da.Idtaikhoan);

			var checkkh = await _context.Khachhangs
						.FirstOrDefaultAsync(z => z.Taikhoanidtaikhoan == da.Idtaikhoan);

			if (checknv != null)
            {
				checknv.Active = false;
			} 
            
            if (checkdvvc != null)
            {
				checkdvvc.Active = false;
			} 
            
            if (checkkh != null)
            {
				checkkh.Active = false;
			} 
            
            if (checkncc != null)
            {
				checkncc.Active = false;
			}

			da.Active = false;
			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Tendangnhap;
			return RedirectToAction(nameof(Create));
		}

		public async Task<IActionResult> Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Taikhoans
				.FirstOrDefaultAsync(m => m.Idtaikhoan == id);

			if (da == null)
			{
				return NotFound();
			}

			var checknv = await _context.Nhanviens
						.FirstOrDefaultAsync(t => t.Taikhoanidtaikhoan == da.Idtaikhoan);

			var checkdvvc = await _context.Donvivanchuyens
						.FirstOrDefaultAsync(x => x.Taikhoanidtaikhoan == da.Idtaikhoan);

			var checkncc = await _context.Nhacungcaps
						.FirstOrDefaultAsync(y => y.Taikhoanidtaikhoan == da.Idtaikhoan);

			var checkkh = await _context.Khachhangs
						.FirstOrDefaultAsync(z => z.Taikhoanidtaikhoan == da.Idtaikhoan);

			if (checknv != null)
			{
				checknv.Active = true;
			}

			if (checkdvvc != null)
			{
				checkdvvc.Active = true;
			}

			if (checkkh != null)
			{
				checkkh.Active = true;
			}

			if (checkncc != null)
			{
				checkncc.Active = true;
			}

			da.Active = true;
			await _context.SaveChangesAsync();
			TempData["AlertReturnMessage"] = da.Tendangnhap;
			return RedirectToAction(nameof(Create));
		}

		private bool TaikhoanExists(int id)
        {
          return (_context.Taikhoans?.Any(e => e.Idtaikhoan == id)).GetValueOrDefault();
        }
    }
}
