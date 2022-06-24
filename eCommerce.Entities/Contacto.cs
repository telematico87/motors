using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Contacto : BaseEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(100)]
        public string Email { get;  set; }

        [StringLength(100)]
        public string Asunto { get; set; }

        [StringLength(1000)]
        public string Mensaje { get; set; }

        public DateTime Fecha { get; set; }

    }
}
