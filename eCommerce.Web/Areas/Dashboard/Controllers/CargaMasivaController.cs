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

        public FileResult DownloadExcel()
        {
            string path = "~/Content/bm3/excel-format/ProductTemplate1.xlsx";
            return File(path, "application/vnd.ms-excel", "ProductTemplate1.xlsx");
        }

        [HttpPost]
        public JsonResult UploadExcel(ProductRequest products, HttpPostedFileBase FileUpload)
        {

            JsonResult json = new JsonResult();

            List<string> data = new List<string>();
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

                    var contador = 1;
                    foreach (var model in artistAlbums)
                    {
                        try
                        {
                            if (model.IDCatalogo != 0)
                            {
                                //Obtener Categoria o crear una nueva Categoria
                                int idCategoria = Int32.Parse(model.Categoria);
                                int idMarca = Int32.Parse(model.Marca);

                                Product product = new Product
                                {
                                    CategoryID = idCategoria,
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
                                    MarcaId = idMarca,
                                    CatalogoId = model.IDCatalogo,
                                    TipoMoneda = model.TipoMoneda,
                                    EtiquetaOferta = model.EtiquetaOferta
                                };

                                product.IsActive = true;

                                if (!ProductsService.Instance.SaveProduct(product))
                                {
                                    throw new Exception("Error al insertar producto en la línea: " + contador);
                                }

                                var currentLanguageRecord = new ProductRecord
                                {
                                    ProductID = product.ID,
                                    LanguageID = AppDataHelper.CurrentLanguage.ID,
                                    Name = model.Nombre,
                                    Summary = model.Resumen,
                                    Description = model.Descripcion,

                                    ModifiedOn = DateTime.Now
                                };

                                var result = ProductsService.Instance.SaveProductRecord(currentLanguageRecord);

                                if (!result)
                                {
                                    throw new Exception("Dashboard.Products.Action.Validation.UnableToCreateProductRecord".LocalizedString());
                                }

                            }
                            else
                            {
                                data.Add("<ul>");
                                if (model.IDCatalogo == 0) data.Add("<li> El campo IDCatalogo no tiene un valor válido</li>");                                
                                data.Add("</ul>");
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

                        contador++;
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

        public Boolean validarExcel(ProductRequest p) {

            List<string> data = new List<string>();

            if (p.IDCatalogo == 0)
            {
                data.Add("<li>Please choose Excel file</li>");
            }


            return false;
        }


    }
}