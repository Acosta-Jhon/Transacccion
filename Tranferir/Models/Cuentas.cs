using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tranferir.Models
{
    public partial class Cuentas
    {

        public int Id { get; set; }
        [Required]
        public int Digitos { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public float Dinero { get; set; }

    }

}
