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
        public int Stock_TallaS { get; set; }
        public int Stock_TallaM { get; set; }
        public int Stock_TallaL { get; set; }
        public int Stock_TallaXL { get; set; }
        public int Stock_TallaXXL { get; set; }
        public string Esp_Titulo1 { get; set; }
        public string Esp_Valor1 { get; set; }
        public string Esp_Titulo2 { get; set; }
        public string Esp_Valor2 { get; set; }
        public string Esp_Titulo3 { get; set; }
        public string Esp_Valor3 { get; set; }
        public string Esp_Titulo4 { get; set; }
        public string Esp_Valor4 { get; set; }
        public string Esp_Titulo5 { get; set; }
        public string Esp_Valor5 { get; set; }        
        public string Especificaciones { get; set; }
        public string SKU_S { get; set; }
        public string SKU_M { get; set; }
        public string SKU_L { get; set; }
        public string SKU_XL { get; set; }
        public string SKU_XXL { get; set; }
        
    }
}
