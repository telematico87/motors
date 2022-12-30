using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities.Request
{
    public class ProductRequest
    {
        public int IDProducto { get; set; }
        public int IDCatalogo { get; set; }
        public string Nombre { get; set; }
        public string Resumen { get; set; }
        public string Descripcion { get; set; }                
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public int TipoMoneda { get; set; }
        public decimal Precio { get; set; }
        public decimal Descuento { get; set; }
        public string EtiquetaOferta { get; set; }
        public decimal Costo { get; set; }
        public string SKU { get; set; }
        public string Tags { get; set; }
        public string Proveedor { get; set; }
        public int Stock { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
    }
}
