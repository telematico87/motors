using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Medida : BaseEntity
    {
        public string Description { get; set; }
    }

    public class ProductMedida : BaseEntity
    {
        public int MedidaID { get; set; }
        public int ProductID { get; set; }
        public virtual Medida Medida { get; set; }
        public virtual Product Product { get; set; }
    }
}
