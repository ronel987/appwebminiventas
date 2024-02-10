using System;
using System.Collections.Generic;

namespace MiniVentas.Models
{
    public partial class Venta
    {
        public Venta()
        {
            VentaDetalle = new HashSet<VentaDetalle>();
        }

        public int Id { get; set; }
        public string IdEmpleado { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool? Estado { get; set; }

        public Empleado IdEmpleadoNavigation { get; set; }
        public ICollection<VentaDetalle> VentaDetalle { get; set; }
    }
}
