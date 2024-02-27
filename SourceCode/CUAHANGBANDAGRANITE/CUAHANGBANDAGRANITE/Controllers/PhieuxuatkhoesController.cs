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
    public class PhieuxuatkhoesController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public PhieuxuatkhoesController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Phieuxuatkhoes
        public IActionResult Index(DateTime frmdatepx, DateTime todatepx, DateTime frmdatepxdthu, DateTime todatepxdthu)
        {

			var fdatepx = frmdatepx.ToString("ddMMyyyy");
			var tdatepx = todatepx.ToString("ddMMyyyy");
			var fdatepxdthu = frmdatepxdthu.ToString("ddMMyyyy");
			var tdatepxdthu = todatepxdthu.ToString("ddMMyyyy");

			if (fdatepx != "01010001" && tdatepx != "01010001" && Int32.Parse(fdatepx) <= Int32.Parse(tdatepx))
			{
				var phieuactive = _context.Phieuxuatkhos.Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Where(x => x.Active == true && x.Sotiendathu == 0 && x.Ngaydat >= frmdatepx && x.Ngaydat <= todatepx).ToList();
				ViewBag.Phieuactive = phieuactive;
			}
			else
			{
				var phieuactive = _context.Phieuxuatkhos.Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Where(x => x.Active == true && x.Sotiendathu == 0).ToList();
				ViewBag.Phieuactive = phieuactive;
			}

			var phieuinactive = _context.Phieuxuatkhos.Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Where(x => x.Active == false).ToList();
			if (fdatepxdthu != "01010001" && tdatepxdthu != "01010001" && Int32.Parse(fdatepxdthu) <= Int32.Parse(tdatepxdthu))
			{
				var phieudathu = _context.Phieuxuatkhos.Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Where(x => x.Active == true && x.Sotiendathu != 0 && x.Ngaydat >= frmdatepxdthu && x.Ngaydat <= todatepxdthu).ToList();
				ViewBag.Phieudathu = phieudathu;
				TempData["Selecttab2"] = true;
			}
			else
			{
				var phieudathu = _context.Phieuxuatkhos.Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Where(x => x.Active == true && x.Sotiendathu != 0).ToList();
				ViewBag.Phieudathu = phieudathu;
			}

			ViewBag.Phieuinactive = phieuinactive;
			

			return View();
        }

		public JsonResult Diendongia(double sloaithung)
		{

			var loaithung = _context.Loaithungs.Include(p => p.DagraniteiddaNavigation)
											   .Include(p => p.QuycachidquycachNavigation)
											   .Where(x => x.Active == true && x.Idloaithung == sloaithung).FirstOrDefault();

			var tongsothungnhap = _context.Chitietnhaps.Where(x => x.Active == true && x.Loaithungidloaithung == sloaithung).Sum(s => s.Sothung);

			var tongsothungxuat = _context.Chitietxuats.Where(x => x.Active == true && x.Loaithungidloaithung == sloaithung).Sum(s => s.Sothunggiaokh);

			var sothungcon = tongsothungnhap - tongsothungxuat;

			var result = loaithung.Dongiaban;

			var response = new { dongia = result, sothungconlai = sothungcon };

			return Json(response);
		}

		public JsonResult Tinhsothung(float idientich, double sloaithung,float sothunghienco)
		{

			var loaithung = _context.Loaithungs.Include(p => p.DagraniteiddaNavigation)
											   .Include(p => p.QuycachidquycachNavigation)
											   .Where(x => x.Active == true && x.Idloaithung == sloaithung).FirstOrDefault();

			var quycachdonggoi = _context.Quycachdonggois.Where(x => x.Active == true && x.Idquycach == loaithung.Quycachidquycach).FirstOrDefault();

			var tongsothungnhap = _context.Chitietnhaps.Include(x => x.PhieunhapkhoidpnkNavigation)
														.Where(x => x.Active == true && x.Loaithungidloaithung == sloaithung && x.PhieunhapkhoidpnkNavigation.Tongtien - x.PhieunhapkhoidpnkNavigation.Sotiendathanhtoan == 0)
														.Sum(s => s.Sothung);

			var tongsothungxuat = _context.Chitietxuats.Where(x => x.Active == true && x.Loaithungidloaithung == sloaithung).Sum(s => s.Sothunggiaokh);

			var sothungcon = tongsothungnhap - tongsothungxuat - sothunghienco;

			var result = idientich / (quycachdonggoi.Dientichbemat);

			if (result % 1 != 0)
			{
				result += 1;
			}

			result = Math.Floor((double)result);

			var sothungthuc = sothungcon - result;
			if (sothungthuc < 0)
			{
				var response = new { resultkq = sothungthuc, conlai = sothungcon };
				return Json(response);
			}

			var response1 = new { resultkq = result, conlai = 0 };

			return Json(response1);
		}

		public IActionResult Detailphieuthu(int? id)
		{
			if (id == null || _context.Phieuxuatkhos == null)
			{
				return NotFound();
			}

			Phieuxuatkho phieuxuatkho = _context.Phieuxuatkhos.Include(x => x.Chitietxuats)
															  .Include(x => x.Thongtinxes).Where(a => a.Idpxk == id).FirstOrDefault();

			if (phieuxuatkho == null)
			{
				return NotFound();
			}

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true && x.Sothungcon != 0).ToList();
			TempData["Donvivanchuyeniddvvc"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();
			TempData["Khachhangs"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
			//TempData["Trangthaiidtt"] = _context.Trangthais.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieuxuatkho = qlchdgraniteContext;

			return View(phieuxuatkho);
		}

		public IActionResult Detaildathu(int? id)
		{
			if (id == null || _context.Phieuxuatkhos == null)
			{
				return NotFound();
			}

			Phieuxuatkho phieuxuatkho = _context.Phieuxuatkhos.Include(x => x.Chitietxuats)
															  .Include(x => x.Thongtinxes).Where(a => a.Idpxk == id).FirstOrDefault();

			if (phieuxuatkho == null)
			{
				return NotFound();
			}

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true && x.Sothungcon != 0).ToList();
			TempData["Donvivanchuyeniddvvc"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();
			TempData["Khachhangs"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
			//TempData["Trangthaiidtt"] = _context.Trangthais.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieuxuatkho = qlchdgraniteContext;

			return View(phieuxuatkho);
		}

		// GET: Phieuxuatkhoes/Details/5
		public IActionResult Details(int? id)
        {
			if (id == null || _context.Phieuxuatkhos == null)
			{
				return NotFound();
			}

			Phieuxuatkho phieuxuatkho = _context.Phieuxuatkhos.Include(x => x.Chitietxuats)
															  .Include(x => x.Thongtinxes).Where(a => a.Idpxk == id).FirstOrDefault();

			if (phieuxuatkho == null)
			{
				return NotFound();
			}

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true && x.Sothungcon != 0).ToList();
			TempData["Donvivanchuyeniddvvc"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();
			TempData["Khachhangs"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
			//TempData["Trangthaiidtt"] = _context.Trangthais.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieuxuatkho = qlchdgraniteContext;

			return View(phieuxuatkho);
        }

		public JsonResult Kiemtradongia(float idongia, double sloaithung)
		{

			var loaithung = _context.Loaithungs.Include(p => p.DagraniteiddaNavigation)
											   .Include(p => p.QuycachidquycachNavigation)
											   .Where(x => x.Active == true && x.Idloaithung == sloaithung).FirstOrDefault();

			var result = loaithung.Dongiaban - idongia;

			return Json(result);
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

			double sotienconlai = Convert.ToDouble(phieuxuatkho.Tongtien) - Convert.ToDouble(phieuxuatkho.Sotiendathu);

			var chitietxuatvalue = _context.Chitietxuats.Include(p => p.PhieuxuatkhoidpxkNavigation)
														 .Include(p => p.LoaithungidloaithungNavigation)
															.ThenInclude(x => x.QuycachidquycachNavigation)
														 .Include(quycach => quycach.LoaithungidloaithungNavigation)
															.ThenInclude(x => x.DagraniteiddaNavigation)
														 .Where(x => x.Active == true && x.Phieuxuatkhoidpxk == id).ToList();
			ViewBag.Chitietxuat = chitietxuatvalue;

			if (phieuxuatkho == null)
			{
				return NotFound();
			}

			return new ViewAsPdf("DownPDF", phieuxuatkho)
			{
				FileName = $"Phieuxuatkho_{phieuxuatkho.Sopxk}.pdf",
				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
				PageSize = Rotativa.AspNetCore.Options.Size.A4,
				ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
				{
					Model = phieuxuatkho,
					["Chitietxuat"] = chitietxuatvalue
				}

			};

		}


		private string TaoSoPhieu()
		{

			string ngayThangNam = DateTime.Now.ToString("ddMMyy");

			var phieuCu = _context.Phieuxuatkhos.OrderByDescending(x => x.Sopxk).FirstOrDefault(p => p.Sopxk.StartsWith($"XK{ngayThangNam}"));

			if (phieuCu != null)
			{
				string soPhieuCu = phieuCu.Sopxk.Substring(8);
				int soPhieuMoi = int.Parse(soPhieuCu) + 1;
				return $"XK{ngayThangNam}{soPhieuMoi:D4}";
			}
			else
			{
				return $"XK{ngayThangNam}0001";
			}
		}

		// GET: Phieuxuatkhoes/Create
		public IActionResult Create()
        {

			Phieuxuatkho phieuxuatkho = new Phieuxuatkho();
			phieuxuatkho.Chitietxuats.Add(new Chitietxuat() { Idctx = 1 });

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true && x.Sothungcon != 0).ToList();
			TempData["Donvivanchuyeniddvvc"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();
			TempData["Khachhangs"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
			//TempData["Trangthaiidtt"] = _context.Trangthais.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieuxuatkho = qlchdgraniteContext;

			string Sophieutudong = TaoSoPhieu();
			ViewData["idtd"] = Sophieutudong;

			return View(phieuxuatkho);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Phieuxuatkho phieuxuatkho)
        {
            if (ModelState.IsValid)
            {
				phieuxuatkho.Chitietxuats.RemoveAll(n => n.Isdeleted == false || n.Loaithungidloaithung == null);
				foreach (var item in phieuxuatkho.Chitietxuats)
				{
					var loaithung = _context.Loaithungs.Where(x => x.Active == true && x.Idloaithung == item.Loaithungidloaithung).FirstOrDefault();
					loaithung.Sothungcon = loaithung.Sothungcon - item.Sothunggiaokh;
					_context.Update(loaithung);
				}
				phieuxuatkho.Sotiendathu = 0;
				phieuxuatkho.Active = true;
				_context.Add(phieuxuatkho);
                _context.SaveChanges();
				TempData["CreateSuccess"] = phieuxuatkho.Sopxk;
				return RedirectToAction(nameof(Index));
            }

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true && x.Sothungcon != 0).ToList();
			TempData["Donvivanchuyeniddvvc"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();
			TempData["Khachhangs"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
			//TempData["Trangthaiidtt"] = _context.Trangthais.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieuxuatkho = qlchdgraniteContext;

			string Sophieutudong = TaoSoPhieu();
			ViewData["idtd"] = Sophieutudong;
			TempData["CreateFail"] = phieuxuatkho.Sopxk;
			return View(phieuxuatkho);
        }

        // GET: Phieuxuatkhoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Phieuxuatkhos == null)
            {
                return NotFound();
            }

			Phieuxuatkho phieuxuatkho = _context.Phieuxuatkhos.Include(x => x.Chitietxuats)
															  .Include(x => x.Thongtinxes).Where(a => a.Idpxk == id).FirstOrDefault();

			if (phieuxuatkho == null)
            {
                return NotFound();
            }

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true && x.Sothungcon != 0).ToList();
			TempData["Donvivanchuyeniddvvc"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();
			TempData["Khachhangs"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
			//TempData["Trangthaiidtt"] = _context.Trangthais.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieuxuatkho = qlchdgraniteContext;

			return View(phieuxuatkho);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Phieuxuatkho phieuxuatkho)
        {
            if (id != phieuxuatkho.Idpxk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					var OldToDelete = _context.Chitietxuats
												  .Where(n => n.Phieuxuatkhoidpxk == id)
												  .ToList();
					_context.Chitietxuats.RemoveRange(OldToDelete);
					phieuxuatkho.Chitietxuats.RemoveAll(n => n.Isdeleted == false || n.Loaithungidloaithung == null);
					phieuxuatkho.Active = true;
					_context.Update(phieuxuatkho);
					_context.SaveChanges();
					foreach (var item in phieuxuatkho.Chitietxuats)
					{
						var loaithung = _context.Loaithungs.Where(x => x.Active == true && x.Idloaithung == item.Loaithungidloaithung).FirstOrDefault();
						var tongsothungnhap = _context.Chitietnhaps.Include(x => x.PhieunhapkhoidpnkNavigation)
														.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung && x.PhieunhapkhoidpnkNavigation.Tongtien - x.PhieunhapkhoidpnkNavigation.Sotiendathanhtoan == 0)
														.Sum(s => s.Sothung);
						var tongsothungxuat = _context.Chitietxuats.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung).Sum(s => s.Sothunggiaokh);
						var sothungcon = tongsothungnhap - tongsothungxuat;
						loaithung.Sothungcon = sothungcon;

						_context.Update(loaithung);
					}
					_context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuxuatkhoExists(phieuxuatkho.Idpxk))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["EditSuccess"] = phieuxuatkho.Sopxk;
				return RedirectToAction(nameof(Index));
            }

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true && x.Sothungcon != 0).ToList();
			TempData["Donvivanchuyeniddvvc"] = _context.Donvivanchuyens.Where(x => x.Active == true).ToList();
			TempData["Khachhangs"] = _context.Khachhangs.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
			//TempData["Trangthaiidtt"] = _context.Trangthais.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieuxuatkhos.Include(p => p.DonvivanchuyeniddvvcNavigation)
														 .Include(p => p.KhachhangidkhachhangNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieuxuatkho = qlchdgraniteContext;
			TempData["EditFail"] = phieuxuatkho.Sopxk;
			return View(phieuxuatkho);
        }

		public IActionResult Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var ctp = _context.Chitietxuats.Where(x => x.Phieuxuatkhoidpxk == id).ToList();
			foreach (var item in ctp)
			{
				item.Active = false;
			}

			var da = _context.Phieuxuatkhos.FirstOrDefault(m => m.Idpxk == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;

			_context.SaveChanges();

			foreach (var item in ctp)
			{
				var loaithung = _context.Loaithungs.Where(x => x.Active == true && x.Idloaithung == item.Loaithungidloaithung).FirstOrDefault();
				var tongsothungnhap = _context.Chitietnhaps.Include(x => x.PhieunhapkhoidpnkNavigation)
															.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung && x.PhieunhapkhoidpnkNavigation.Tongtien - x.PhieunhapkhoidpnkNavigation.Sotiendathanhtoan == 0)
															.Sum(s => s.Sothung); 
				var tongsothungxuat = _context.Chitietxuats.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung).Sum(s => s.Sothunggiaokh);
				var sothungcon = tongsothungnhap - tongsothungxuat;
				loaithung.Sothungcon = sothungcon;
			}
			_context.SaveChanges();
			TempData["DeleteSuccess"] = da.Sopxk;
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var ctp = _context.Chitietxuats.Where(x => x.Phieuxuatkhoidpxk == id).ToList();
			foreach (var item in ctp)
			{
				item.Active = true;
			}

			var da = _context.Phieuxuatkhos.FirstOrDefault(m => m.Idpxk == id);

			if (da == null)
			{
				return NotFound();
			}

			da.Active = true;

			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		// GET: Phieuxuatkhoes/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phieuxuatkhos == null)
            {
                return NotFound();
            }

            var phieuxuatkho = await _context.Phieuxuatkhos
                .Include(p => p.DonvivanchuyeniddvvcNavigation)
                .Include(p => p.KhachhangidkhachhangNavigation)
                .Include(p => p.NhanvienidnhanvienNavigation)
                .Include(p => p.TrangthaiidttNavigation)
                .FirstOrDefaultAsync(m => m.Idpxk == id);
            if (phieuxuatkho == null)
            {
                return NotFound();
            }

            return View(phieuxuatkho);
        }

        // POST: Phieuxuatkhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phieuxuatkhos == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Phieuxuatkhos'  is null.");
            }
            var phieuxuatkho = await _context.Phieuxuatkhos.FindAsync(id);
            if (phieuxuatkho != null)
            {
                _context.Phieuxuatkhos.Remove(phieuxuatkho);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuxuatkhoExists(int id)
        {
          return (_context.Phieuxuatkhos?.Any(e => e.Idpxk == id)).GetValueOrDefault();
        }
    }
}
