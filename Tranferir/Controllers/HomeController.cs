using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tranferir.Models;
using Tranferir.Repositories;

namespace Tranferir.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryCuentas repo;

        public HomeController(IRepositoryCuentas repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(String? result)
        {
            result?.ToList();
            ViewData["resultado"] = result;
            return View(this.repo.GetCuentas());

            
        }

        [HttpPost]

        public IActionResult Index(int cantidad)
        {
            String resultado = this.repo.Traspaso(cantidad, 1, 2);
            return RedirectToAction("Index", new { result = resultado });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
