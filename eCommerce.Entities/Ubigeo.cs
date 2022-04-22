using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Ubigeo : BaseEntity
    {
         
        public string CodUbigeo { get; set; }
        public string NombreUbigeo { get; set; }
        public string CodPais { get; set; }
        public string CodDep { get; set; }
        public string CodProv { get; set; }
        public string CodDist { get; set; }
        public string EstadoUbigeo { get; set; }
        
    }
}
