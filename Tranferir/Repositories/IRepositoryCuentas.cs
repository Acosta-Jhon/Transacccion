using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tranferir.Models;

namespace Tranferir.Repositories
{
    public interface IRepositoryCuentas
    {
        List<Cuentas> GetCuentas();
        String Traspaso(float cantidad, int ida, int idb);
    }
}
