using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Talla : BaseEntity
    {
        public string Description { get; set; }
        public string Barcode { get; set; }
        public int Orden { get; set; }
       
    }

    public class ProductTalla : BaseEntity
    {
        public int TallaID { get; set; }
        public int ProductID { get; set; }
        public decimal PrecioTalla { get; set; }
        public virtual Talla Talla { get; set; }
        public virtual Product Product { get; set; }
    }
}
