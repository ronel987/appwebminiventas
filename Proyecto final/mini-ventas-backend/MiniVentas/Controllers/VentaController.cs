using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniVentas.Models;

namespace MiniVentas.Controllers
{
    [Route("api/ventas")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly BDMiniVentasContext _context;

        public VentaController(BDMiniVentasContext context)
        {
            _context = context;
        }

        // GET: api/Venta
        [HttpGet]
        public IEnumerable<object> GetVenta()
        {
            return (
                from sale in _context.Venta
                join saleDetail in _context.VentaDetalle
                    on sale.Id equals saleDetail.IdVenta
                    into salesDetails
                select new
                {
                    sale.Id,
                    empleado = (
                        from employee in _context.Empleado
                        where employee.Id == sale.IdEmpleado
                        select employee
                    ).First(),
                    detalles = (
                        from saleDetail in salesDetails
                        join product in _context.Producto
                            on saleDetail.IdProducto equals product.Id
                        select new
                        {
                            saleDetail.Id,
                            producto = product,
                            saleDetail.PrecioUnidad,
                            saleDetail.Cantidad,
                            saleDetail.SubTotal
                        }
                    ),
                    sale.Total,
                    sale.FechaRegistro,
                    sale.Estado,
                }
            );
        }

        // GET: api/Venta/5
        [HttpGet("{id}")]
        public IActionResult GetVenta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var venta = (
                from sale in _context.Venta
                join saleDetail in _context.VentaDetalle
                    on sale.Id equals saleDetail.IdVenta
                    into salesDetails
                where sale.Id == id
                select new
                {
                    sale.Id,
                    empleado = (
                        from employee in _context.Empleado
                        where employee.Id == sale.IdEmpleado
                        select employee
                    ).First(),
                    detalles = (
                        from saleDetail in salesDetails
                        join product in _context.Producto
                            on saleDetail.IdProducto equals product.Id
                        select new
                        {
                            saleDetail.Id,
                            producto = product,
                            saleDetail.PrecioUnidad,
                            saleDetail.Cantidad,
                            saleDetail.SubTotal
                        }
                    ),
                    sale.Total,
                    sale.FechaRegistro,
                    sale.Estado,
                }
            ).FirstOrDefault();

            if (venta == null)
            {
                return NotFound();
            }

            return Ok(venta);
        }

        // PUT: api/Venta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenta([FromRoute] int id, [FromBody] Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venta.Id)
            {
                return BadRequest();
            }

            _context.Entry(venta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Venta
        [HttpPost]
        public async Task<IActionResult> PostVenta([FromBody] Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Venta.Add(venta);
            await _context.SaveChangesAsync();

            return Ok(venta);
        }

        // DELETE: api/Venta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var venta = await _context.Venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            _context.Venta.Remove(venta);
            await _context.SaveChangesAsync();

            return Ok(venta);
        }

        private bool VentaExists(int id)
        {
            return _context.Venta.Any(e => e.Id == id);
        }
    }
}