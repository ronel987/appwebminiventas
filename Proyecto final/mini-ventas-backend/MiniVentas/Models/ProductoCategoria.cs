using System;
using System.Collections.Generic;

namespace MiniVentas.Models
{
    public partial class ProductoCategoria
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }

        public Categoria IdCategoriaNavigation { get; set; }
        public Producto IdProductoNavigation { get; set; }
    }
}
