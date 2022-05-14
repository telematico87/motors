using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Enums;
using eCommerce.Shared.Extensions;
using eCommerce.Shared.Helpers;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.Controllers
{
    public class ProductsController : PublicBaseController
    {
        //[OutputCache(Duration = 1000, VaryByParam = "productID;pageSize")]
        public ActionResult FeaturedProducts(int? productID, int pageSize = (int)RecordSizeEnums.Size10, bool isForHomePage = false)
        {
            FeaturedProductsViewModel model = new FeaturedProductsViewModel
            {
                Products = ProductsService.Instance.SearchFeaturedProducts(recordSize: pageSize, excludeProductIDs: new List<int>() { productID.HasValue ? productID.Value : 0 })
            };

            if (isForHomePage)
            {
                return PartialView("_FeaturedProductsHomePage", model);
            }
            else
            {
                return PartialView("_FeaturedProducts", model);
            }
        }

        public ActionResult RecentProducts(int? productID, int pageSize = 0)
        {
            if (pageSize == 0)
            {
                pageSize = (int)RecordSizeEnums.Size10;
            }

            FeaturedProductsViewModel model = new FeaturedProductsViewModel
            {
                Products = ProductsService.Instance.SearchProducts(null, null, null, null, null, 1, pageSize, activeOnly: true, out int count, stockCheckCount: null)
            };

            return PartialView("_RecentProducts", model);
        }

        public ActionResult RelatedProducts(int categoryID, int recordSize = (int)RecordSizeEnums.Size6)
        {
            RelatedProductsViewModel model = new RelatedProductsViewModel
            {
                Products = ProductsService.Instance.SearchProducts(new List<int>() { categoryID }, null, null, null, null, 1, recordSize, activeOnly: true, out int count, stockCheckCount: null)
            };

            if (model.Products == null || model.Products.Count < (int)RecordSizeEnums.Size6)
            {
                //the realted products are less than the specfified RelatedProductsRecordsSize, so instead show featured products
                model.Products = ProductsService.Instance.SearchFeaturedProducts(recordSize);
                model.IsFeaturedProductsOnly = true;
            }

            return PartialView("_RelatedProducts", model);
        }

        [HttpGet]
        public ActionResult DetalleBm3(int? ID, string category)
        {
            ProductDetalleViewModel model = new ProductDetalleViewModel();

            if (ID.HasValue)
            {
                var product = ProductsService.Instance.GetProductResponseByID(ID.Value, activeOnly: false);

                if (product == null) return HttpNotFound();

                var currentLanguageRecord = product.ProductRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);

                currentLanguageRecord = currentLanguageRecord ?? new ProductRecord();

                model.ProductID = product.ID;
                model.CategoryID = product.CategoryID;
                model.Price = product.Price;
                model.Discount = product.Discount;
                model.Cost = product.Cost;
                model.isFeatured = product.isFeatured;
                model.StockQuantity = product.StockQuantity;
                model.ProductPicturesList = product.ProductPictures;
                model.ThumbnailPicture = product.ThumbnailPictureID;
                model.SKU = product.SKU;
                model.Barcode = product.Barcode;
                model.Tags = product.Tags;
                model.Supplier = product.Supplier;
                model.InActive = !product.IsActive;
                model.MarcaID = product.MarcaID;
                model.CatalogoID = product.CatalogoID;
                model.TipoMoneda = product.TipoMoneda;

                model.ProductRecordID = currentLanguageRecord.ID;
                model.Name = currentLanguageRecord.Name;
                model.Summary = currentLanguageRecord.Summary;
                model.Description = currentLanguageRecord.Description;

                model.ProductSpecifications = currentLanguageRecord.ProductSpecifications;
                model.ProductoCaracteristica = product.ProductoCaracteristica;
                model.TipoProducto = product.TipoProducto;
                model.ProductColors = ProductColorService.Instance.SearchProductColorByProductId(product.ID);
            }

            model.Categories = CategoriesService.Instance.GetCategories();
            model.Colors = ColorService.Instance.GetAllColors();
            model.Catalogos = CatalogoService.Instance.GetCatalogos();
            model.Marcas = MarcaService.Instance.ListarMarca();
            model.TipoMonedas = TablaMasterService.Instance.GetTablaMasterByTipoTabla("TIPO_MONEDA");

            return View(model);


        }


        //[HttpGet]
        //public ActionResult DetalleBm3(int ID, string category)
        //{
        //    try
        //    {
        //        ProductDetailsViewModel model = new ProductDetailsViewModel
        //        {
        //            Product = ProductsService.Instance.GetProductByID(ID, activeOnly: false)
        //        };

        //        if (model.Product == null || !model.Product.Category.SanitizedName.ToLower().Equals(category))
        //            return HttpNotFound();

        //        model.Rating = CommentsService.Instance.GetProductRating(model.Product.ID);

        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }


        //}


    }
}
