using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class TipoCambio : BaseEntity
    {
        public double Venta { get; set; }
        public double Compra { get; set; }
        public DateTime Fecha { get; set; }
    }
}
