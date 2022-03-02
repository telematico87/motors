using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Modelo : BaseEntity
    {
        public string Description { get; set; }
    }

    public class ProductModelo : BaseEntity
    {
        public int ModeloID { get; set; }
        public int ProductID { get; set; }
        public virtual Modelo Modelo { get; set; }
        public virtual Product Product { get; set; }
    }
}
