using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class TablaMaster : BaseEntity
    {

        public string TipoTabla { get; set; }  
        public int Codigo { get; set; }  
        public string Name { get; set; } 
        public string Value { get; set; }

    }
}
