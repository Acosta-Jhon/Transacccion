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
        /*------------------INDEX-------------------------*/
        [HttpPost]
        public IActionResult Index(int cantidad)
        {
            String resultado = this.repo.Traspaso(cantidad, 1, 2);
            return RedirectToAction("Index", new { result = resultado });
        }

        /*------------------CREATE----------------------------*/
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

        /*--------------------------EDIT---------------------------*/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varcuentas = await _context.Cuentas.SingleOrDefaultAsync(m=> m.Id == id);
            if (varcuentas == null)
            {
                return NotFound();
            }
            return View(varcuentas);
        }

        [HttpPost]
        
        public IActionResult Edit(int id, [Bind("Id,Nombre,Dinero,Apellido,Cedula,Digitos")] Cuentas cuentas)
        {
            if (id != cuentas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "SP_UPDATE_CUENTAS";
                    cmd.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar, 100).Value = cuentas.Nombre;
                    cmd.Parameters.Add("@dinero", System.Data.SqlDbType.Real).Value = cuentas.Dinero;
                    cmd.Parameters.Add("@apellido", System.Data.SqlDbType.NVarChar, 100).Value = cuentas.Apellido;
                    cmd.Parameters.Add("@cedula", System.Data.SqlDbType.NVarChar, 10).Value = cuentas.Cedula;
                    cmd.Parameters.Add("@digitos", System.Data.SqlDbType.Int).Value = cuentas.Digitos;
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    /* _context.Update(categoria);
                     await _context.SaveChangesAsync();*/
                }
                catch (Exception er)
                {
                    Console.WriteLine(er.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cuentas);
        }
        /*--------------------------DELETE-----------------------------*/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varcuenta = await _context.Cuentas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (varcuenta == null)
            {
                return NotFound();
            }

            return View(varcuenta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "SP_ELIMINAR_CUENTAS";
            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

            cmd.ExecuteNonQuery();
            conn.Close();
            /*  var categoria = await _contex.Cuentas.FindAsync(id);
              _context.Categoria.Remove(categoria);
              await _context.SaveChangesAsync();*/

            return RedirectToAction(nameof(Index));
        }
        /*----------------------DETAILS---------------------------*/

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var varCuenta = await _context.Cuentas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (varCuenta == null)
            {
                return NotFound();
            }

            return View(varCuenta);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
