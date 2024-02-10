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
    [Route("api/ventas-detalles")]
    [ApiController]
    public class VentaDetalleController : ControllerBase
    {
        private readonly BDMiniVentasContext _context;

        public VentaDetalleController(BDMiniVentasContext context)
        {
            _context = context;
        }

        // GET: api/VentaDetalle
        [HttpGet]
        public IEnumerable<VentaDetalle> GetVentaDetalle()
        {
            return _context.VentaDetalle;
        }

        // GET: api/VentaDetalle/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVentaDetalle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ventaDetalle = await _context.VentaDetalle.FindAsync(id);

            if (ventaDetalle == null)
            {
                return NotFound();
            }

            return Ok(ventaDetalle);
        }

        // PUT: api/VentaDetalle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentaDetalle([FromRoute] int id, [FromBody] VentaDetalle ventaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ventaDetalle.Id)
            {
                return BadRequest();
            }

            _context.Entry(ventaDetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaDetalleExists(id))
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

        // POST: api/VentaDetalle
        [HttpPost]
        public async Task<IActionResult> PostVentaDetalle([FromBody] VentaDetalle ventaDetalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.VentaDetalle.Add(ventaDetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVentaDetalle", new { id = ventaDetalle.Id }, ventaDetalle);
        }

        // DELETE: api/VentaDetalle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentaDetalle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ventaDetalle = await _context.VentaDetalle.FindAsync(id);
            if (ventaDetalle == null)
            {
                return NotFound();
            }

            _context.VentaDetalle.Remove(ventaDetalle);
            await _context.SaveChangesAsync();

            return Ok(ventaDetalle);
        }

        private bool VentaDetalleExists(int id)
        {
            return _context.VentaDetalle.Any(e => e.Id == id);
        }
    }
}