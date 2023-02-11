using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Shared.Helpers;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using eCommerce.Shared.Extensions;
using eCommerce.Shared.Enums;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using eCommerce.Shared.Commons;
using System.Configuration;
using System.Drawing.Printing;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class ProductsController : DashboardBaseController
    {
        public ActionResult Index(int? categoryID, bool? showOnlyLowStock, string searchTerm, int? pageNo/*, string colorID*/)
        {
            var recordSize = (int)RecordSizeEnums.Size10;          

            ProductsListingViewModel model = new ProductsListingViewModel
            {
                SearchTerm = searchTerm,
                ShowOnlyLowStock = showOnlyLowStock,
                Categories = CategoriesService.Instance.GetCategories()
            };

            List<int> selectedCategoryIDs = null;

            if (categoryID.HasValue && categoryID.Value > 0)
            {
                var selectedCategory = model.Categories.FirstOrDefault(x=>x.ID == categoryID);

                if (selectedCategory != null)
                {
                    model.CategoryID = selectedCategory.ID;

                    var searchedCategories = CategoryHelpers.GetAllCategoryChildrens(selectedCategory, model.Categories);

                    selectedCategoryIDs = searchedCategories != null ? searchedCategories.Select(x => x.ID).ToList() : null;
                }
            }

            model.Products = ProductsService.Instance.SearchProducts(selectedCategoryIDs, searchTerm, null, null, null, pageNo, recordSize, activeOnly: false, out int count, stockCheckCount: showOnlyLowStock.HasValue && showOnlyLowStock.Value ? 5 : 0);

            model.Pager = new Pager(count, pageNo, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ProductActionViewModel model = new ProductActionViewModel();

            if (ID.HasValue)
            {
                var product = ProductsService.Instance.GetProductResponseByID(ID.Value, activeOnly: false);

                if (product == null) return HttpNotFound();

                var currentLanguageRecord = product.ProductRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);

                currentLanguageRecord = currentLanguageRecord ?? new ProductRecord();

                string precioStr = product.Price.ToString();
                string dsctoStr = product.Discount.ToString();
                string costStr = product.Cost.ToString();
                precioStr = precioStr.Replace(",", ".");
                dsctoStr = dsctoStr.Replace(",", ".");
                costStr = costStr.Replace(",", ".");

                model.ProductID = product.ID;
                model.CategoryID = product.CategoryID;

                model.Price = product.Price;
                model.Discount = product.Discount;
                model.Cost = product.Cost;
                //model.PriceStr = precioStr;
                //model.DiscountStr = dsctoStr;
                //model.CostStr = costStr;

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
                model.EtiquetaOferta = product.EtiquetaOferta;
                model.EtiquetaSoat = product.EtiquetaSoat;
                model.IncluyeSoat = product.IncluyeSoat;
                model.ProductColors = ProductColorService.Instance.SearchProductColorByProductId(product.ID);
            }

            model.Categories = CategoriesService.Instance.GetCategoryByCatalogoID(model.CatalogoID);
            //model.Categories = CategoriesService.Instance.GetCategories();
            model.Colors = ColorService.Instance.GetAllColors();
            model.Catalogos = CatalogoService.Instance.GetCatalogos();
            model.Marcas = MarcaService.Instance.GetMarcaByCatalogoID(model.CatalogoID);
            //model.Marcas = MarcaService.Instance.ListarMarca();
            model.TipoMonedas = TablaMasterService.Instance.GetTablaMasterByTipoTabla("TIPO_MONEDA");

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult Action(FormCollection formCollection)
        {
            JsonResult json = new JsonResult();

            try
            {
                ProductActionViewModel model = GetProductActionViewModelFromForm(formCollection);

                if (model.ProductID > 0)
                {
                    var product = ProductsService.Instance.GetProductResponseByID(model.ProductID, activeOnly: false);

                    if (product == null)
                    {
                        throw new Exception("Dashboard.Products.Action.Validation.ProductNotFound".LocalizedString());
                    }

                    var stockFinal = calcularStock(model.StockQuantity, model.ProductColorsStock);

                    product.ID = model.ProductID;
                    product.CategoryID = model.CategoryID;
                    product.Price = model.Price;
                    product.Discount = model.Discount;
                    product.Cost = model.Cost;
                    product.SKU = model.SKU;
                    product.Barcode = model.Barcode;
                    product.Tags = model.Tags;
                    product.Supplier = model.Supplier;
                    product.StockQuantity = stockFinal;
                    product.isFeatured = model.isFeatured;
                    product.ModifiedOn = DateTime.Now;
                    product.ProductoCaracteristica = model.ProductoCaracteristica;
                    product.MarcaID = model.MarcaID;
                    product.CatalogoID = model.CatalogoID;
                    product.TipoMoneda = model.TipoMoneda;
                    product.EtiquetaOferta = model.EtiquetaOferta;
                    product.EtiquetaSoat = model.EtiquetaSoat;
                    product.IncluyeSoat = model.IncluyeSoat;

                    if (!string.IsNullOrEmpty(model.ProductPictures))
                    {
                        var pictureIDs = model.ProductPictures
                                                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(ID => int.Parse(ID)).ToList();                        

                        if (pictureIDs.Count > 0)
                        {
                            var newPictures = new List<ProductPicture>();

                            newPictures.AddRange(pictureIDs.Select(x => new ProductPicture() { ProductID = product.ID, PictureID = x }).ToList());

                            if (!ProductsService.Instance.UpdateProductPictures(product.ID, newPictures))
                            {
                                throw new Exception("Dashboard.Products.Action.Validation.UnableToUpdateProductPictures".LocalizedString());
                            }

                            product.ThumbnailPictureID = model.ThumbnailPicture != 0 ? model.ThumbnailPicture : pictureIDs.First();
                        }

                        if (!string.IsNullOrEmpty(model.ProductColorsPicture))
                        {
                            var colorPictureIDs = model.ProductColorsPicture
                                                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(ID => int.Parse(ID)).ToList();

                            var colorIds = model.ProductColorsId
                                                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                        .Select(ID => int.Parse(ID)).ToList();

                            var colorStocks = model.ProductColorsStock
                                                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                        .Select(ID => int.Parse(ID)).ToList();

                            if (colorPictureIDs.Count > 0)
                            {
                                var newProductColors = new List<ProductColor>();

                                for (int i = 0; i < colorPictureIDs.Count; i++)
                                {

                                    ProductColor pcolor = new ProductColor();
                                    pcolor.ProductID = product.ID;
                                    pcolor.PictureID = colorPictureIDs[i];
                                    pcolor.ColorID = colorIds[i];
                                    pcolor.Stock = colorStocks[i];
                                    newProductColors.Add(pcolor);
                                }

                                if (!ProductsService.Instance.UpdateProductPictureColors(product.ID, newProductColors))
                                {
                                    throw new Exception("Dashboard.Products.Action.Validation.UnableToUpdateProductPictures".LocalizedString());
                                }
                            }

                        }                                                  
                    }

                    product.IsActive = !model.InActive;
                    product.TipoProducto = model.TipoProducto;

                    var toProduct = ProductsService.Instance.ProductResponseToProduct(product);


                    if (!ProductsService.Instance.UpdateProduct(toProduct))
                    {
                        throw new Exception("Dashboard.Products.Action.Validation.UnableToUpdateProduct".LocalizedString());
                    }

                    var currentLanguageRecord = product.ProductRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);

                    var isNewRecord = false;

                    if (currentLanguageRecord == null)
                    {
                        currentLanguageRecord = new ProductRecord();
                        isNewRecord = true;
                    }

                    currentLanguageRecord.ProductID = product.ID;
                    currentLanguageRecord.LanguageID = AppDataHelper.CurrentLanguage.ID;
                    currentLanguageRecord.Name = model.Name;
                    currentLanguageRecord.Summary = model.Summary;
                    currentLanguageRecord.Description = model.Description;

                    currentLanguageRecord.ModifiedOn = DateTime.Now;

                    var newProductSpecification = new List<ProductSpecification>();

                    if (model.ProductSpecifications != null && model.ProductSpecifications.Count > 0)
                    {
                        newProductSpecification.AddRange(model.ProductSpecifications.Select(x => new ProductSpecification() { ProductRecordID = currentLanguageRecord.ID, Title = x.Title, Value = x.Value }));
                    }

                    if (isNewRecord)
                    {
                        currentLanguageRecord.ProductSpecifications = newProductSpecification;
                    }
                    else
                    {
                        var productSpecsUpdated = ProductsService.Instance.UpdateProductSpecifications(currentLanguageRecord.ID, newProductSpecification);

                        /*No need to check if products specs were updated because their maybe cases when no specs are presnt and database doesn't update anything so it will return false*/
                        //if (!productSpecsUpdated)
                        //{
                        //    throw new Exception("Dashboard.Products.Action.Validation.UnableToUpdateProductSpecification".LocalizedString());
                        //}
                    }

                    var result = false;
                    if (isNewRecord)
                    {
                        result = ProductsService.Instance.SaveProductRecord(currentLanguageRecord);
                    }
                    else
                    {
                        result = ProductsService.Instance.UpdateProductRecord(currentLanguageRecord);
                    }

                    if (!result)
                    {
                        throw new Exception("Dashboard.Products.Action.Validation.UnableToUpdateProductRecord".LocalizedString());
                    }
                }
                else
                {
                    var caracteristica = ProductsService.Instance.ProductoCaracteristicaToString(model.ProductoCaracteristica);
                    var stockFinal = calcularStock(model.StockQuantity, model.ProductColorsStock);

                    Product product = new Product
                    {
                        CategoryID = model.CategoryID,
                        Price = model.Price,

                        Discount = model.Discount,
                        Cost = model.Cost,
                        SKU = model.SKU,
                        Barcode = model.Barcode,
                        Tags = model.Tags,
                        Supplier = model.Supplier,

                        StockQuantity = stockFinal,
                        Caracteristica = caracteristica,
                        isFeatured = model.isFeatured,
                        TipoProducto = model.TipoProducto,
                        ModifiedOn = DateTime.Now,
                        MarcaId = model.MarcaID,
                        CatalogoId = model.CatalogoID,
                        TipoMoneda = model.TipoMoneda
                    };

                    

                    if (!string.IsNullOrEmpty(model.ProductPictures))
                    {
                        var pictureIDs = model.ProductPictures
                                                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(ID => int.Parse(ID)).ToList();

                        if (pictureIDs.Count > 0)
                        {
                            product.ProductPictures = new List<ProductPicture>();
                            product.ProductPictures.AddRange(pictureIDs.Select(x => new ProductPicture() { ProductID = product.ID, PictureID = x }).ToList());

                            product.ThumbnailPictureID = model.ThumbnailPicture != 0 ? model.ThumbnailPicture : pictureIDs.First();
                        }
                    }



                    product.IsActive = !model.InActive;

                    if (!ProductsService.Instance.SaveProduct(product))
                    {
                        throw new Exception("Dashboard.Products.Action.Validation.UnableToCreateProduct".LocalizedString());
                    }

                    if (!string.IsNullOrEmpty(model.ProductPictures))
                    {                        
                        if (!string.IsNullOrEmpty(model.ProductColorsPicture))
                        {
                            var colorPictureIDs = model.ProductColorsPicture
                                                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                    .Select(ID => int.Parse(ID)).ToList();

                            var colorIds = model.ProductColorsId
                                                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                        .Select(ID => int.Parse(ID)).ToList();

                            var colorStocks = model.ProductColorsStock
                                                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                                        .Select(ID => int.Parse(ID)).ToList();

                            if (colorPictureIDs.Count > 0)
                            {
                                var newProductColors = new List<ProductColor>();

                                for (int i = 0; i < colorPictureIDs.Count; i++)
                                {

                                    ProductColor pcolor = new ProductColor();
                                    pcolor.ProductID = product.ID;
                                    pcolor.PictureID = colorPictureIDs[i];
                                    pcolor.ColorID = colorIds[i];
                                    pcolor.Stock = colorStocks[i];
                                    newProductColors.Add(pcolor);
                                }

                                if (!ProductsService.Instance.UpdateProductPictureColors(product.ID, newProductColors))
                                {
                                    throw new Exception("Dashboard.Products.Action.Validation.UnableToUpdateProductPictures".LocalizedString());
                                }
                            }

                        }

                    }                    

                    var currentLanguageRecord = new ProductRecord
                    {
                        ProductID = product.ID,
                        LanguageID = AppDataHelper.CurrentLanguage.ID,
                        Name = model.Name,
                        Summary = model.Summary,
                        Description = model.Description,

                        ModifiedOn = DateTime.Now
                    };

                    if (model.ProductSpecifications != null)
                    {
                        currentLanguageRecord.ProductSpecifications = new List<ProductSpecification>();
                        currentLanguageRecord.ProductSpecifications.AddRange(model.ProductSpecifications.Select(x => new ProductSpecification() { ProductRecordID = product.ID, Title = x.Title, Value = x.Value }));
                    }

                    var result = ProductsService.Instance.SaveProductRecord(currentLanguageRecord);

                    if (!result)
                    {
                        throw new Exception("Dashboard.Products.Action.Validation.UnableToCreateProductRecord".LocalizedString());
                    }
                }

                json.Data = new { Success = true };
            }
            catch (Exception ex)
            {
                json.Data = new { Success = false, Message = ex.Message };
            }

            return json;
        }

        public int calcularStock(int stockProducto, String productColorStock) {

            var colorStocks = productColorStock.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ID => int.Parse(ID)).ToList();
            var stock = colorStocks.Sum();
            if (stock <= 0) {
                return stockProducto;
            }
            return stock;
        }

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            JsonResult result = new JsonResult();

            try
            {
                var operation = ProductsService.Instance.DeleteProduct(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Dashboard.Products.Action.Validation.UnableToDeleteProduct".LocalizedString() };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }

        private ProductActionViewModel GetProductActionViewModelFromForm(FormCollection formCollection)
        {
            var productoCaracteristicas = new ProductoCaracteristica
            {
                motor = new Motor {
                    Cilindrada = formCollection["Cilindrada"],
                    NroCilindrada = formCollection["NroCilindrada"],
                    Potencia = formCollection["Potencia"],
                    TipoMotor = formCollection["TipoMotor"],
                    SistemaEnfriamiento = formCollection["SistemaEnfriamiento"],
                    SistemaEncendido = formCollection["SistemaEncendido"],
                    SistemaArranque = formCollection["SistemaArranque"],
                    Torque = formCollection["Torque"]
                },
                frenos = new Frenos {
                    FrenoDelantero = formCollection["FrenoDelantero"],
                    FrenoTrasero = formCollection["FrenoTrasero"]
                },
                arollanta = new AroLLanta
                {
                    NeumaticoDelantero = formCollection["NeumaticoDelantero"],
                    NeumaticoPosterior = formCollection["NeumaticoPosterior"],
                    AroDelantero = formCollection["AroDelantero"],
                    AroPosterior = formCollection["AroPosterior"]
                },
                suspension = new Suspension
                {
                    SuspensionDelantera = formCollection["SuspensionDelantera"],
                    SuspensionPosterior = formCollection["SuspensionPosterior"]

                },

                consumo = new Consumo
                {
                    Octanaje = formCollection["Octanaje"],
                    SistemaCombustible = formCollection["SistemaCombustible"],
                    CapacidadTanque = formCollection["CapacidadTanque"],
                    Autonomia = formCollection["Autonomia"],
                    RendimientoGalon = formCollection["RendimientoGalon"]
                },
                transmisiones = new Transmisions
                {
                    Transmision = formCollection["Transmision"],
                    NroCambios = formCollection["NroCambios"],
                    VelocidadMaxima = formCollection["VelocidadMaxima"]

                },
                dimensiones = new Dimensiones
                {
                    Peso = formCollection["Peso"],
                    CargaUtil = formCollection["CargaUtil"],
                    Pasajeros = formCollection["Pasajeros"]

                },

                destacados = new Destacados
                {
                    Texto1 = formCollection["Texto1"],
                    Texto2 = formCollection["Texto2"],
                    Texto3 = formCollection["Texto3"],
                    Texto4 = formCollection["Texto4"],
                    Compacta = formCollection["Compacta"],
                    AroRayos = formCollection["AroRayos"],
                    ColoresDisponibles = formCollection["ColoresDisponibles"],
                    Adicionales = formCollection["Adicionales"],
                    Tablero = formCollection["Tablero"],
                    Garantia = formCollection["Garantia"]
                },
            };


            var model = new ProductActionViewModel
            {
                ProductID = !string.IsNullOrEmpty(formCollection["ProductID"]) ? int.Parse(formCollection["ProductID"]) : 0,
                CategoryID = int.Parse(formCollection["CategoryID"]),
                Price = decimal.Parse(formCollection["Price"]),

                Discount = !string.IsNullOrEmpty(formCollection["Discount"]) ? decimal.Parse(formCollection["Discount"]) : 0,
                Cost = !string.IsNullOrEmpty(formCollection["Cost"]) ? decimal.Parse(formCollection["Cost"]) : 0,
                SKU = formCollection["SKU"],
                Tags = formCollection["Tags"],
                Barcode = formCollection["Barcode"],
                Supplier = formCollection["Supplier"],

                StockQuantity = int.Parse(formCollection["StockQuantity"]),

                isFeatured = formCollection["isFeatured"].Contains("true"),
                InActive = formCollection["InActive"].Contains("true"),
                TipoProducto = formCollection["TipoProducto"].Contains("true"),
                ProductPictures = formCollection["ProductPictures"],
                ProductColorsPicture = formCollection["ProductColorsPicture"],
                ProductColorsId = formCollection["ProductColorsId"],
                ProductColorsStock = formCollection["ProductColorsStock"],
                ThumbnailPicture = !string.IsNullOrEmpty(formCollection["ThumbnailPicture"]) ? int.Parse(formCollection["ThumbnailPicture"]) : 0,
                
                EtiquetaOferta = formCollection["EtiquetaOferta"],
                EtiquetaSoat = formCollection["EtiquetaSoat"],
                IncluyeSoat = formCollection["IncluyeSoat"].Contains("true"),

                ProductRecordID = !string.IsNullOrEmpty(formCollection["ProductRecordID"]) ? int.Parse(formCollection["ProductRecordID"]) : 0,
                Name = formCollection["Name"],
                Summary = formCollection["Summary"],
                Description = formCollection["Description"],

                ProductSpecifications = new List<ProductSpecification>(),
                ProductoCaracteristica = productoCaracteristicas,
                MarcaID = int.Parse(formCollection ["MarcaID"]),
                CatalogoID = int.Parse(formCollection ["CatalogoID"]),
                TipoMoneda = int.Parse(formCollection["TipoMoneda"])
                
            };

            foreach (string key in formCollection)
            {
                if (key.Contains("specification"))
                {
                    var value = formCollection[key];

                    if(!string.IsNullOrEmpty(value))
                    {
                        var specificationTitle = value.GetSubstringText("", "~");
                        var specificationValue = value.GetSubstringText("~", "");

                        if (!string.IsNullOrEmpty(specificationTitle) && !string.IsNullOrEmpty(specificationValue))
                        {
                            model.ProductSpecifications.Add(new ProductSpecification() { Title = specificationTitle, Value = specificationValue });
                        }
                    }
                }
            }

            return model;
        }       
                       

        [HttpPost]
        public JsonResult Consultor(int CatalogoID)
        {
            JsonResult result = new JsonResult();
            try
            {
                var operation = CategoriesService.Instance.GetCategoryByCatalogoID(CatalogoID);
                result.Data = new { Success = operation, Message = string.Empty };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result; 
        }

        [HttpPost]
        public JsonResult LoadCategoriesAndMarcasByCatalogoID(int CatalogoID)
        {

            List<Category> categories = CategoriesService.Instance.GetCategoryByCatalogoID(CatalogoID);
            List<Marca> marcas = MarcaService.Instance.GetMarcaByCatalogoID(CatalogoID);
            List<CategoryResponse> categoriesResponse = new List<CategoryResponse>();
            List<MarcaResponse> marcaResponses = new List<MarcaResponse>();

            foreach (Category cat in categories) {

                var categoryResponse = new CategoryResponse();

                var currentLanguageCategoryRecord = cat.CategoryRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);
                var nameCategory = "";

                if (currentLanguageCategoryRecord != null)
                {
                    nameCategory = currentLanguageCategoryRecord.Name;
                }
                else
                {
                    nameCategory = cat.SanitizedName;
                }

                categoryResponse.ID = cat.ID;
                categoryResponse.NombreCategoria = nameCategory;
                categoriesResponse.Add(categoryResponse);
            }
            
            foreach (Marca marca in marcas) {

                var marcaResponse = new MarcaResponse();
                marcaResponse.ID = marca.ID;
                marcaResponse.NombreMarca = marca.Descripcion;
                marcaResponses.Add(marcaResponse);
            }

            CatalogoChangeResponse response = new CatalogoChangeResponse();
            response.Categorias = categoriesResponse;
            response.Marcas = marcaResponses;
            return Json(response);
        }

        [HttpPost]
        public JsonResult LoadTallas()
        {           
            var tallas = TallaService.Instance.AllTalla();           

            List<TallaResponse> response = new List<TallaResponse>();
            tallas.ForEach(x => {

                TallaResponse obj = new TallaResponse();
                obj.TallaID = x.ID;
                obj.Description = x.Description;
                obj.Orden = x.Orden;
                response.Add(obj);
            });            
            return Json(response);
        }

        [HttpPost]
        public JsonResult LoadColores()
        {
            var colores = ColorService.Instance.GetAllColors();

            List<ColorResponse> response = new List<ColorResponse>();
            colores.ForEach(x => {

                ColorResponse obj = new ColorResponse();
                obj.ColorID = x.ID;
                obj.Description = x.Description;
                obj.Valor = x.Valor;
                response.Add(obj);
            });
            return Json(response);
        }

        public class CatalogoChangeResponse {
            public List<CategoryResponse> Categorias { get; set; }
            public List<MarcaResponse> Marcas { get; set; }
        }

        public class CategoryResponse
        {
            public int ID { get; set; }
            public string NombreCategoria { get; set; }
        }
        
        public class MarcaResponse
        {
            public int ID { get; set; }
            public string NombreMarca { get; set; }
        }

        public class TallaResponse {
            public int TallaID { get; set; }
            public string Description { get; set; }
            public int Orden { get; set; }

        }
        
        public class ColorResponse {

            public int ColorID { get; set; }
            public string Description { get; set; }
            public string Valor { get; set; }

        }

    }
}
