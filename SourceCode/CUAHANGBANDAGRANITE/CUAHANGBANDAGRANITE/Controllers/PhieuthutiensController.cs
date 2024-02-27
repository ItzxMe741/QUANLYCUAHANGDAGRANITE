using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUAHANGBANDAGRANITE.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Rotativa.AspNetCore;

namespace CUAHANGBANDAGRANITE.Controllers
{
    public class PhieuthutiensController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public PhieuthutiensController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Phieuthutiens
        public IActionResult Index(DateTime frmdatepxthu, DateTime todatepxthu, DateTime frmdatepthu, DateTime todatepthu)
        {
			var fdatepxthu = frmdatepxthu.ToString("ddMMyyyy");
			var tdatepxthu = todatepxthu.ToString("ddMMyyyy");
			var fdatepthu = frmdatepthu.ToString("ddMMyyyy");
			var tdatepthu = todatepthu.ToString("ddMMyyyy");

			if (fdatepxthu != "01010001" && tdatepxthu != "01010001" && Int32.Parse(fdatepxthu) <= Int32.Parse(tdatepxthu))
			{
				var phieuxuatkho = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Ngaydat >= frmdatepxthu && x.Ngaydat <= todatepxthu).ToList();
				ViewBag.Chitietxuats = phieuxuatkho;
			}
			else
			{
				var phieuxuatkho = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();
				ViewBag.Chitietxuats = phieuxuatkho;
			}

			if (fdatepthu != "01010001" && tdatepthu != "01010001" && Int32.Parse(fdatepthu) <= Int32.Parse(tdatepthu))
			{
				var phieuactive = _context.Phieuthutiens.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Ngaythutien >= frmdatepthu && x.Ngaythutien <= todatepthu).ToList();
				ViewBag.Phieuactive = phieuactive;
				TempData["Selecttab2"] = true;
			}
			else
			{
				var phieuactive = _context.Phieuthutiens.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true).ToList();
				ViewBag.Phieuactive = phieuactive;
			}

				
			var phieuinactive = _context.Phieuthutiens.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == false).ToList();
			
			
			ViewBag.Phieuinactive = phieuinactive;
			
			return View();
        }

        public IActionResult DownPDF(int? id)
        {
            if (id == null || _context.Phieuxuatkhos == null)
            {
                return NotFound();
            }

            var phieuxuatkho = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
                                                         .Include(p => p.KhachhangidkhachhangNavigation)
                                                         .Include(p => p.NhanvienidnhanvienNavigation)
                                                         .Include(p => p.TrangthaiidttNavigation)
                                                         .Where(x => x.Active == true && x.Idpxk == id).FirstOrDefault();


            var Phieuthutienvalue = _context.Phieuthutiens.Include(p => p.PhieuxuatkhoidpxkNavigation)
                                                           .Include(p => p.NhanvienidnhanvienNavigation)
                                                           .Include(p => p.HtttidhtttNavigation)
                                                         .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == id).ToList();

            if (phieuxuatkho == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DownPDF", phieuxuatkho)
            {
                FileName = $"Phieuthutien_{phieuxuatkho.Sopxk}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = phieuxuatkho,
                    ["Phieuthu"] = Phieuthutienvalue
                }

            };

        }

        // GET: Phieuthutiens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Phieuthutiens == null)
			{
				return NotFound();
			}

			var phieuthutien = await _context.Phieuthutiens.FindAsync(id);
			if (phieuthutien == null)
			{
				return NotFound();
			}

			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var phieuxuatkho = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpxk == phieuthutien.Phieuxuatkhoidpxk).FirstOrDefault();
			ViewData["idpnk"] = phieuxuatkho.Idpxk;
			double sotienconlai = Convert.ToDouble(phieuxuatkho.Tongtien) - Convert.ToDouble(phieuxuatkho.Sotiendathu) + Convert.ToDouble(phieuthutien.Sotienthu);

			ViewData["sotienconlai"] = sotienconlai;

			var qlchdgraniteContext = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();

			var chitietxuatvalue = _context.Chitietxuats.Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == phieuthutien.Phieuxuatkhoidpxk).ToList();
			ViewBag.Chitietxuat = chitietxuatvalue;
			ViewBag.Phieuthu = qlchdgraniteContext;

			return View(phieuthutien);
		}

		private string TaoSoPhieu()
		{

			string ngayThangNam = DateTime.Now.ToString("ddMMyy");

			var phieuCu = _context.Phieuthutiens.OrderByDescending(x => x.Sopthu).FirstOrDefault(p => p.Sopthu.StartsWith($"PTHU{ngayThangNam}"));

			if (phieuCu != null)
			{
				string soPhieuCu = phieuCu.Sopthu.Substring(10);
				int soPhieuMoi = int.Parse(soPhieuCu) + 1;
				return $"PTHU{ngayThangNam}{soPhieuMoi:D4}";
			}
			else
			{
				return $"PTHU{ngayThangNam}0001";
			}
		}

		// GET: Phieuthutiens/Create
		public IActionResult Create(int id)
        {
			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			string Sophieutudong = TaoSoPhieu();
			ViewData["idtd"] = Sophieutudong;
			var phieuxuatkho = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpxk == id).FirstOrDefault();
			ViewData["idpxk"] = phieuxuatkho.Idpxk;
			double sotienconlai = Convert.ToDouble(phieuxuatkho.Tongtien) - Convert.ToDouble(phieuxuatkho.Sotiendathu);

			ViewData["sotienconlai"] = sotienconlai;

			var qlchdgraniteContext = _context.Phieuthutiens.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == id).ToList();
			var chitiexuatvalue = _context.Chitietxuats.Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == id).ToList();
			ViewBag.Chitietxuat = chitiexuatvalue;
			ViewBag.Phieuthutien = qlchdgraniteContext;
			return View();
        }

        // POST: Phieuthutiens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idpthu,Htttidhttt,Nhanvienidnhanvien,Phieuxuatkhoidpxk,Sotienthu,Sopthu,Ngaythutien")] Phieuthutien phieuthutien)
        {
            if (ModelState.IsValid)
            {
                var phieuxuatkho = _context.Phieuxuatkhos.Where(x=>x.Active == true && x.Idpxk == phieuthutien.Phieuxuatkhoidpxk).FirstOrDefault();
                if(phieuxuatkho.Sotiendathu == null)
                {
                    phieuxuatkho.Sotiendathu = 0;

				}
                var sotienconlai = phieuxuatkho.Sotiendathu + phieuthutien.Sotienthu;
                phieuxuatkho.Sotiendathu = sotienconlai;
                phieuthutien.Active = true;
                _context.Update(phieuxuatkho);
                _context.Add(phieuthutien);
                await _context.SaveChangesAsync();
				TempData["CreateSuccess"] = phieuthutien.Sopthu;
				return RedirectToAction(nameof(Index));
            }
			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			string Sophieutudong = TaoSoPhieu();
			ViewData["idtd"] = Sophieutudong;
			var phieuxuatkho1 = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpxk == phieuthutien.Phieuxuatkhoidpxk).FirstOrDefault();
			ViewData["idpxk"] = phieuxuatkho1.Idpxk;
			double sotienconlai1 = Convert.ToDouble(phieuxuatkho1.Tongtien) - Convert.ToDouble(phieuxuatkho1.Sotiendathu);

			ViewData["sotienconlai"] = sotienconlai1;

			var qlchdgraniteContext = _context.Phieuthutiens.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == phieuthutien.Phieuxuatkhoidpxk).ToList();
			var chitiexuatvalue = _context.Chitietxuats.Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == phieuthutien.Phieuxuatkhoidpxk).ToList();
			ViewBag.Chitietxuat = chitiexuatvalue;
			ViewBag.Phieuthutien = qlchdgraniteContext;

			TempData["CreateFail"] = phieuthutien.Sopthu;

			return View(phieuthutien);
        }

        // GET: Phieuthutiens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Phieuthutiens == null)
            {
                return NotFound();
            }

            var phieuthutien = await _context.Phieuthutiens.FindAsync(id);
            if (phieuthutien == null)
            {
                return NotFound();
            }

			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var phieuxuatkho = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpxk == phieuthutien.Phieuxuatkhoidpxk).FirstOrDefault();
			ViewData["idpnk"] = phieuxuatkho.Idpxk;
			double sotienconlai = Convert.ToDouble(phieuxuatkho.Tongtien) - Convert.ToDouble(phieuxuatkho.Sotiendathu) + Convert.ToDouble(phieuthutien.Sotienthu);

			ViewData["sotienconlai"] = sotienconlai;

			var qlchdgraniteContext = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();

			var chitietxuatvalue = _context.Chitietxuats.Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == phieuthutien.Phieuxuatkhoidpxk).ToList();
			ViewBag.Chitietxuat = chitietxuatvalue;
			ViewBag.Phieuthu = qlchdgraniteContext;

			return View(phieuthutien);
        }

        // POST: Phieuthutiens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Phieuthutien phieuthutien)
        {
            if (id != phieuthutien.Idpthu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					phieuthutien.Active = true;
					_context.Update(phieuthutien);
					_context.SaveChanges();
					var sotiendathu = _context.Phieuthutiens.Where(x => x.Active == true && x.Phieuxuatkhoidpxk == phieuthutien.Phieuxuatkhoidpxk).Sum(x => x.Sotienthu);
					var phieuxuatkho = _context.Phieuxuatkhos.Where(x => x.Active == true && x.Idpxk == phieuthutien.Phieuxuatkhoidpxk).FirstOrDefault();
					phieuxuatkho.Sotiendathu = sotiendathu;
					_context.Update(phieuxuatkho);
					_context.SaveChanges();
				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuthutienExists(phieuthutien.Idpthu))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["Selecttab2"] = true;
				TempData["EditSuccess"] = phieuthutien.Sopthu;
				return RedirectToAction(nameof(Index));
            }

			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var phieuxuatkho1 = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpxk == phieuthutien.Phieuxuatkhoidpxk).FirstOrDefault();
			ViewData["idpnk"] = phieuxuatkho1.Idpxk;
			double sotienconlai = Convert.ToDouble(phieuxuatkho1.Tongtien) - Convert.ToDouble(phieuxuatkho1.Sotiendathu) + Convert.ToDouble(phieuthutien.Sotienthu);

			ViewData["sotienconlai"] = sotienconlai;

			var qlchdgraniteContext = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();

			var chitietxuatvalue = _context.Chitietxuats.Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == phieuthutien.Phieuxuatkhoidpxk).ToList();
			ViewBag.Chitietxuat = chitietxuatvalue;
			ViewBag.Phieuthu = qlchdgraniteContext;
			TempData["EditFail"] = phieuthutien.Sopthu;
			return View(phieuthutien);
        }

        // GET: Phieuthutiens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phieuthutiens == null)
            {
                return NotFound();
            }

            var phieuthutien = await _context.Phieuthutiens
                .Include(p => p.HtttidhtttNavigation)
                .Include(p => p.NhanvienidnhanvienNavigation)
                .FirstOrDefaultAsync(m => m.Idpthu == id);
            if (phieuthutien == null)
            {
                return NotFound();
            }

            return View(phieuthutien);
        }

		public async Task<IActionResult> Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = await _context.Phieuthutiens
				.FirstOrDefaultAsync(m => m.Idpthu == id);
			if (da == null)
			{
				return NotFound();
			}

            var phieuxuat = await _context.Phieuxuatkhos
				.FirstOrDefaultAsync(m => m.Idpxk == da.Phieuxuatkhoidpxk && m.Active == true );

            var result = phieuxuat.Sotiendathu - da.Sotienthu;
            phieuxuat.Sotiendathu = result;

			da.Active = false;

			await _context.SaveChangesAsync();
			TempData["DeleteSuccess"] = da.Sopthu;
			return RedirectToAction(nameof(Index));
		}


		// POST: Phieuthutiens/Delete/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phieuthutiens == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Phieuthutiens'  is null.");
            }
            var phieuthutien = await _context.Phieuthutiens.FindAsync(id);
            if (phieuthutien != null)
            {
                _context.Phieuthutiens.Remove(phieuthutien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuthutienExists(int id)
        {
          return (_context.Phieuthutiens?.Any(e => e.Idpthu == id)).GetValueOrDefault();
        }
    }
}
