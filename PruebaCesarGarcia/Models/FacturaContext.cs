using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaCesarGarcia.Models
{
    public class FacturaContext : DbContext
    {
        public FacturaContext(DbContextOptions<FacturaContext> options)
    : base(options)
        {
        }

        public DbSet<Factura> facturas { get; set; }
        public DbSet<LineaFactura> lineasFactura { get; set; }
        public DbSet<Producto> productos { get; set; }

    }
}
