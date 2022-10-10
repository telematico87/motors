using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class LogSystem : BaseEntity
    {        
        public string LogDescription { get; set; }
        public DateTime LogDate { get; set; }

    }
}
