using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Litros : BaseEntity
    {
        public string Description { get; set; }
    }
    public class ProductLitros : BaseEntity
    {
        public int LitrosID { get; set; }
        public int ProductID { get; set; }
        public virtual Litros Litros { get; set; }
        public virtual Product Product { get; set; }

    }
}
