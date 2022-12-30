using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class CatalogoMarca : BaseEntity
    {
        public int CatalogoId { get; set; }
        public int MarcaId { get; set; }
    }
}
