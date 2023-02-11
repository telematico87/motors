using eCommerce.Shared.Enums;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Entities.Request;
using eCommerce.Shared.Helpers;
using eCommerce.Shared.Commons;
using OfficeOpenXml;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class CargaMasivaController : DashboardBaseController
    {
        // GET: Dashboard/CargaMasiva
        public ActionResult Index(int? categoryID, bool? showOnlyLowStock, string searchTerm, int? pageNo/*, string colorID*/)
        {
            var recordSize = (int)RecordSizeEnums.Size10;

            CargaMasivaViewModel model = new CargaMasivaViewModel
            {
            };

            return View(model);
        }

        public FileResult DownloadExcel(string fileName)
        {
            string path2 = "~/Content/bm3/excel-format";

            string path = string.Format(@"~\Content\bm3\excel-format\{0}", fileName);
            //return File(path, "application/vnd.ms-excel");
            return File(path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult DownloadExcel2(string fileName)
        {
            string path = string.Format(@"~\Content\bm3\excel-format\{0}", fileName);
            return File(path, "application/octet-stream");
        }

        public FilePathResult DownloadExampleFilesTxt(string fileName)
        {
            return new FilePathResult(string.Format(@"~\Content\bm3\excel-format\{0}", fileName + ".txt"), "text/plain");
        }

        public FilePathResult DownloadExampleFiles(string fileName)
        {
            //return new FilePathResult(string.Format(@"~\Content\bm3\excel-format\{0}", fileName + ".xlsx"), "application/vnd.ms-excel");
            return new FilePathResult(string.Format(@"~\Content\bm3\excel-format\{0}", fileName + ".xlsx"), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Server.MapPath("~/Content/bm3/excel-format/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        [HttpPost]
        public JsonResult UploadExcel(ProductRequest products, HttpPostedFileBase FileUpload)
        {

            JsonResult json = new JsonResult();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/Content/bm3/excel-format/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Hoja1$]", connectionString);
                    //var adapter2 = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    adapter.Fill(ds, "ExcelTable");
                    DataTable dtable = ds.Tables["ExcelTable"];
                    string sheetName = "Hoja1";
                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var productsExcel = from a in excelFile.Worksheet<ProductRequest>(sheetName) select a;

                    List<CargaMasivaResponse> listResponse = new List<CargaMasivaResponse>();

                    var filaExcel = 2;
                    foreach (var model in productsExcel)
                    {
                        try
                        {
                            CargaMasivaResponse response = new CargaMasivaResponse();
                            response.data = validarDatos(model, filaExcel);

                            if (response.data.Count == 0)
                            {
                                //Obtener Categoria o crear una nueva Categoria                                
                                Category categoria = CategoriesService.Instance.GetCategoryByNameAndByCatalogo(model.Categoria, model.IDCatalogo);
                                Marca marca = MarcaService.Instance.GetMarcaByName(model.Marca);

                                Product product = new Product
                                {
                                    ID = model.IDProducto,
                                    CategoryID = categoria.ID,
                                    MarcaId = marca.ID,
                                    Price = model.Precio,
                                    Discount = model.Descuento,
                                    Cost = model.Costo,
                                    SKU = model.SKU,
                                    Barcode = null,
                                    Tags = model.Tags,
                                    Supplier = model.Proveedor,
                                    StockQuantity = model.Stock,
                                    Caracteristica = null,
                                    isFeatured = false,
                                    TipoProducto = false,
                                    ModifiedOn = DateTime.Now,

                                    CatalogoId = model.IDCatalogo,
                                    TipoMoneda = model.TipoMoneda,
                                    EtiquetaOferta = model.EtiquetaOferta
                                };

                                product.IsActive = true;

                                if (!ProductsService.Instance.SaveProduct(product))
                                {
                                    throw new Exception("Error al insertar producto en la línea: " + filaExcel);
                                }

                                //Guardar Especificaciones                                                               
                                List<ProductSpecification> productSpecifications = buildSpecifications(model);

                                var currentLanguageRecord = new ProductRecord
                                {
                                    ProductID = product.ID,
                                    LanguageID = AppDataHelper.CurrentLanguage.ID,
                                    Name = model.Nombre,
                                    Summary = model.Resumen,
                                    Description = model.Descripcion,
                                    ModifiedOn = DateTime.Now
                                };

                                if (productSpecifications.Count > 0)
                                {
                                    currentLanguageRecord.ProductSpecifications = new List<ProductSpecification>();
                                    currentLanguageRecord.ProductSpecifications.AddRange(productSpecifications.Select(x => new ProductSpecification() { ProductRecordID = product.ID, Title = x.Title, Value = x.Value }));
                                }

                                var result = ProductsService.Instance.SaveProductRecord(currentLanguageRecord);

                                if (!result)
                                {
                                    throw new Exception("Dashboard.Products.Action.Validation.UnableToCreateProductRecord".LocalizedString());
                                }

                                //Guardar Stock
                                List<ProductStock> productStocks = buildStock(model, product.ID);

                                var resultStock = ProductStockService.Instance.SaveProductStockRange(product.ID, productStocks);
                                if (!result)
                                {
                                    throw new Exception("Error al guardar Stocks");
                                }

                            }
                            else
                            {
                                listResponse.Add(response);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {
                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                                }
                            }
                        }

                        filaExcel++;
                    }
                    //deleting excel file from folder
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }

                    if (listResponse.Count > 0)
                    {
                        listResponse.ToArray();
                        json.Data = new { Success = false, Data = listResponse };
                        return json;
                        //return Json(listResponse, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        json.Data = new { Success = true };
                        return json;
                    }
                    //return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<string> data = new List<string>();
                    //alert message for invalid file format                    
                    data.Add("Solo se permite archivos en formato Excel");
                    data.ToArray();
                    json.Data = new { Success = false, Data = data };
                    return json;
                    //return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                List<string> data = new List<string>();
                //alert message for invalid file format                                                    
                if (FileUpload == null) data.Add("Por favor seleccione un archivo Excel");
                data.ToArray();
                json.Data = new { Success = false, Data = data };
                return json;
                //return Json(data, JsonRequestBehavior.AllowGet);
            }
        }        

        private List<ProductSpecification> buildSpecifications(ProductRequest r)
        {

            List<ProductSpecification> specifications = new List<ProductSpecification>();
            ProductSpecification specification1 = new ProductSpecification();
            ProductSpecification specification2 = new ProductSpecification();
            ProductSpecification specification3 = new ProductSpecification();
            ProductSpecification specification4 = new ProductSpecification();
            ProductSpecification specification5 = new ProductSpecification();

            if (r.Esp_Titulo1.Trim() != null && r.Esp_Valor1.Trim() != null)
            {
                specification1.Title = r.Esp_Titulo1.Trim();
                specification1.Value = r.Esp_Valor1.Trim();
                specifications.Add(specification1);
            }
            if (r.Esp_Titulo2.Trim() != null && r.Esp_Valor2.Trim() != null)
            {
                specification2.Title = r.Esp_Titulo2.Trim();
                specification2.Value = r.Esp_Valor2.Trim();
                specifications.Add(specification2);
            }
            if (r.Esp_Titulo3.Trim() != null && r.Esp_Valor3.Trim() != null)
            {
                specification3.Title = r.Esp_Titulo3.Trim();
                specification3.Value = r.Esp_Valor3.Trim();
                specifications.Add(specification3);
            }
            if (r.Esp_Titulo4.Trim() != null && r.Esp_Valor4.Trim() != null)
            {
                specification4.Title = r.Esp_Titulo4.Trim();
                specification4.Value = r.Esp_Valor4.Trim();
                specifications.Add(specification4);
            }
            if (r.Esp_Titulo5.Trim() != null && r.Esp_Valor5.Trim() != null)
            {
                specification5.Title = r.Esp_Titulo5.Trim();
                specification5.Value = r.Esp_Valor5.Trim();
                specifications.Add(specification5);
            }
            return specifications;
        }

        private List<ProductStock> buildStock(ProductRequest r, int ProductID)
        {

            List<ProductStock> stocks = new List<ProductStock>();


            //Talla S
            if (r.SKU_S != null)
            {
                if (r.SKU_S.Trim() != "")
                {
                    var prod = createStock(ProductID, eCommerceConstants.TALLA_S, r.Stock_TallaS, r.SKU_S);
                    stocks.Add(prod);
                }
            }

            //Talla M
            if (r.SKU_M != null)
            {
                if (r.SKU_M != null)
                {
                    var prod = createStock(ProductID, eCommerceConstants.TALLA_M, r.Stock_TallaM, r.SKU_M);
                    stocks.Add(prod);
                }
            }

            //Talla L
            if (r.SKU_L != null)
            {
                if (r.SKU_L.Trim() != "")
                {
                    var prod = createStock(ProductID, eCommerceConstants.TALLA_L, r.Stock_TallaL, r.SKU_L);
                    stocks.Add(prod);
                }
            }

            //Talla XL
            if (r.SKU_XL != null)
            {
                if (r.SKU_XL.Trim() != "")
                {
                    var prod = createStock(ProductID, eCommerceConstants.TALLA_XL, r.Stock_TallaXL, r.SKU_XL);
                    stocks.Add(prod);
                }
            }

            //Talla XXL
            if (r.SKU_XXL != null)
            {
                if (r.SKU_XXL.Trim() != "")
                {
                    var prod = createStock(ProductID, eCommerceConstants.TALLA_XXL, r.Stock_TallaXXL, r.SKU_XXL);
                    stocks.Add(prod);
                }
            }
            return stocks;
        }

        public ProductStock createStock(int productID, int tallaId, int stock, string sku)
        {

            ProductStock obj = new ProductStock();
            obj.ProductID = productID;
            obj.TallaID = tallaId;
            obj.Stock = stock;
            obj.SKU = sku;
            return obj;
        }

        public void DownloadPlantilla()
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();

            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Hoja1");
            Sheet.Cells["A1"].Value = "IDProducto";
            Sheet.Cells["B1"].Value = "IDCatalogo";
            Sheet.Cells["C1"].Value = "Nombre";
            Sheet.Cells["D1"].Value = "Resumen";
            Sheet.Cells["E1"].Value = "Descripcion";
            Sheet.Cells["F1"].Value = "Marca";
            Sheet.Cells["G1"].Value = "Categoria";
            Sheet.Cells["H1"].Value = "TipoMonena";
            Sheet.Cells["I1"].Value = "Precio";
            Sheet.Cells["J1"].Value = "Descuento";
            Sheet.Cells["K1"].Value = "EtiquetaOferta";
            Sheet.Cells["L1"].Value = "Costo";
            Sheet.Cells["M1"].Value = "Tags";
            Sheet.Cells["N1"].Value = "Proveedor";
            Sheet.Cells["O1"].Value = "IsActive";
            Sheet.Cells["P1"].Value = "IsDeleted";
            Sheet.Cells["Q1"].Value = "SKU_S";
            Sheet.Cells["R1"].Value = "Stock_TallaS";
            Sheet.Cells["S1"].Value = "SKU_M";
            Sheet.Cells["T1"].Value = "Stock_TallaM";
            Sheet.Cells["U1"].Value = "SKU_L";
            Sheet.Cells["V1"].Value = "Stock_TallaL";
            Sheet.Cells["W1"].Value = "SKU_XL";
            Sheet.Cells["X1"].Value = "Stock_TallaXL";
            Sheet.Cells["Y1"].Value = "SKU_XXL";
            Sheet.Cells["Z1"].Value = "Stock_TallaXXL";
            Sheet.Cells["AA1"].Value = "Esp_Titulo1";
            Sheet.Cells["AB1"].Value = "Esp_Valor1";
            Sheet.Cells["AC1"].Value = "Esp_Titulo2";
            Sheet.Cells["AD1"].Value = "Esp_Valor2";
            Sheet.Cells["AE1"].Value = "Esp_Titulo3";
            Sheet.Cells["AF1"].Value = "Esp_Valor3";
            Sheet.Cells["AG1"].Value = "Esp_Titulo4";
            Sheet.Cells["AH1"].Value = "Esp_Valor4";
            Sheet.Cells["AI1"].Value = "Esp_Titulo5";
            Sheet.Cells["AJ1"].Value = "Esp_Valor5";

            //IMPRIMIR FILAS
            int row = 2;
            Sheet.Cells[string.Format("A{0}", row)].Value = "0";
            Sheet.Cells[string.Format("B{0}", row)].Value = "0";
            Sheet.Cells[string.Format("C{0}", row)].Value = "";
            Sheet.Cells[string.Format("D{0}", row)].Value = "";
            Sheet.Cells[string.Format("E{0}", row)].Value = "";
            Sheet.Cells[string.Format("F{0}", row)].Value = "";
            Sheet.Cells[string.Format("G{0}", row)].Value = "";
            Sheet.Cells[string.Format("H{0}", row)].Value = "1";            
            Sheet.Cells[string.Format("I{0}", row)].Value = "0";
            Sheet.Cells[string.Format("J{0}", row)].Value = "0";
            Sheet.Cells[string.Format("K{0}", row)].Value = "";
            Sheet.Cells[string.Format("L{0}", row)].Value = "0";
            Sheet.Cells[string.Format("M{0}", row)].Value = "";
            Sheet.Cells[string.Format("N{0}", row)].Value = "";
            Sheet.Cells[string.Format("O{0}", row)].Value = "1";
            Sheet.Cells[string.Format("P{0}", row)].Value = "0";

            //IMPRIMIR STOCK Y SKU POR TALLA
            Sheet.Cells[string.Format("Q{0}", row)].Value = "";
            Sheet.Cells[string.Format("R{0}", row)].Value = "";
            Sheet.Cells[string.Format("S{0}", row)].Value = "";
            Sheet.Cells[string.Format("T{0}", row)].Value = "";
            Sheet.Cells[string.Format("U{0}", row)].Value = "";
            Sheet.Cells[string.Format("V{0}", row)].Value = "";
            Sheet.Cells[string.Format("W{0}", row)].Value = "";
            Sheet.Cells[string.Format("X{0}", row)].Value = "";
            Sheet.Cells[string.Format("Y{0}", row)].Value = "";
            Sheet.Cells[string.Format("Z{0}", row)].Value = "";
            //IMPRIMIR ESPECIFICACIONES                               
            Sheet.Cells[string.Format("AA{0}", row)].Value = "";
            Sheet.Cells[string.Format("AB{0}", row)].Value = "";
            Sheet.Cells[string.Format("AC{0}", row)].Value = "";
            Sheet.Cells[string.Format("AD{0}", row)].Value = "";
            Sheet.Cells[string.Format("AE{0}", row)].Value = "";
            Sheet.Cells[string.Format("AF{0}", row)].Value = "";
            Sheet.Cells[string.Format("AG{0}", row)].Value = "";
            Sheet.Cells[string.Format("AH{0}", row)].Value = "";
            Sheet.Cells[string.Format("AI{0}", row)].Value = "";
            Sheet.Cells[string.Format("AJ{0}", row)].Value = "";
            Sheet.Cells[string.Format("AJ{0}", row)].Value = "";
            
            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ProductTemplate.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public void DownloadAccesorio()
        {
            DownloadProducts(eCommerceConstants.CATALOGO_ACCESORIO_ID);
        }

        public void DownloadProducts(int catalogId)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            MantenedorFinanciera m = new MantenedorFinanciera();

            List<Product> collection = ProductsService.Instance.ProductsByCatalogID(catalogId);

            ExcelPackage Ep = new ExcelPackage();

            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Hoja1");
            Sheet.Cells["A1"].Value = "IDProducto";
            Sheet.Cells["B1"].Value = "IDCatalogo";
            Sheet.Cells["C1"].Value = "Nombre";
            Sheet.Cells["D1"].Value = "Resumen";
            Sheet.Cells["E1"].Value = "Descripcion";
            Sheet.Cells["F1"].Value = "Marca";
            Sheet.Cells["G1"].Value = "Categoria";
            Sheet.Cells["H1"].Value = "TipoMonena";
            Sheet.Cells["I1"].Value = "Precio";
            Sheet.Cells["J1"].Value = "Descuento";
            Sheet.Cells["K1"].Value = "EtiquetaOferta";
            Sheet.Cells["L1"].Value = "Costo";
            Sheet.Cells["M1"].Value = "Tags";
            Sheet.Cells["N1"].Value = "Proveedor";
            Sheet.Cells["O1"].Value = "IsActive";
            Sheet.Cells["P1"].Value = "IsDeleted";
            Sheet.Cells["Q1"].Value = "SKU_S";
            Sheet.Cells["R1"].Value = "Stock_TallaS";
            Sheet.Cells["S1"].Value = "SKU_M";
            Sheet.Cells["T1"].Value = "Stock_TallaM";
            Sheet.Cells["U1"].Value = "SKU_L";
            Sheet.Cells["V1"].Value = "Stock_TallaL";
            Sheet.Cells["W1"].Value = "SKU_XL";
            Sheet.Cells["X1"].Value = "Stock_TallaXL";
            Sheet.Cells["Y1"].Value = "SKU_XXL";
            Sheet.Cells["Z1"].Value = "Stock_TallaXXL";
            Sheet.Cells["AA1"].Value = "Esp_Titulo1";
            Sheet.Cells["AB1"].Value = "Esp_Valor1";
            Sheet.Cells["AC1"].Value = "Esp_Titulo2";
            Sheet.Cells["AD1"].Value = "Esp_Valor2";
            Sheet.Cells["AE1"].Value = "Esp_Titulo3";
            Sheet.Cells["AF1"].Value = "Esp_Valor3";
            Sheet.Cells["AG1"].Value = "Esp_Titulo4";
            Sheet.Cells["AH1"].Value = "Esp_Valor4";
            Sheet.Cells["AI1"].Value = "Esp_Titulo5";
            Sheet.Cells["AJ1"].Value = "Esp_Valor5";
            int row = 2;
            foreach (var item in collection)
            {
                var currentLanguageRecord = item.ProductRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);
                var marca = MarcaService.Instance.GetMarcaByID(item.MarcaId);
                var categoria = CategoriesService.Instance.GetCategoryByID(item.CategoryID);
                var categoryRecord = categoria.CategoryRecords.FirstOrDefault(c => c.LanguageID == AppDataHelper.CurrentLanguage.ID);
                var isActive = item.IsActive ? 1 : 0;
                var etiquetaOferta = item.EtiquetaOferta != null ? item.EtiquetaOferta.ToString() : "";


                Sheet.Cells[string.Format("A{0}", row)].Value = item.ID.ToString();
                Sheet.Cells[string.Format("B{0}", row)].Value = item.CatalogoId.ToString();
                Sheet.Cells[string.Format("C{0}", row)].Value = currentLanguageRecord.Name.Trim();
                Sheet.Cells[string.Format("D{0}", row)].Value = currentLanguageRecord.Summary.Trim();
                Sheet.Cells[string.Format("E{0}", row)].Value = currentLanguageRecord.Description.Trim();
                Sheet.Cells[string.Format("F{0}", row)].Value = marca.Descripcion.Trim();
                Sheet.Cells[string.Format("G{0}", row)].Value = categoryRecord.Name.Trim();
                Sheet.Cells[string.Format("H{0}", row)].Value = item.TipoMoneda.ToString();
                Sheet.Cells[string.Format("I{0}", row)].Value = item.Price.ToString();
                Sheet.Cells[string.Format("J{0}", row)].Value = item.Discount.ToString();
                Sheet.Cells[string.Format("K{0}", row)].Value = etiquetaOferta;
                Sheet.Cells[string.Format("L{0}", row)].Value = item.Cost.ToString();

                Sheet.Cells[string.Format("M{0}", row)].Value = item.Tags.Trim();
                Sheet.Cells[string.Format("N{0}", row)].Value = item.Supplier.Trim();

                Sheet.Cells[string.Format("O{0}", row)].Value = isActive.ToString();
                Sheet.Cells[string.Format("P{0}", row)].Value = "0";

                //BUSCAR PRODUCTSTOCK DEL 1 AL 5
                List<int> TallaIDs = getTallasIDs();
                var productStocks = ProductStockService.Instance.GetProductStocksByTallaIDs(item.ID, TallaIDs);

                //IMPRIMIR STOCK Y SKU POR TALLA
                Sheet.Cells[string.Format("Q{0}", row)].Value = getSKUByTallaID(productStocks, eCommerceConstants.TALLA_S);
                Sheet.Cells[string.Format("R{0}", row)].Value = getStockByTallaID(productStocks, eCommerceConstants.TALLA_S);
                Sheet.Cells[string.Format("S{0}", row)].Value = getSKUByTallaID(productStocks, eCommerceConstants.TALLA_M);
                Sheet.Cells[string.Format("T{0}", row)].Value = getStockByTallaID(productStocks, eCommerceConstants.TALLA_M);
                Sheet.Cells[string.Format("U{0}", row)].Value = getSKUByTallaID(productStocks, eCommerceConstants.TALLA_L);
                Sheet.Cells[string.Format("V{0}", row)].Value = getStockByTallaID(productStocks, eCommerceConstants.TALLA_L);
                Sheet.Cells[string.Format("W{0}", row)].Value = getSKUByTallaID(productStocks, eCommerceConstants.TALLA_XL);
                Sheet.Cells[string.Format("X{0}", row)].Value = getStockByTallaID(productStocks, eCommerceConstants.TALLA_XL);
                Sheet.Cells[string.Format("Y{0}", row)].Value = getSKUByTallaID(productStocks, eCommerceConstants.TALLA_XXL);
                Sheet.Cells[string.Format("Z{0}", row)].Value = getStockByTallaID(productStocks, eCommerceConstants.TALLA_XXL);

                //IMPRIMIR ESPECIFICACIONES                
                List<ProductSpecification> productSpecifications = currentLanguageRecord.ProductSpecifications;

                Sheet.Cells[string.Format("AA{0}", row)].Value = getSpecification(productSpecifications, 0, "TITLE");
                Sheet.Cells[string.Format("AB{0}", row)].Value = getSpecification(productSpecifications, 0, "VALOR");
                Sheet.Cells[string.Format("AC{0}", row)].Value = getSpecification(productSpecifications, 1, "TITLE");
                Sheet.Cells[string.Format("AD{0}", row)].Value = getSpecification(productSpecifications, 1, "VALOR");
                Sheet.Cells[string.Format("AE{0}", row)].Value = getSpecification(productSpecifications, 2, "TITLE");
                Sheet.Cells[string.Format("AF{0}", row)].Value = getSpecification(productSpecifications, 2, "VALOR");
                Sheet.Cells[string.Format("AG{0}", row)].Value = getSpecification(productSpecifications, 3, "TITLE");
                Sheet.Cells[string.Format("AH{0}", row)].Value = getSpecification(productSpecifications, 3, "VALOR");
                Sheet.Cells[string.Format("AI{0}", row)].Value = getSpecification(productSpecifications, 4, "TITLE");
                Sheet.Cells[string.Format("AJ{0}", row)].Value = getSpecification(productSpecifications, 4, "VALOR");
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ProductsDataSet.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public string getSpecification(List<ProductSpecification> productSpecifications, int indice, string columna)
        {

            string valor = "";

            bool exist = productSpecifications.Count > indice;

            if (exist)
            {
                var specification = productSpecifications[indice];
                if (columna.Equals("TITLE"))
                {
                    valor = specification.Title.Trim();
                }
                if (columna.Equals("VALOR"))
                {
                    valor = specification.Value.Trim();
                }
            }
            return valor;
        }

        public string getSKUByTallaID(List<ProductStock> productStocks, int tallaID)
        {

            var sku = "";
            var obj = productStocks.FirstOrDefault(x => x.TallaID == tallaID);
            if (obj != null)
            {
                sku = obj.SKU.Trim();
            }
            return sku;
        }

        public string getStockByTallaID(List<ProductStock> productStocks, int tallaID)
        {

            var stock = "0";
            var obj = productStocks.FirstOrDefault(x => x.TallaID == tallaID);
            if (obj != null)
            {
                stock = obj.Stock.ToString();
            }
            return stock;
        }

        public List<int> getTallasIDs()
        {
            List<int> TallaIDs = new List<int>();
            TallaIDs.Add(eCommerceConstants.TALLA_S);
            TallaIDs.Add(eCommerceConstants.TALLA_M);
            TallaIDs.Add(eCommerceConstants.TALLA_L);
            TallaIDs.Add(eCommerceConstants.TALLA_XL);
            TallaIDs.Add(eCommerceConstants.TALLA_XXL);

            return TallaIDs;
        }

        public List<string> validarDatos(ProductRequest product, int filaExcel)
        {
            List<string> validacion = new List<string>();

            Product p = new Product();

            if (product.Nombre.Trim() == "")
            {
                validacion.Add(String.Format("El campo Nombre de la fila {0} está vacío.", filaExcel));
            }
            else
            {

                if (product.IDProducto > 0)
                {
                    p = ProductsService.Instance.GetProductByID(product.IDProducto);
                    if (p == null)
                    {
                        validacion.Add(String.Format("El campo IDProducto de la fila {0} no existe.", filaExcel));
                    }
                }

                if (product.IDCatalogo > 0)
                {
                    var catalogo = CatalogoService.Instance.GetCatalogoByID(product.IDCatalogo);
                    if (catalogo == null)
                    {
                        validacion.Add(String.Format("El campo IDCatalogo de la fila {0} no existe.", filaExcel));
                    }
                }

                if (product.IDProducto > 0)
                {
                    var prod = ProductsService.Instance.GetProductByID(product.IDProducto);
                    if (prod == null)
                    {
                        validacion.Add(String.Format("El campo IDProducto de la fila {0} no existe.", filaExcel));
                    }
                }

                Category categoria = CategoriesService.Instance.GetCategoryByNameAndByCatalogo(product.Categoria, product.IDCatalogo);
                if (categoria == null)
                {
                    validacion.Add(String.Format("El campo Categoria de la fila {0} no existe.", filaExcel));
                }

                Marca marca = MarcaService.Instance.GetMarcaByName(product.Marca);
                if (marca == null)
                {
                    validacion.Add(String.Format("El campo Marca de la fila {0} no existe.", filaExcel));
                }

                if (product.TipoMoneda != 1 && product.TipoMoneda != 2)
                {
                    validacion.Add(String.Format("El campo TipoMoneda de la fila {0} es incorrecto.", filaExcel));
                }


            }

            return validacion;
        }

        private static void TryToParseInt(string value)
        {
            int number;
            bool result = Int32.TryParse(value, out number);
            if (result)
            {
                Console.WriteLine("Converted '{0}' to {1}.", value, number);
            }
            else
            {
                if (value == null) value = "";
                Console.WriteLine("Attempted conversion of '{0}' failed.", value);
            }
        }
        public class CargaMasivaResponse
        {
            public List<string> data { get; set; }
        }

    }
}