using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class ProductStock : BaseEntity
    {
        public int ProductID { get; set; }
        public int TallaID { get; set; }
        public int ColorID { get; set; }
        public int Stock { get; set; }

        [StringLength(16)]       
        public string SKU { get; set; }
        public double Precio { get; set; }

    }
}
