using eCommerce.Entities;
using eCommerce.Entities.Response;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class ProductsListingViewModel : PageViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        public int? CategoryID { get; set; }
        public string SearchTerm { get; set; }
        public bool? ShowOnlyLowStock { get; set; }

        public Pager Pager { get; set; }

        public string ColorID { get; set; }
        public string TallaID { get; set; }
        public string ModeloID { get; set; }
        public string MedidaID { get; set; }
        public string AroID { get; set; }
        public string LitroID { get; set; }

        //Lista de Colores
        public List<Color> Colors { get; set; }
        public List<Talla> Tallas { get; set; }
        public List<Modelo> Modelos { get; set; }
        public List<Medida> Medidas { get; set; }
        public List<Aro> Aros { get; set; }
        public List<Litros> Litros { get; set; }
    }
    
    public class ProductActionViewModel : PageViewModel
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public string PriceStr { get; set; }
        public decimal? Discount { get; set; }
        public string DiscountStr { get; set; }
        public decimal? Cost { get; set; }
        public string CostStr { get; set; }
        public bool isFeatured { get; set; }
        public bool InActive { get; set; }

        public int StockQuantity { get; set; }

        public int ProductRecordID { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public List<ProductSpecification> ProductSpecifications { get; set; }

        public string SKU { get; set; }
        public string Tags { get; set; }
        public string Barcode { get; set; }
        public string Supplier { get; set; }

        public bool TipoProducto { get; set; }
        public string ProductPictures { get; set; }
        public string ProductColorsPicture { get; set; }
        public string ProductColorsId { get; set; }
        public string ProductColorsStock { get; set; }
        public string ProductStockSize { get; set; }
        public string ProductStockColor { get; set; }
        public string ProductStockSKU { get; set; }
        public string ProductStockQuantity { get; set; }
        public string ProductStockPrice { get; set; }

        public int ThumbnailPicture { get; set; }
        public string EtiquetaOferta { get; set; }
        public string EtiquetaSoat { get; set; }
        public bool IncluyeSoat { get; set; }
        public List<ProductPicture> ProductPicturesList { get; set; }

        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductStockResponse> ProductStocks { get; set; }
        public bool ExisteProductStocks { get; set; }

       public ProductoCaracteristica ProductoCaracteristica { get; set; }

        public int CatalogoID { get; set; }

        public List<Catalogo> Catalogos { get; set; }

        public int MarcaID { get; set; }
        public int TipoMoneda { get; set; }

        public List<Marca> Marcas { get; set; }
        public List<TablaMaster> TipoMonedas{ get; set; }
      
    }
}