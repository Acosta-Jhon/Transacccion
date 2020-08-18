using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tranferir.Models
{
    public partial class Cuentas
    {

        public int Id { get; set; }
        public int Digitos { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public float Dinero { get; set; }

    }

}
