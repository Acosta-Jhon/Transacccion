using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tranferir.Models
{
    public interface ICuentasContext
    {
        DbSet<Cuentas> Cuentas { get; set; }

        //permite acceder a los triggers
        DatabaseFacade Database { get; }
        void GuardarCambios();
    }
}
