using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MiniVentas.Models
{
    public partial class VentaDetalle
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public decimal PrecioUnidad { get; set; }
        public int Cantidad { get; set; }
        public decimal SubTotal { get; set; }

        public Producto IdProductoNavigation { get; set; }

        [JsonIgnore]
        public Venta IdVentaNavigation { get; set; }
    }
}
