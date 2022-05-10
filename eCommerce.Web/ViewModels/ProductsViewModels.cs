using eCommerce.Entities;
using eCommerce.Entities.CustomEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.ViewModels
{
    public class ProductsViewModel : PageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Category SelectedCategory { get; set; }
        public List<Category> SearchedCategories { get; set; }

        public string SearchTerm { get; set; }
        public string SortBy { get; set; }

        public Pager Pager { get; set; }
        public int PageNo { get; set; }
        public int? PageSize { get; set; }

        public List<string> Brands { get; set; }
        public List<string> Colors { get; set; }
        public List<string> Sizes { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }

    public class FeaturedProductsViewModel : PageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
    public class RelatedProductsViewModel : FeaturedProductsViewModel
    {
        public Category Category { get; set; }
        public bool IsFeaturedProductsOnly { get; set; }
    }

    public class ProductsByFeaturedCategoriesViewModel : PageViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }

    public class FeaturedCategoriesAndProductsSectionsViewModel : PageViewModel
    {
        public List<Category> FeaturedCategories { get; set; }
        public List<Product> Products { get; set; }
    }
    public class CartProductsViewModel
    {
        public List<int> ProductIDs { get; set; }
        public List<Product> Products { get; set; }
    }

    public class ProductDetailsViewModel : CommentablePageViewModel
    {
        public Product Product { get; set; }
        public ProductRating Rating { get; internal set; }
    }

    public class ProductDetalleViewModel : PageViewModel
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int CategoryID { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Cost { get; set; }
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
        public int ThumbnailPicture { get; set; }
        public List<ProductPicture> ProductPicturesList { get; set; }

        public List<Category> Categories { get; set; }
        public List<Color> Colors { get; set; }
        public List<ProductColor> ProductColors { get; set; }

        public ProductoCaracteristica ProductoCaracteristica { get; set; }

        public int CatalogoID { get; set; }

        public List<Catalogo> Catalogos { get; set; }

        public int MarcaID { get; set; }
        public int TipoMoneda { get; set; }

        public List<Marca> Marcas { get; set; }
        public List<TablaMaster> TipoMonedas { get; set; }
    }


}