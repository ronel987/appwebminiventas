using System;
using System.Collections.Generic;

namespace MiniVentas.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            ProductoCategoria = new HashSet<ProductoCategoria>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }

        public ICollection<ProductoCategoria> ProductoCategoria { get; set; }
    }
}
