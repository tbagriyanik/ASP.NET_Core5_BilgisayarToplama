using BilisimBilgisayar.Data;
using BilisimBilgisayar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BilisimBilgisayar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var say1 = _context.Urun;
            var say2 = _context.Kategori;
            ViewBag.urunSayisi = say1.Count();
            ViewBag.kategoriSayisi = say2.Count();

            var datalar = GetirSonKayitlar(4);
            return View(datalar);
        }

        private object GetirSonKayitlar(int sayi)
        {
            var last4Products = _context.Urun.OrderByDescending(p => p.ID).Take(sayi);

            return last4Products;
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
