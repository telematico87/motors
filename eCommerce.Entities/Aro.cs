using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Aro : BaseEntity
    {
        public string Description { get; set; }
    }

    public class ProductsAro : BaseEntity
    {
        public int AroID { get; set; }
        public int ProductID { get; set; }

        public virtual Aro Aro { get; set; }
        public virtual Product Product { get; set; }
    }
}
