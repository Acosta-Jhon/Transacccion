using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tranferir.Models;

namespace Tranferir.Repositories
{
    public class RepositoryCuentas : IRepositoryCuentas
    {
        ICuentasContext context;

        public RepositoryCuentas(ICuentasContext context)
        {
            this.context = context;
        }

        public List<Cuentas> GetCuentas()
        {
            return this.context.Cuentas.ToList();
        }
        public Cuentas GetCuentas(int id)
        {
            return this.context.Cuentas.Where(z => z.Id == id).SingleOrDefault();
        }

        public String Traspaso(float cantidad, int ida, int idb)
        {
            String resultado = "";

            using (IDbContextTransaction transaction = this.context.Database.BeginTransaction())
            {
                /* transaction.Open();

                 SqlTransaction sqlTran = transaction.BeginTransaction();

                 SqlCommand command = transaction.CreateCommand();
                 command.Transaction = sqlTran;*/

                try
                {
                    Cuentas cuentas1 = GetCuentas(ida);
                    cuentas1.Dinero -= cantidad;
                    this.context.GuardarCambios();

                    Cuentas cuenta2 = GetCuentas(idb);
                    cuenta2.Dinero += cantidad;
                    this.context.GuardarCambios();

                    transaction.Commit();
                    resultado = "Transaccion Exitosa ✔️";

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    resultado = "Trasaccion Fallida ❌";
                    Console.WriteLine(e.Message);
                }
                return resultado;
            }
        }
    }
}
