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
    [Route("api/productos")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly BDMiniVentasContext _context;

        public ProductoController(BDMiniVentasContext context)
        {
            _context = context;
        }

        // GET: api/Producto
        [HttpGet]
        public IEnumerable<Producto> GetProducto()
        {
            return _context.Producto;
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public IActionResult GetProducto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = (
                from product in _context.Producto
                join productCategory in _context.ProductoCategoria
                    on product.Id equals productCategory.IdProducto
                    into productCategories
                where product.Id == id
                select new
                {
                    product.Id,
                    product.Nombre,
                    product.Marca,
                    product.Descripcion,
                    Categorias = (
                        from productCategory in productCategories
                        join category in _context.Categoria
                            on productCategory.IdCategoria equals category.Id
                        select category
                    ),
                    product.Precio,
                    product.Stock,
                    product.FechaRegistro,
                    product.Estado
                }
            ).FirstOrDefault();

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto([FromRoute] int id, [FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> PostProducto([FromBody] Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(producto);
        }

        [HttpDelete("{productoId}/categorias/{categoriaId}")]
        public IActionResult DeleteProductoCategoria([FromRoute] int productoId, [FromRoute] int categoriaId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productoCategoria = (
                from productCategory in _context.ProductoCategoria
                where productCategory.IdProducto == productoId && productCategory.IdCategoria == categoriaId
                select productCategory
            ).FirstOrDefault();

            if (productoCategoria == null)
            {
                return NotFound();
            }

            _context.ProductoCategoria.Remove(productoCategoria);
            _context.SaveChanges();

            return Ok(productoCategoria);
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}