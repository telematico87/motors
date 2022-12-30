using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class CatalogoCategoria : BaseEntity
    {
        public int CatalogoId { get; set; }
        public int CategoriaId { get; set; }
    }
}
