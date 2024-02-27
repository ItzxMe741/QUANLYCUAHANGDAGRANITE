using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUAHANGBANDAGRANITE.Models;
using static System.Net.WebRequestMethods;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CUAHANGBANDAGRANITE.Controllers
{
    public class PhieunhapkhoesController : Controller
    {
        private readonly QlchdgraniteContext _context;

        public PhieunhapkhoesController(QlchdgraniteContext context)
        {
            _context = context;
        }

        // GET: Phieunhapkhoes
        public IActionResult Index(DateTime frmdatepn, DateTime todatepn, DateTime frmdatepndtt, DateTime todatepndtt)
        {

			var fdatepn = frmdatepn.ToString("ddMMyyyy");
			var tdatepn = todatepn.ToString("ddMMyyyy");
			var fdatepndtt = frmdatepndtt.ToString("ddMMyyyy");
			var tdatepndtt = todatepndtt.ToString("ddMMyyyy");

			if (fdatepn != "01010001" && tdatepn != "01010001" && Int32.Parse(fdatepn) <= Int32.Parse(tdatepn))
			{
				//var qlchdgraniteContext = _context.Phieunhapkhos.Include(p => p.DonvivanchuyeniddvvcNavigation).Include(p => p.NhacungcapidnccNavigation).Include(p => p.NhanvienidnhanvienNavigation).Include(p => p.TrangthaiidttNavigation);
				var phieuactive = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Sotiendathanhtoan == 0 && x.Ngaydat >= frmdatepn && x.Ngaydat <= todatepn).ToList();
				ViewBag.Phieuactive = phieuactive;
			}
			else
			{
				var phieuactive = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Sotiendathanhtoan == 0).ToList();
				ViewBag.Phieuactive = phieuactive;
			}
			var phieuinactive = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == false).ToList();
			if (fdatepndtt != "01010001" && tdatepndtt != "01010001" && Int32.Parse(fdatepndtt) <= Int32.Parse(tdatepndtt))
			{
				var phieudathanhtoan = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Sotiendathanhtoan != 0 && x.Ngaydat >= frmdatepndtt && x.Ngaydat <= todatepndtt).ToList();
				ViewBag.Phieudathanhtoan = phieudathanhtoan;
				TempData["Selecttab2"] = true;
			}
			else
			{
				var phieudathanhtoan = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true && x.Sotiendathanhtoan != 0).ToList();
				ViewBag.Phieudathanhtoan = phieudathanhtoan;
			}
				

			
			ViewBag.Phieuinactive = phieuinactive;
			
			return View();
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

            double sotienconlai = Convert.ToDouble(phieunhapkho.Tongtien) - Convert.ToDouble(phieunhapkho.Sotiendathanhtoan);

            var chitietnhapvalue = _context.Chitietnhaps.Include(p => p.PhieunhapkhoidpnkNavigation)
                                                         .Include(p => p.LoaithungidloaithungNavigation)
															.ThenInclude(x => x.QuycachidquycachNavigation)
                                                         .Include(quycach => quycach.LoaithungidloaithungNavigation)
                                                            .ThenInclude(x => x.DagraniteiddaNavigation)
                                                         .Where(x => x.Active == true && x.Phieunhapkhoidpnk == id).ToList();
            ViewBag.Chitietnhap = chitietnhapvalue;

            if (phieunhapkho == null)
			{
				return NotFound();
			}

			return new ViewAsPdf("DownPDF", phieunhapkho)
			{
				FileName = $"Phieunhapkho_{phieunhapkho.Sopnk}.pdf",
				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
				PageSize = Rotativa.AspNetCore.Options.Size.A4,
				ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
				{
					Model = phieunhapkho,
					["Chitietnhap"] = chitietnhapvalue
				}

			};

		}


		public IActionResult Hidden(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var ctp = _context.Chitietnhaps.Where(x => x.Phieunhapkhoidpnk == id).ToList();
			foreach (var item in ctp)
			{
				item.Active = false;
			}

			var da = _context.Phieunhapkhos.FirstOrDefault(m => m.Idpnk == id);
			if (da == null)
			{
				return NotFound();
			}


			da.Active = false;

			_context.SaveChanges();
			TempData["DeleteSuccess"] = da.Sopnk;
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Getback(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var ctp = _context.Chitietnhaps.Where(x => x.Phieunhapkhoidpnk == id).ToList();
			foreach (var item in ctp)
			{
				item.Active = true;
			}

			var da = _context.Phieunhapkhos.FirstOrDefault(m => m.Idpnk == id);

			if (da == null)
			{
				return NotFound();
			}

			da.Active = true;

			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public JsonResult Kiemtradongia(float idongia, double sloaithung)
		{

			var loaithung = _context.Loaithungs.Include(p => p.DagraniteiddaNavigation)
											   .Include(p => p.QuycachidquycachNavigation)
											   .Where(x => x.Active == true && x.Idloaithung == sloaithung).FirstOrDefault();

			var result = idongia - loaithung.Dongiaban;

			return Json(result);
		}

		// GET: Phieunhapkhoes/Details/5
		public IActionResult Details(int? id)
        {
			if (id == null || _context.Phieunhapkhos == null)
			{
				return NotFound();
			}

			Phieunhapkho phieunhapkho = _context.Phieunhapkhos.Include(x => x.Chitietnhaps).Where(a => a.Idpnk == id).FirstOrDefault();


			if (phieunhapkho == null)
			{
				return NotFound();
			}

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhacungcapidncc"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieunhapkho = qlchdgraniteContext;

			return View(phieunhapkho);
        }

		public IActionResult Detailphieuthanhtoan(int? id)
		{
			if (id == null || _context.Phieunhapkhos == null)
			{
				return NotFound();
			}

			Phieunhapkho phieunhapkho = _context.Phieunhapkhos.Include(x => x.Chitietnhaps).Where(a => a.Idpnk == id).FirstOrDefault();


			if (phieunhapkho == null)
			{
				return NotFound();
			}

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhacungcapidncc"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieunhapkho = qlchdgraniteContext;

			return View(phieunhapkho);
		}


		public IActionResult Detaildathanhtoan(int? id)
		{
			if (id == null || _context.Phieunhapkhos == null)
			{
				return NotFound();
			}

			Phieunhapkho phieunhapkho = _context.Phieunhapkhos.Include(x => x.Chitietnhaps).Where(a => a.Idpnk == id).FirstOrDefault();


			if (phieunhapkho == null)
			{
				return NotFound();
			}

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhacungcapidncc"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieunhapkho = qlchdgraniteContext;

			return View(phieunhapkho);
		}

		private string TaoSoPhieu()
		{

			string ngayThangNam = DateTime.Now.ToString("ddMMyy");

				var phieuCu = _context.Phieunhapkhos.OrderByDescending(x => x.Sopnk).FirstOrDefault(p => p.Sopnk.StartsWith($"NK{ngayThangNam}"));

				if (phieuCu != null)
				{
					string soPhieuCu = phieuCu.Sopnk.Substring(8);
					int soPhieuMoi = int.Parse(soPhieuCu) + 1;
					return $"NK{ngayThangNam}{soPhieuMoi:D4}";
				}
				else
				{
					return $"NK{ngayThangNam}0001";
				}
			}

		// GET: Phieunhapkhoes/Create
		public IActionResult Create()
        {
            Phieunhapkho phieunhapkho = new Phieunhapkho();
            phieunhapkho.Chitietnhaps.Add(new Chitietnhap() { Idctn = 1 });

            TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true).ToList();
            TempData["Nhacungcapidncc"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
            TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();
            //TempData["Trangthaiidtt"] = _context.Trangthais.Where(x => x.Active == true).ToList();

            string Sophieutudong = TaoSoPhieu();
			ViewData["idtd"] = Sophieutudong;

			var qlchdgraniteContext = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieunhapkho = qlchdgraniteContext;

			return View(phieunhapkho);
        }

        // POST: Phieunhapkhoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Phieunhapkho phieunhapkho)
        {
            

			if (ModelState.IsValid)
			{
                phieunhapkho.Chitietnhaps.RemoveAll(n => n.Isdeleted == false || n.Loaithungidloaithung == null);
				//foreach (var item in phieunhapkho.Chitietnhaps)
				//{
				//	var loaithung = _context.Loaithungs.Where(x => x.Active == true && x.Idloaithung == item.Loaithungidloaithung).FirstOrDefault();
				//	loaithung.Sothungcon += item.Sothung;
				//	_context.Update(loaithung);
				//}
				phieunhapkho.Sotiendathanhtoan = 0;
				phieunhapkho.Active=true;
				_context.Add(phieunhapkho);
				_context.SaveChanges();
				TempData["CreateSuccess"] = phieunhapkho.Sopnk;
				return RedirectToAction(nameof(Index));
			}

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhacungcapidncc"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieunhapkho = qlchdgraniteContext;

			string Sophieutudong = TaoSoPhieu();
			ViewData["idtd"] = Sophieutudong;

			TempData["CreateFail"] = phieunhapkho.Sopnk;

			return View(phieunhapkho);
        }

        // GET: Phieunhapkhoes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Phieunhapkhos == null)
            {
                return NotFound();
            }

            Phieunhapkho phieunhapkho = _context.Phieunhapkhos.Include(x => x.Chitietnhaps).Where(a => a.Idpnk == id).FirstOrDefault();
            

            if (phieunhapkho == null)
            {
                return NotFound();
            }

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhacungcapidncc"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieunhapkho = qlchdgraniteContext;

			return View(phieunhapkho);
        }

        // POST: Phieunhapkhoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Phieunhapkho phieunhapkho)
        {
            if (id != phieunhapkho.Idpnk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					var OldToDelete = _context.Chitietnhaps
												  .Where(n => n.Phieunhapkhoidpnk == id)
												  .ToList();

					_context.Chitietnhaps.RemoveRange(OldToDelete);
					phieunhapkho.Chitietnhaps.RemoveAll(n => n.Isdeleted == false || n.Loaithungidloaithung == null);
					phieunhapkho.Sotiendathanhtoan = 0;
					phieunhapkho.Active= true;
					_context.Update(phieunhapkho);
					await _context.SaveChangesAsync();
					//foreach (var item in phieunhapkho.Chitietnhaps)
					//{
					//	var loaithung = _context.Loaithungs.Where(x => x.Active == true && x.Idloaithung == item.Loaithungidloaithung).FirstOrDefault();
					//	var tongsothungnhap = _context.Chitietnhaps.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung).Sum(s => s.Sothung);
					//	var tongsothungxuat = _context.Chitietxuats.Where(x => x.Active == true && x.Loaithungidloaithung == item.Loaithungidloaithung).Sum(s => s.Sothunggiaokh);
					//	var sothungcon = tongsothungnhap - tongsothungxuat;
					//	loaithung.Sothungcon = sothungcon;

					//	_context.Update(loaithung);
					//}
					//await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieunhapkhoExists(phieunhapkho.Idpnk))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

				TempData["EditSuccess"] = phieunhapkho.Sopnk;

				return RedirectToAction(nameof(Index));
            }

			TempData["Loaithungidloaithung"] = _context.Loaithungs.Include(x => x.QuycachidquycachNavigation)
																  .Include(x => x.DagraniteiddaNavigation).Where(x => x.Active == true).ToList();
			TempData["Nhacungcapidncc"] = _context.Nhacungcaps.Where(x => x.Active == true).ToList();
			TempData["Nhanvienidnhanvien"] = _context.Nhanviens.Where(x => x.Active == true).ToList();

			var qlchdgraniteContext = _context.Phieunhapkhos.Include(p => p.NhacungcapidnccNavigation)
														 .Include(p => p.NhanvienidnhanvienNavigation)
														 .Include(p => p.TrangthaiidttNavigation)
														 .Where(x => x.Active == true).ToList();

			ViewBag.Phieunhapkho = qlchdgraniteContext;

			TempData["EditFail"] = phieunhapkho.Sopnk;

			return View(phieunhapkho);
        }

        // GET: Phieunhapkhoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Phieunhapkhos == null)
            {
                return NotFound();
            }

            var phieunhapkho = await _context.Phieunhapkhos
				.Include(p => p.NhacungcapidnccNavigation)
                .Include(p => p.NhanvienidnhanvienNavigation)
                .Include(p => p.TrangthaiidttNavigation)
                .FirstOrDefaultAsync(m => m.Idpnk == id);
            if (phieunhapkho == null)
            {
                return NotFound();
            }

            return View(phieunhapkho);
        }

        // POST: Phieunhapkhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Phieunhapkhos == null)
            {
                return Problem("Entity set 'QlchdgraniteContext.Phieunhapkhos'  is null.");
            }
            var phieunhapkho = await _context.Phieunhapkhos.FindAsync(id);
            if (phieunhapkho != null)
            {
                _context.Phieunhapkhos.Remove(phieunhapkho);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

		

		private bool PhieunhapkhoExists(int id)
        {
          return (_context.Phieunhapkhos?.Any(e => e.Idpnk == id)).GetValueOrDefault();
        }
    }
}
