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
using eCommerce.Entities.Response;
using eCommerce.Shared.Helpers;
using System.Web.Management;
using eCommerce.Shared.Commons;
using eCommerce.Web.ViewModels;
using WebGrease.Css.Ast.Selectors;
using System.Text;
using Microsoft.Ajax.Utilities;
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

        //public FilePathResult GetTemplateFile(string fileName)
        //{
        //    return File("/CargaMasiva/DownloadExcel/ProductTemplate", "multipart/form-data", fileName);
        //}

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
                    var artistAlbums = from a in excelFile.Worksheet<ProductRequest>(sheetName) select a;

                    var filaExcel = 1;
                    foreach (var model in artistAlbums)
                    {
                        try
                        {
                            List<string> data = new List<string>();
                            data = validarDatos(model, filaExcel);

                            if (data.Count == 0)
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
                                List<ProductSpecification> productSpecifications = new List<ProductSpecification>();
                                                                
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

                            }
                            else
                            {                                                                
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
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

                    json.Data = new { Success = true };
                    return json;
                    //return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format
                    data.Add("<ul>");
                    data.Add("<li>Only Excel file format is allowed</li>");
                    data.Add("</ul>");
                    data.ToArray();

                    json.Data = new { Success = false, Data = data };

                    return json;

                    //return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li>Please choose Excel file</li>");
                data.Add("</ul>");
                data.ToArray();

                json.Data = new { Success = false, Data = data };

                return json;

                //return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        private List<string> validarDatos(ProductRequest model)
        {
            throw new NotImplementedException();
        }

        public void DownloadAccesorio() {
            DownloadProducts(eCommerceConstants.CATALOGO_ACCESORIO_ID);
        }        

        public void DownloadProducts(int catalogId)
        {
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            MantenedorFinanciera m = new MantenedorFinanciera();

            List<Product> collection = ProductsService.Instance.ProductsByCatalogID(catalogId);

            ExcelPackage Ep = new ExcelPackage();

            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
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
            Sheet.Cells["M1"].Value = "SKU";
            Sheet.Cells["N1"].Value = "Tags";
            Sheet.Cells["O1"].Value = "Proveedor";
            Sheet.Cells["P1"].Value = "Stock";
            Sheet.Cells["Q1"].Value = "IsActive";
            Sheet.Cells["R1"].Value = "IsDeleted";
            Sheet.Cells["S1"].Value = "Especificaciones";            
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
                Sheet.Cells[string.Format("M{0}", row)].Value = item.SKU.Trim();
                Sheet.Cells[string.Format("N{0}", row)].Value = item.Tags.Trim();
                Sheet.Cells[string.Format("O{0}", row)].Value = item.Supplier.Trim();
                Sheet.Cells[string.Format("P{0}", row)].Value = item.StockQuantity.ToString();
                Sheet.Cells[string.Format("Q{0}", row)].Value = isActive.ToString();
                Sheet.Cells[string.Format("R{0}", row)].Value = "0";
                Sheet.Cells[string.Format("S{0}", row)].Value = "";
               
                //Sheet.Cells[string.Format("T{0}", row)].Style.Numberformat.Format = "dd-MM-yyyy";
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ProductsDataSet.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        public List<string> validarDatos(ProductRequest product, int filaExcel) {

            List<string> validacion = new List<string>();

            Product p = new Product();

            if (product.IDProducto > 0) {
                p = ProductsService.Instance.GetProductByID(product.IDProducto);
                if (p == null) {
                    validacion.Add(String.Format("El campo IDProducto de la fila {0} no existe.", filaExcel));                    
                }            
            }

            if (product.IDCatalogo > 0 ) {
                var catalogo = CatalogoService.Instance.GetCatalogoByID(product.IDCatalogo);
                if (catalogo == null) {
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
            if (marca == null) {
                validacion.Add(String.Format("El campo Marca de la fila {0} no existe.", filaExcel));
            }

            if (product.TipoMoneda != 1 && product.TipoMoneda != 2) {
                validacion.Add(String.Format("El campo TipoMoneda de la fila {0} es incorrecto.", filaExcel));
            }                       
            return validacion;
        }

        

        public Boolean validarExcel(ProductRequest p) {

            List<string> data = new List<string>();

            if (p.IDCatalogo == 0)
            {
                data.Add("<li>Please choose Excel file</li>");
            }


            return false;
        }


        public ActionResult CargaMasivaResult(CargaMasivaViewModel model)
        {            
            return PartialView("HomeMarcasMoto", model);
        }

    }
}