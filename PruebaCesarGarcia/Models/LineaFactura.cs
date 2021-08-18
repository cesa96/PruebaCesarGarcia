using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaCesarGarcia.Models
{
    public class LineaFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 idLinea { get; set; }
        public Int64 idProducto { get; set; }
        public Int64 idFactura { get; set; }
        public Double cantidad { get; set; }
        [ForeignKey("idProducto")]
        public Producto producto { get; set; }

    }
}
