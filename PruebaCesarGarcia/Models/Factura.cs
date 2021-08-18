using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaCesarGarcia.Models
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 idFactura { get; set; }
        public String  numeroFactura { get; set; }
        public DateTime fechaFactura { get; set; }
        public String tipoPago { get; set; }
        public String documentoCliente { get; set; }
        public String nombreCliente { get; set; }
        public Double subTotal { get; set; }
        public Double descuento { get; set; }
        public Double iva { get; set; }
        public Double totalDescuento { get; set; }
        public Double totalImpuesto { get; set; }
        public Double total { get; set; }
        [ForeignKey("idFactura")]   
        public List<LineaFactura> lineas { get; set; }

    }
}
