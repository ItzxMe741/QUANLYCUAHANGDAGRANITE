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
    public class PhieuthanhtoansController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public PhieuthanhtoansController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Phieuthanhtoans
        public IActionResult Index(DateTime frmdatepntt, DateTime todatepntt, DateTime frmdateptt, DateTime todateptt)
        {

			var fdatepntt = frmdatepntt.ToString("ddMMyyyy");
			var tdatepntt = todatepntt.ToString("ddMMyyyy");
			var fdateptt = frmdateptt.ToString("ddMMyyyy");
			var tdateptt = todateptt.ToString("ddMMyyyy");

			if(fdatepntt != "01010001" && tdatepntt != "01010001" && Int32.Parse(fdatepntt) <= Int32.Parse(tdatepntt))
			{
				var phieunhapkho = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Ngaydat >= frmdatepntt && x.Ngaydat <= todatepntt).ToList();
				ViewBag.Chitietnhaps = phieunhapkho;
			}
			else
			{
				var phieunhapkho = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();
				ViewBag.Chitietnhaps = phieunhapkho;
			}

			if (fdateptt != "01010001" && tdateptt != "01010001" && Int32.Parse(fdateptt) <= Int32.Parse(tdateptt))
			{
				var phieuactive = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Ngaythanhtoan >= frmdateptt && x.Ngaythanhtoan <= todateptt).ToList();
				ViewBag.Phieuactive = phieuactive;
				TempData["Selecttab2"] = true;
			}
			else
			{
				var phieuactive = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true).ToList();
				ViewBag.Phieuactive = phieuactive;
			}

			var phieuinactive = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == false).ToList();
			
			ViewBag.Phieuinactive = phieuinactive;

			return View();
		}

        // GET: Phieuthanhtoans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
			if (id == null || _context.Phieuthanhtoans == null)
			{
				return NotFound();
			}

			var phieuthanhtoan = await _context.Phieuthanhtoans.FindAsync(id);
			if (phieuthanhtoan == null)
			{
				return NotFound();
			}
			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var phieunhapkho = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpnk == phieuthanhtoan.Phieunhapkhoidpnk).FirstOrDefault();
			ViewData["idpnk"] = phieunhapkho.Idpnk;
			double sotienconlai = Convert.ToDouble(phieunhapkho.Tongtien) - Convert.ToDouble(phieunhapkho.Sotiendathanhtoan) + Convert.ToDouble(phieuthanhtoan.Sotienthanhtoan);

			ViewData["sotienconlai"] = sotienconlai;

			var qlchdgraniteContext = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();
			var chitietnhapvalue = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == phieuthanhtoan.Phieunhapkhoidpnk).ToList();
			ViewBag.Chitietnhap = chitietnhapvalue;
			ViewBag.Phieuthanhtoan = qlchdgraniteContext;

			return View(phieuthanhtoan);
		}

        public IActionResult DownPDF(int? id)
        {
            if (id == null || _context.Phieunhapkhos == null)
            {
                return NotFound();
            }

            var phieunhapkho = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
                                                         .Include(p => p.NhacungcapidnccNavigation)
                                                         .Include(p => p.NhanvienidnhanvienNavigation)
                                                         .Include(p => p.TrangthaiidttNavigation)
                                                         .Where(x => x.Active == true && x.Idpnk == id).FirstOrDefault();


            var Phieuthanhtoanvalue = _context.Phieuthanhtoans.Include(p => p.PhieunhapkhoidpnkNavigation)
                                                           .Include(p => p.NhanvienidnhanvienNavigation)
                                                           .Include(p => p.HtttidhtttNavigation)
                                                         .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();

            if (phieunhapkho == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("DownPDF", phieunhapkho)
            {
                FileName = $"Phieuthanhtoan_{phieunhapkho.Sopnk}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = phieunhapkho,
                    ["Phieuthanhtoan"] = Phieuthanhtoanvalue
                }

            };

        }

        private string TaoSoPhieu()
		{

			string ngayThangNam = DateTime.Now.ToString("ddMMyy");

			var phieuCu = _context.Phieuthutiens.OrderByDescending(x => x.Sopthu).FirstOrDefault(p => p.Sopthu.StartsWith($"PTT{ngayThangNam}"));

			if (phieuCu != null)
			{
				string soPhieuCu = phieuCu.Sopthu.Substring(9);
				int soPhieuMoi = int.Parse(soPhieuCu) + 1;
				return $"PTT{ngayThangNam}{soPhieuMoi:D4}";
			}
			else
			{
				return $"PTT{ngayThangNam}0001";
			}
		}

		// GET: Phieuthanhtoans/Create
		public IActionResult Create(int id)
        {
			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			string Sophieutudong = TaoSoPhieu();
			ViewData["idtd"] = Sophieutudong;
			var phieunhapkho = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpnk == id).FirstOrDefault();
			ViewData["idpnk"] = phieunhapkho.Idpnk;
			double sotienconlai = Convert.ToDouble(phieunhapkho.Tongtien) - Convert.ToDouble(phieunhapkho.Sotiendathanhtoan);

			ViewData["sotienconlai"] = sotienconlai;

			var qlchdgraniteContext = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();
			var chitietnhapvalue = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();
			ViewBag.Chitietnhap = chitietnhapvalue;
			ViewBag.Phieuthanhtoan = qlchdgraniteContext;

			return View();
        }

        // POST: Phieuthanhtoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idptt,Htttidhttt,Nhanvienidnhanvien,Soptt,Phieunhapkhoidpnk,Sotienthanhtoan,Ngaythanhtoan")] Phieuthanhtoan phieuthanhtoan)
        {
            if (ModelState.IsValid)
            {
				var phieunhapkho = _context.Phieunhapkhos.Where(x => x.Active == true && x.Idpnk == phieuthanhtoan.Phieunhapkhoidpnk).FirstOrDefault();
				
				var sotienconlai = phieunhapkho.Sotiendathanhtoan + phieuthanhtoan.Sotienthanhtoan;
				phieunhapkho.Sotiendathanhtoan = sotienconlai;
				phieuthanhtoan.Active = true;
				_context.Update(phieunhapkho);
				_context.Add(phieuthanhtoan);
				await _context.SaveChangesAsync();

				var ctp = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
										 .Include(p => p.LoaithungidloaithungNavigation)
										 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == phieuthanhtoan.Phieunhapkhoidpnk).ToList();

				var pnk = _context.Phieunhapkhos.Where(x => x.Active == true && x.Idpnk == phieuthanhtoan.Phieunhapkhoidpnk).FirstOrDefault();

				if (pnk.Tongtien - pnk.Sotiendathanhtoan == 0)
				{
					foreach (var item in ctp)
					{
						var loaithung = _context.Loaithungs.Where(x => x.Active == true && x.Idloaithung == item.Loaithungidloaithung).FirstOrDefault();
						var tongsothungnhap = _context.Chitietnhaps.Include(x => x.PhieunhapkhoidpnkNavigation)
																.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung)
																.Sum(s => s.Sothung);
						var tongsothungxuat = _context.Chitietxuats.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung).Sum(s => s.Sothunggiaokh);
						var sothungcon = tongsothungnhap - tongsothungxuat;
						loaithung.Sothungcon = sothungcon;
					}
					_context.SaveChanges();
				}

				TempData["CreateSuccess"] = phieuthanhtoan.Soptt;

				return RedirectToAction(nameof(Index));
			}
			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			string Sophieutudong = TaoSoPhieu();
			ViewData["idtd"] = Sophieutudong;
			var phieunhapkho1 = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpnk == phieuthanhtoan.Phieunhapkhoidpnk).FirstOrDefault();
			ViewData["idpnk"] = phieunhapkho1.Idpnk;
			double sotienconlai1 = Convert.ToDouble(phieunhapkho1.Tongtien) - Convert.ToDouble(phieunhapkho1.Sotiendathanhtoan);

			ViewData["sotienconlai"] = sotienconlai1;

			var qlchdgraniteContext = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == phieuthanhtoan.Phieunhapkhoidpnk).ToList();
			var chitietnhapvalue = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == phieuthanhtoan.Phieunhapkhoidpnk).ToList();
			ViewBag.Chitietnhap = chitietnhapvalue;
			ViewBag.Phieuthanhtoan = qlchdgraniteContext;
			TempData["CreateFail"] = phieuthanhtoan.Soptt;
			return View(phieuthanhtoan);
        }

        // GET: Phieuthanhtoans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
			if (id == null || _context.Phieuthanhtoans == null)
			{
				return NotFound();
			}

			var phieuthanhtoan = await _context.Phieuthanhtoans.FindAsync(id);
			if (phieuthanhtoan == null)
			{
				return NotFound();
			}
			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var phieunhapkho = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpnk == phieuthanhtoan.Phieunhapkhoidpnk).FirstOrDefault();
			ViewData["idpnk"] = phieunhapkho.Idpnk;
			double sotienconlai = Convert.ToDouble(phieunhapkho.Tongtien) - Convert.ToDouble(phieunhapkho.Sotiendathanhtoan) + Convert.ToDouble(phieuthanhtoan.Sotienthanhtoan);

			ViewData["sotienconlai"] = sotienconlai;

			var qlchdgraniteContext = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();
			var chitietnhapvalue = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == phieuthanhtoan.Phieunhapkhoidpnk).ToList();
			ViewBag.Chitietnhap = chitietnhapvalue;
			ViewBag.Phieuthanhtoan = qlchdgraniteContext;
			
			return View(phieuthanhtoan);
        }

        // POST: Phieuthanhtoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Phieuthanhtoan phieuthanhtoan)
        {
            if (id != phieuthanhtoan.Idptt)
            {
                return NotFound();
            }

			if (ModelState.IsValid)
            {
                try
                {
					phieuthanhtoan.Active = true;
					_context.Update(phieuthanhtoan);
                    _context.SaveChanges();
					var sotiendathanhtoan = _context.Phieuthanhtoans.Where(x => x.Active == true && x.Phieunhapkhoidpnk == phieuthanhtoan.Phieunhapkhoidpnk).Sum(x => x.Sotienthanhtoan);
					var phieunhapkho = _context.Phieunhapkhos.Where(x => x.Active == true && x.Idpnk == phieuthanhtoan.Phieunhapkhoidpnk).FirstOrDefault();
					phieunhapkho.Sotiendathanhtoan = sotiendathanhtoan;
					_context.Update(phieunhapkho);
					_context.SaveChanges();

					var ctp = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == phieuthanhtoan.Phieunhapkhoidpnk).ToList();

					var pnk = _context.Phieunhapkhos.Where(x => x.Active == true && x.Idpnk == phieuthanhtoan.Phieunhapkhoidpnk).FirstOrDefault();

					if (pnk.Tongtien - pnk.Sotiendathanhtoan == 0)
					{
						foreach (var item in ctp)
						{
							var loaithung = _context.Loaithungs.Where(x => x.Active == true && x.Idloaithung == item.Loaithungidloaithung).FirstOrDefault();
							var tongsothungnhap = _context.Chitietnhaps.Include(x => x.PhieunhapkhoidpnkNavigation)
																	.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung)
																	.Sum(s => s.Sothung);
							var tongsothungxuat = _context.Chitietxuats.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung).Sum(s => s.Sothunggiaokh);
							var sothungcon = tongsothungnhap - tongsothungxuat;
							loaithung.Sothungcon = sothungcon;
						}

						_context.SaveChanges();
					}
					

				}
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuthanhtoanExists(phieuthanhtoan.Idptt))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["Selecttab2"] = true;
				TempData["EditSuccess"] = phieuthanhtoan.Soptt;
                return RedirectToAction(nameof(Index));
            }
			TempData["Htttidhttt"] = _context.Hinhthucthanhtoans.Include(x => x.NganhangidnganhangNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var phieunhapkho1 = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Idpnk == phieuthanhtoan.Phieunhapkhoidpnk).FirstOrDefault();
			ViewData["idpnk"] = phieunhapkho1.Idpnk;
			double sotienconlai = Convert.ToDouble(phieunhapkho1.Tongtien) - Convert.ToDouble(phieunhapkho1.Sotiendathanhtoan) + Convert.ToDouble(phieuthanhtoan.Sotienthanhtoan);

			ViewData["sotienconlai"] = sotienconlai;

			var qlchdgraniteContext = _context.Phieuthanhtoans.Include(p => p.HtttidhtttNavigation)
														 .Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();
			var chitietnhapvalue = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == phieuthanhtoan.Phieunhapkhoidpnk).ToList();
			ViewBag.Chitietnhap = chitietnhapvalue;
			ViewBag.Phieuthanhtoan = qlchdgraniteContext;
			TempData["EditFail"] = phieuthanhtoan.Soptt;
			return View(phieuthanhtoan);
        }

        // GET: Phieuthanhtoans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phieuthanhtoans == null)
            {
                return NotFound();
            }

            var phieuthanhtoan = await _context.Phieuthanhtoans
                .Include(p => p.HtttidhtttNavigation)
                .Include(p => p.NhanvienidnhanvienNavigation)
                .FirstOrDefaultAsync(m => m.Idptt == id);
            if (phieuthanhtoan == null)
            {
                return NotFound();
            }

            return View(phieuthanhtoan);
        }

		public IActionResult Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var da = _context.Phieuthanhtoans.FirstOrDefault(m => m.Idptt == id);
			if (da == null)
			{
				return NotFound();
			}

			var phieunhap = _context.Phieunhapkhos
			   .FirstOrDefault(m => m.Idpnk == da.Phieunhapkhoidpnk && m.Active == true);

			var result = phieunhap.Sotiendathanhtoan - da.Sotienthanhtoan;
			
			var sotiencon = phieunhap.Tongtien - phieunhap.Sotiendathanhtoan;
			if (sotiencon == 0)
			{
				var ctp = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
														 .Where(x => x.Active == true && x.Phieunhapkhoidpnk == da.Phieunhapkhoidpnk).ToList();
				foreach (var item in ctp)
				{
					var loaithung = _context.Loaithungs.Where(x => x.Active == true && x.Idloaithung == item.Loaithungidloaithung).FirstOrDefault();
					var sothungcon = loaithung.Sothungcon - item.Sothung;
					loaithung.Sothungcon = sothungcon;
				}
			}

			phieunhap.Sotiendathanhtoan = result;
			da.Active = false;

			_context.SaveChanges();
			TempData["DeleteSuccess"] = da.Soptt;
			return RedirectToAction(nameof(Index));
		}


		// POST: Phieuthanhtoans/Delete/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phieuthanhtoans == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Phieuthanhtoans'  is null.");
            }
            var phieuthanhtoan = await _context.Phieuthanhtoans.FindAsync(id);
            if (phieuthanhtoan != null)
            {
                _context.Phieuthanhtoans.Remove(phieuthanhtoan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuthanhtoanExists(int id)
        {
          return (_context.Phieuthanhtoans?.Any(e => e.Idptt == id)).GetValueOrDefault();
        }
    }
}
