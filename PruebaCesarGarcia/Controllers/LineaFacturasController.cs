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
    public class LineaFacturasController : ControllerBase
    {
        private readonly FacturaContext _context;

        public LineaFacturasController(FacturaContext context)
        {
            _context = context;
        }

        // GET: api/LineaFacturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineaFactura>>> GetlineasFactura()
        {
            return await _context.lineasFactura.ToListAsync();
        }

        // GET: api/LineaFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineaFactura>> GetLineaFactura(long id)
        {
            var lineaFactura = await _context.lineasFactura.FindAsync(id);

            if (lineaFactura == null)
            {
                return NotFound();
            }

            return lineaFactura;
        }

        // PUT: api/LineaFacturas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineaFactura(long id, LineaFactura lineaFactura)
        {
            if (id != lineaFactura.idLinea)
            {
                return BadRequest();
            }

            _context.Entry(lineaFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineaFacturaExists(id))
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

        // POST: api/LineaFacturas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<LineaFactura>> PostLineaFactura(LineaFactura lineaFactura)
        {
            _context.lineasFactura.Add(lineaFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLineaFactura", new { id = lineaFactura.idLinea }, lineaFactura);
        }

        // DELETE: api/LineaFacturas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LineaFactura>> DeleteLineaFactura(long id)
        {
            var lineaFactura = await _context.lineasFactura.FindAsync(id);
            if (lineaFactura == null)
            {
                return NotFound();
            }

            _context.lineasFactura.Remove(lineaFactura);
            await _context.SaveChangesAsync();

            return lineaFactura;
        }

        private bool LineaFacturaExists(long id)
        {
            return _context.lineasFactura.Any(e => e.idLinea == id);
        }
    }
}
