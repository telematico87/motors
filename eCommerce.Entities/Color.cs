using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Color : BaseEntity
    {

        public string Description { get; set; }

    }

    public class ProductColor : BaseEntity
    {
        public int ColorID { get; set; }
        public int ProductID { get; set; }
        public virtual Color Color { get; set; }
        public virtual Product Product { get; set; }

    }
}
