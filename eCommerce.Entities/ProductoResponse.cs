using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class ProductoResponse
    {
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }


        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Cost { get; set; }
        public bool isFeatured { get; set; }
        public int ThumbnailPictureID { get; set; }

        public string SKU { get; set; }
        public string Tags { get; set; }
        public string Barcode { get; set; }
        public string Supplier { get; set; }

        public int StockQuantity { get; set; }

        public virtual List<ProductPicture> ProductPictures { get; set; }

        public virtual List<ProductRecord> ProductRecords { get; set; }

        public string Caracteristica { get; set; }

        public ProductoCaracteristica productoCaracteristica { get; set;}
    }
}
