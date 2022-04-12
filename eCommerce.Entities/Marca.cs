using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Marca : BaseEntity
    {
        public string Descripcion { get; set; }
        public string Resumen { get; set; }
        public string URL { get; set; }
        public int CatalogoID { get; set; }

        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }

    }
}
