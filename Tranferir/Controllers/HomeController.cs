using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tranferir.Models;
using Tranferir.Repositories;

namespace Tranferir.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICuentasContext _context;

        IRepositoryCuentas repo;

        public HomeController(IRepositoryCuentas repo,ICuentasContext context)
        {
            this.repo = repo;
            this._context = context;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "SP_INSERT_CUENTAS";
                cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 100).Value = cuentas.Nombre;
                cmd.Parameters.Add("@dinero", System.Data.SqlDbType.Real).Value = cuentas.Dinero;
                cmd.Parameters.Add("@apellido", System.Data.SqlDbType.NVarChar, 100).Value = cuentas.Apellido;
                cmd.Parameters.Add("@cedula", System.Data.SqlDbType.NVarChar, 10).Value = cuentas.Cedula;
                cmd.Parameters.Add("@digitos", System.Data.SqlDbType.Int).Value = cuentas.Digitos;
                cmd.ExecuteNonQuery();
                conn.Close();
              /*  repo.Add(categoria);
                await repo.SaveChangesAsync();*/
                return RedirectToAction(nameof(Index));
            }
            return View(cuentas);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
