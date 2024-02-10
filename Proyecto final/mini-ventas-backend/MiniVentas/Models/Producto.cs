using System;
using System.Collections.Generic;

namespace MiniVentas.Models
{
    public partial class Producto
    {
        public Producto()
        {
            ProductoCategoria = new HashSet<ProductoCategoria>();
            VentaDetalle = new HashSet<VentaDetalle>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool? Estado { get; set; }

        public ICollection<ProductoCategoria> ProductoCategoria { get; set; }
        public ICollection<VentaDetalle> VentaDetalle { get; set; }
    }
}
