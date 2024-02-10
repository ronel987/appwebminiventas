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
    [Route("api/productos-categorias")]
    [ApiController]
    public class ProductoCategoriaController : ControllerBase
    {
        private readonly BDMiniVentasContext _context;

        public ProductoCategoriaController(BDMiniVentasContext context)
        {
            _context = context;
        }

        // GET: api/ProductoCategoria
        [HttpGet]
        public IEnumerable<ProductoCategoria> GetProductoCategoria()
        {
            return _context.ProductoCategoria;
        }

        // GET: api/ProductoCategoria/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductoCategoria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productoCategoria = await _context.ProductoCategoria.FindAsync(id);

            if (productoCategoria == null)
            {
                return NotFound();
            }

            return Ok(productoCategoria);
        }

        // PUT: api/ProductoCategoria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoCategoria([FromRoute] int id, [FromBody] ProductoCategoria productoCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productoCategoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(productoCategoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoCategoriaExists(id))
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

        // POST: api/ProductoCategoria
        [HttpPost]
        public async Task<IActionResult> PostProductoCategoria([FromBody] IEnumerable<ProductoCategoria> productosCategorias)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ProductoCategoria.AddRange(productosCategorias);
            await _context.SaveChangesAsync();

            return Ok(productosCategorias);
        }

        // DELETE: api/ProductoCategoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoCategoria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productoCategoria = await _context.ProductoCategoria.FindAsync(id);
            if (productoCategoria == null)
            {
                return NotFound();
            }

            _context.ProductoCategoria.Remove(productoCategoria);
            await _context.SaveChangesAsync();

            return Ok(productoCategoria);
        }

        private bool ProductoCategoriaExists(int id)
        {
            return _context.ProductoCategoria.Any(e => e.Id == id);
        }
    }
}