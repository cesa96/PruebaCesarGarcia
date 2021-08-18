using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaCesarGarcia.Models;

namespace PruebaCesarGarcia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturaContext _context;

        public FacturasController(FacturaContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> Getfacturas()
        {
            return await _context.facturas.ToListAsync();
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(long id)
        {
            var factura = await _context.facturas.FindAsync(id);

            if (factura == null)
            {
                return NotFound();
            }
            _context.Entry(factura).Collection(p => p.lineas).Load();
            foreach (LineaFactura lineaFactura in factura.lineas)
            {
                //lineaFactura.producto = await _context.productos.FindAsync(lineaFactura.idProducto);
                _context.Entry(lineaFactura).Reference("producto").Load();
            }
            return factura;
        }

        // PUT: api/Facturas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(long id, Factura factura)
        {
            if (id != factura.idFactura)
            {
                return BadRequest();
            }

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST: api/Facturas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
            _context.facturas.Add(factura);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetFactura", new { id = factura.idFactura }, factura);
            return CreatedAtAction(nameof(GetFactura), new { id = factura.idFactura }, factura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Factura>> DeleteFactura(long id)
        {
            var factura = await _context.facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return factura;
        }

        private bool FacturaExists(long id)
        {
            return _context.facturas.Any(e => e.idFactura == id);
        }
    }
}
