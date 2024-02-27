using CUAHANGBANDAGRANITE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CUAHANGBANDAGRANITE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		private readonly QlchdgraniteContext _context;

		public HomeController(ILogger<HomeController> logger, QlchdgraniteContext context)
        {
            _logger = logger;
			_context = context;
		}

		public List<object> Chart()
		{
			List<object> data = new List<object>();
			var ten = _context.Loaithungs
				.Where(x => x.Active == true)
				.GroupBy(x => new { x.DagraniteiddaNavigation.Tenda })
				.OrderByDescending(g => g.Sum(x => x.Sothungcon))
				.Take(3)
				.Select(g => g.Key.Tenda)
				.ToList();
			var soLuongTon = _context.Loaithungs
				.Where(x => x.Active == true)
				.GroupBy(x => new { x.Tenloaithung })
				.OrderByDescending(g => g.Sum(x => x.Sothungcon))
				.Take(4)
				.Select(g => g.Sum(x => x.Sothungcon))
				.ToList();
			var tenloaithung = _context.Chitietxuats
				.Where(x => x.Active == true)
				.GroupBy(x => new { x.LoaithungidloaithungNavigation.Tenloaithung })
				.OrderByDescending(g => g.Sum(x => x.Sothunggiaokh))
				.Take(8)
				.Select(g => g.Key.Tenloaithung)
				.ToList();
			var soluongxuat = _context.Chitietxuats
				.Where(x => x.Active == true)
				.GroupBy(x => new { x.Loaithungidloaithung })
				.OrderByDescending(g => g.Sum(x => x.Sothunggiaokh))
				.Take(8)
				.Select(g => g.Sum(x => x.Sothunggiaokh))
				.ToList();

			data.Add(ten);
			data.Add(soLuongTon);
			data.Add(tenloaithung);
			data.Add(soluongxuat);
			return data;
		}

		public IActionResult Index()
        {
			var hangton = _context.Loaithungs.Where(x => x.Active == true).Sum(s => s.Sothungcon);
			ViewData["hangton"] = hangton;

			var soluongban = _context.Chitietxuats.Where(x => x.Active == true).Sum(s => s.Sothunggiaokh);
			ViewData["soluongban"] = soluongban;

			var thanhtoan = _context.Phieuthanhtoans.Where(x => x.Active == true).Sum(s => s.Sotienthanhtoan);
			ViewData["thanhtoan"] = thanhtoan;

			var thutien = _context.Phieuthutiens.Where(x => x.Active == true).Sum(s => s.Sotienthu);
			ViewData["thutien"] = thutien;

			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}