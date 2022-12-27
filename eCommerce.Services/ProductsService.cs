using eCommerce.Data;
using eCommerce.Entities;
using eCommerce.Entities.Response;
using eCommerce.Shared.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class ProductsService
    {
        #region Define as Singleton
        private static ProductsService _Instance;

        public static ProductsService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ProductsService();
                }

                return (_Instance);
            }
        }

        private ProductsService()
        {
        }
        #endregion

        public List<Product> GetAllProducts(int? pageNo = 1, int? recordSize = 0)
        {
            var context = DataContextHelper.GetNewContext();

            var products = context.Products
                                    .Where(x => !x.IsDeleted && x.IsActive && !x.Category.IsDeleted)
                                    .OrderBy(x => x.ID)
                                    .AsQueryable();

            if (recordSize.HasValue && recordSize.Value > 0)
            {
                pageNo = pageNo ?? 1;
                var skip = (pageNo.Value - 1) * recordSize.Value;

                products = products.Skip(skip)
                                   .Take(recordSize.Value);
            }
            return products.ToList();
        }

        public List<Product> SearchFeaturedProducts(int? pageNo = 1, int? recordSize = 0, List<int> excludeProductIDs = null)
        {
            excludeProductIDs = excludeProductIDs ?? new List<int>();

            var context = DataContextHelper.GetNewContext();

            var products = context.Products
                                    .Where(x => !x.IsDeleted && x.IsActive && !x.Category.IsDeleted && x.isFeatured && !excludeProductIDs.Contains(x.ID))
                                    .OrderBy(x => x.ID)
                                    .AsQueryable();

            if (recordSize.HasValue && recordSize.Value > 0)
            {
                pageNo = pageNo ?? 1;
                var skip = (pageNo.Value - 1) * recordSize.Value;

                products = products.Skip(skip)
                                       .Take(recordSize.Value);
            }
            return products.ToList();
        }

        public List<Product> SearchProducts(List<int> categoryIDs, string searchTerm, decimal? from, decimal? to, string sortby, int? pageNo, int recordSize, bool activeOnly, out int count, int? stockCheckCount = null)
        {
            var context = DataContextHelper.GetNewContext();

            var products = context.Products
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = context.ProductRecords
                                  .Where(x => !x.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                  .Select(x => x.Product)
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();
            }

            if (categoryIDs != null && categoryIDs.Count > 0)
            {
                products = products.Where(x => categoryIDs.Contains(x.CategoryID));
            }

            if (from.HasValue && from.Value > 0.0M)
            {
                products = products.Where(x => x.Price >= from.Value);
            }

            if (to.HasValue && to.Value > 0.0M)
            {
                products = products.Where(x => x.Price <= to.Value);
            }

            if(stockCheckCount.HasValue && stockCheckCount.Value > 0)
            {
                products = products.Where(x => x.StockQuantity <= stockCheckCount.Value);
            }

            if (!string.IsNullOrEmpty(sortby))
            {
                if(string.Equals(sortby, "price-high", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.OrderByDescending(x => x.Price);
                }
                else {
                    products = products.OrderBy(x => x.Price);
                }
            }
            else //sortBy Product Date
            {
                products = products.OrderByDescending(x => x.ModifiedOn);
            }

            count = products.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return products.Skip(skipCount).Take(recordSize).Include("Category.CategoryRecords").Include("ProductPictures.Picture").ToList();
        }

        public List<Product> SearchProductsMoto(List<int> categoryIDs, int CatalogoId, int marcaId, string searchTerm, decimal? from, decimal? to, string sortby, int? pageNo, int recordSize, bool activeOnly, out int count, int? stockCheckCount = null)
        {
            var context = DataContextHelper.GetNewContext();

            var products = context.Products
                                  .Where(x => x.CatalogoId == CatalogoId && !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = context.ProductRecords
                                  .Where(x => !x.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                  .Select(x => x.Product)
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();
            }

            if (categoryIDs != null && categoryIDs.Count > 0)
            {
                products = products.Where(x => categoryIDs.Contains(x.CategoryID));
            }

            if (marcaId > 0)
            {
                products = products.Where(x => marcaId == x.MarcaId);
            }

            if (from.HasValue && from.Value > 0.0M)
            {
                products = products.Where(x => x.Discount >= from.Value);
            }

            if (to.HasValue && to.Value > 0.0M)
            {
                products = products.Where(x => x.Discount <= to.Value);
            }

            if (stockCheckCount.HasValue && stockCheckCount.Value > 0)
            {
                products = products.Where(x => x.StockQuantity <= stockCheckCount.Value);
            }

            if (!string.IsNullOrEmpty(sortby))
            {
                if (string.Equals(sortby, "price-high", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.OrderByDescending(x => x.Discount);
                }
                else
                {
                    products = products.OrderBy(x => x.Discount);
                }
            }
            else //sortBy Product Date
            {
                products = products.OrderByDescending(x => x.ModifiedOn);
            }

            count = products.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return products.Skip(skipCount).Take(recordSize).Include("Category.CategoryRecords").Include("ProductPictures.Picture").ToList();
        }
        public List<Product> SearchProductsParts(List<int> categoryIDs, int catalogoId, int marcaId, string searchTerm, decimal? from, decimal? to, string sortby, int? pageNo, int recordSize, bool activeOnly, out int count, int? stockCheckCount = null)
        {
            var context = DataContextHelper.GetNewContext();

            var products = context.Products
                                  .Where(x => x.CatalogoId == catalogoId && !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = context.ProductRecords
                                  .Where(x => !x.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                  .Select(x => x.Product)
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();
            }

            if (categoryIDs != null && categoryIDs.Count > 0)
            {
                products = products.Where(x => categoryIDs.Contains(x.CategoryID));
            }

            if (marcaId > 0)
            {
                products = products.Where(x => marcaId == x.MarcaId);
            }

            if (from.HasValue && from.Value > 0.0M)
            {
                products = products.Where(x => x.Discount >= from.Value);
            }

            if (to.HasValue && to.Value > 0.0M)
            {
                products = products.Where(x => x.Discount <= to.Value);
            }

            if (stockCheckCount.HasValue && stockCheckCount.Value > 0)
            {
                products = products.Where(x => x.StockQuantity <= stockCheckCount.Value);
            }

            if (!string.IsNullOrEmpty(sortby))
            {
                if (string.Equals(sortby, "price-high", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.OrderByDescending(x => x.Discount);
                }
                else
                {
                    products = products.OrderBy(x => x.Discount);
                }
            }
            else //sortBy Product Date
            {
                products = products.OrderByDescending(x => x.ModifiedOn);
            }

            count = products.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return products.Skip(skipCount).Take(recordSize).Include("Category.CategoryRecords").Include("ProductPictures.Picture").ToList();
        }


        public List<Product> GetProductWithLessStockQuantity(List<int> categoryIDs, string searchTerm, decimal? from, decimal? to, string sortby, bool activeOnly, int stockCount, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var products = context.Products
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = context.ProductRecords
                                  .Where(x => !x.IsDeleted && x.Name.ToLower().Contains(searchTerm.ToLower()))
                                  .Select(x => x.Product)
                                  .Where(x => !x.IsDeleted && (!activeOnly || x.IsActive) && !x.Category.IsDeleted)
                                  .AsQueryable();
            }

            if (categoryIDs != null && categoryIDs.Count > 0)
            {
                products = products.Where(x => categoryIDs.Contains(x.CategoryID));
            }

            if (from.HasValue && from.Value > 0.0M)
            {
                products = products.Where(x => x.Price >= from.Value);
            }

            if (to.HasValue && to.Value > 0.0M)
            {
                products = products.Where(x => x.Price <= to.Value);
            }

            products = products.Where(x => x.StockQuantity <= stockCount);

            if (!string.IsNullOrEmpty(sortby))
            {
                if (string.Equals(sortby, "price-high", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.OrderByDescending(x => x.Price);
                }
                else
                {
                    products = products.OrderBy(x => x.Price);
                }
            }
            else //sortBy Product Date
            {
                products = products.OrderByDescending(x => x.ModifiedOn);
            }

            count = products.Count();

            return products.Include("Category.CategoryRecords").ToList();
        }

        public ProductResponse GetProductResponseByID(int ID, bool activeOnly = true)
        {            
            var product = GetProductByID(ID, activeOnly);
            return ProductToProductResponse(product);          
        }

        public ProductResponse ProductToProductResponse(Product product) {

            ProductResponse response = new ProductResponse();

            ProductoCaracteristica productoCaracteristica = new ProductoCaracteristica();         

            if (!string.IsNullOrEmpty(product.Caracteristica)) {
                productoCaracteristica = JsonConvert.DeserializeObject<ProductoCaracteristica>(product.Caracteristica); 
            }

            productoCaracteristica = ValidaNulos(productoCaracteristica);

            response.ID = product.ID;
            response.IsActive = product.IsActive;
            response.IsDeleted = product.IsDeleted;
            response.ModifiedOn = product.ModifiedOn;
            response.TipoProducto = product.TipoProducto;
            response.CategoryID = product.CategoryID;
            response.Category = product.Category;

            response.Price = product.Price;
            response.Discount = product.Discount;
            response.Cost = product.Cost;
            response.isFeatured = product.isFeatured;
            response.ThumbnailPictureID = product.ThumbnailPictureID;
            response.SKU = product.SKU;
            response.Barcode = product.Barcode;
            response.Tags = product.Tags;
            response.Supplier = product.Supplier;
            response.StockQuantity = product.StockQuantity;
            response.ProductPictures = product.ProductPictures;
            response.ProductRecords = product.ProductRecords;
            response.Caracteristica = product.Caracteristica;
            response.MarcaID = product.MarcaId;
            response.CatalogoID = product.CatalogoId;
            response.TipoMoneda = product.TipoMoneda;
            response.EtiquetaOferta = product.EtiquetaOferta;
            response.EtiquetaSoat = product.EtiquetaSoat;
            response.IncluyeSoat = product.IncluyeSoat;
            response.ProductoCaracteristica = productoCaracteristica;
            return response;
        }


        public ProductoCaracteristica ValidaNulos(ProductoCaracteristica productoCaracteristica)
        {
             
            if (productoCaracteristica.suspension == null)
            {
                productoCaracteristica.suspension = new Suspension();
            }
            if (productoCaracteristica.destacados == null)
            {
                productoCaracteristica.destacados = new Destacados();
            }

            if (productoCaracteristica.arollanta == null)
            {
                productoCaracteristica.arollanta = new AroLLanta();
            }
            if (productoCaracteristica.dimensiones == null)
            {
                productoCaracteristica.dimensiones = new Dimensiones();
            }

            if (productoCaracteristica.consumo == null)
            {
                productoCaracteristica.consumo = new Consumo();
            }

            if (productoCaracteristica.motor == null)
            {
                productoCaracteristica.motor = new Motor();
            }
             
            if (productoCaracteristica.transmisiones == null)
            {
                productoCaracteristica.transmisiones = new Transmisions();
            }

            if (productoCaracteristica.frenos == null)
            {
                productoCaracteristica.frenos = new Frenos();
            }


            return productoCaracteristica;
        }

        public Product ProductResponseToProduct(ProductResponse productResponse)
        {            
            var caracteristicas = JsonConvert.SerializeObject(productResponse.ProductoCaracteristica);

            var product = new Product();
            product.ID = productResponse.ID;
            product.IsActive = productResponse.IsActive;
            product.IsDeleted = productResponse.IsDeleted;
            product.ModifiedOn = productResponse.ModifiedOn;            
            product.CategoryID = productResponse.CategoryID;
            product.Category = productResponse.Category;            
            product.Price = productResponse.Price;
            product.Discount = productResponse.Discount;
            product.Cost = productResponse.Cost;
            product.isFeatured = productResponse.isFeatured;
            product.ThumbnailPictureID = productResponse.ThumbnailPictureID;
            product.SKU = productResponse.SKU;
            product.Barcode = productResponse.Barcode;
            product.Tags = productResponse.Tags;
            product.Supplier = productResponse.Supplier;
            product.StockQuantity = productResponse.StockQuantity;
            product.ProductPictures = productResponse.ProductPictures;
            product.ProductRecords = productResponse.ProductRecords;
            product.TipoProducto = productResponse.TipoProducto;
            product.Caracteristica = caracteristicas;
            product.MarcaId = productResponse.MarcaID;
            product.CatalogoId = productResponse.CatalogoID;
            product.TipoMoneda = productResponse.TipoMoneda;
            product.EtiquetaOferta = productResponse.EtiquetaOferta;
            product.IncluyeSoat = productResponse.IncluyeSoat;
            product.EtiquetaSoat = productResponse.EtiquetaSoat;

            return product;
        }
        
        public string ProductoCaracteristicaToString(ProductoCaracteristica productoCaracteristica)
        {            
            return JsonConvert.SerializeObject(productoCaracteristica);
        }


        public Product GetProductByID(int ID, bool activeOnly = true)
        {
            var context = DataContextHelper.GetNewContext();

            var product = context.Products.Include("Category.CategoryRecords").Include("ProductPictures.Picture").FirstOrDefault(x=>x.ID == ID);

            if (activeOnly)
            {
                return product != null && !product.IsDeleted && product.IsActive && !product.Category.IsDeleted ? product : null;
            }
            else return product != null && !product.IsDeleted && !product.Category.IsDeleted ? product : null;
        }

        public List<Product> GetProductsByIDs(List<int> IDs)
        {
            var context = DataContextHelper.GetNewContext();

            return IDs.Select(id => context.Products.Find(id)).Where(x=>!x.IsDeleted && x.IsActive && !x.Category.IsDeleted).OrderBy(x=>x.ID).ToList();
        }
        
        public List<ProductSimple> GetProductsByMarcaID(Int32 id)
        {
            List<ProductSimple> list = new List<ProductSimple>();
            var context = DataContextHelper.GetNewContext();
            List<Product> products = context.Products.Where(x => x.MarcaId == id).ToList();
            decimal tipoCambio = TipoCambioService.Instance.GetTypeUltimateChanged();

            products.ForEach( p=>
                {
                    var currentLanguageRecord = p.ProductRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);                    
                    ProductSimple ps = new ProductSimple();                    
                    var price = p.Discount.HasValue && p.Discount.Value > 0 ? p.Discount.Value : p.Price;
                    var priceWithTypeChange = p.TipoMoneda == 2 ? price * tipoCambio : price;                 
                    ps.Id = p.ID; ;
                    ps.Name = currentLanguageRecord.Name;
                    ps.Money = p.TipoMoneda == 1 ? "S/" : "$";
                    ps.Price = priceWithTypeChange;
                    list.Add(ps);
                });
            return list;
        }

        public ProductRecord GetProductRecordByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var productRecord = context.ProductRecords.Find(ID);

            return productRecord != null && !productRecord.IsDeleted ? productRecord : null;
        }

        public decimal GetMaxProductPrice()
        {
            var context = DataContextHelper.GetNewContext();

            var products = context.Products.Where(x => !x.IsDeleted && x.IsActive && !x.Category.IsDeleted);

            return products.Count() > 0 ? products.Max(x => x.Price) : 0;
        }

        public bool SaveProduct(Product product)
        {
            var context = DataContextHelper.GetNewContext();

            context.Products.Add(product);

            return context.SaveChanges() > 0;
        }

        public bool SaveProductRecord(ProductRecord productRecord)
        {
            var context = DataContextHelper.GetNewContext();

            context.ProductRecords.Add(productRecord);

            return context.SaveChanges() > 0;
        }

        
        
        public bool UpdateProduct(Product product)
        {
            var context = DataContextHelper.GetNewContext();

            var existingProduct = context.Products.Find(product.ID);

            context.Entry(existingProduct).CurrentValues.SetValues(product);

            return context.SaveChanges() > 0;
        }

        public bool UpdateProductPictures(int productID, List<ProductPicture> newPictures)
        {
            var context = DataContextHelper.GetNewContext();

            var oldPictures = context.ProductPictures.Where(p => p.ProductID == productID);

            context.ProductPictures.RemoveRange(oldPictures);

            context.ProductPictures.AddRange(newPictures);

            return context.SaveChanges() > 0;
        }

        public bool UpdateProductPictureColors(int productID, List<ProductColor> newColors)
        {
            var context = DataContextHelper.GetNewContext();

            var oldColors = context.ProductsColors.Where(p => p.ProductID == productID);

            context.ProductsColors.RemoveRange(oldColors);

            context.ProductsColors.AddRange(newColors);

            return context.SaveChanges() > 0;
        }

        public bool UpdateProductRecord(ProductRecord productRecord)
        {
            var context = DataContextHelper.GetNewContext();

            var existingRecord = context.ProductRecords.Find(productRecord.ID);

            context.Entry(existingRecord).CurrentValues.SetValues(productRecord);

            return context.SaveChanges() > 0;
        }

        public bool UpdateProductSpecifications(int productRecordID, List<ProductSpecification> newProductSpecification)
        {
            var context = DataContextHelper.GetNewContext();

            var oldProductSpecifications = context.ProductSpecifications.Where(p => p.ProductRecordID == productRecordID);
            
            context.ProductSpecifications.RemoveRange(oldProductSpecifications);


            context.ProductSpecifications.AddRange(newProductSpecification);

            return context.SaveChanges() > 0;
        }
        
        public bool UpdateProductSpecificationsNew(int productRecordID, List<ProductSpecification> newProductSpecification)
        {
            var context = DataContextHelper.GetNewContext();

            //context.ProductSpecifications.Remove

            var oldProductSpecifications = context.ProductSpecifications.Where(p => p.ProductRecordID == productRecordID);



            context.ProductSpecifications.RemoveRange(oldProductSpecifications);

            context.ProductSpecifications.AddRange(newProductSpecification);

            return context.SaveChanges() > 0;
        }

        public bool DeleteProduct(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var product = context.Products.Find(ID);

            product.IsDeleted = true;

            context.Entry(product).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public void UpdateProductQuantities(Order order)
        {
            eCommerceContext context = new eCommerceContext();

            foreach (var orderItem in order.OrderItems)
            {
                var dbProduct = context.Products.Find(orderItem.ProductID);

                dbProduct.StockQuantity = dbProduct.StockQuantity - orderItem.Quantity;

                context.Entry(dbProduct).State = EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
