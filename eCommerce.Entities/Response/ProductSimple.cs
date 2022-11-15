using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities.Response
{
    public class ProductSimple
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Money { get; set; }
        public decimal  Price { get; set; }
    }
}
