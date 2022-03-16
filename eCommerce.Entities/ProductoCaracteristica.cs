using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class ProductoCaracteristica
    {
        public Motor motor { get; set; }
        public Frenos frenos { get; set; }
    }

    public class Motor
    {
        public string Cilindrada { get; set; }
        public string NroCilindrada { get; set; }
        public string Potencia { get; set; }
    }

    public class Frenos
    {
        public string FrenoDelantero { get; set; }
        public string FrenoTrasero { get; set; }
    }
}
