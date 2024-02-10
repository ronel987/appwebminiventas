using System;
using System.Collections.Generic;

namespace MiniVentas.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            Venta = new HashSet<Venta>();
        }

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Alias { get; set; }
        public string Clave { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool? Estado { get; set; }

        public ICollection<Venta> Venta { get; set; }
    }
}
