using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Extensions;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Shared.Helpers;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Shared.Enums;
using static eCommerce.Web.Areas.Dashboard.Controllers.ProductsController;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class CategoriesController : DashboardBaseController
    {
        public ActionResult Index(int? parentCategoryID, string searchTerm, int? pageNo)
        {
            var recordSize = (int)RecordSizeEnums.Size10;

            CategoriesListingViewModel model = new CategoriesListingViewModel
            {
                ParentCategoryID = parentCategoryID,
                SearchTerm = searchTerm,

                ParentCategories = CategoriesService.Instance.GetAllTopLevelCategories()
            };

            model.Categories = CategoriesService.Instance.SearchCategories(parentCategoryID, searchTerm, pageNo, recordSize, out int count);
            model.Catalogos = CatalogoService.Instance.GetCatalogos();
            model.Pager = new Pager(count, pageNo, recordSize);

            return View(model);
        }
                
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CategoryActionViewModel model = new CategoryActionViewModel();
            
            if (ID.HasValue)
            {
                var category = CategoriesService.Instance.GetCategoryByID(ID.Value);

                if (category == null) return HttpNotFound();

                var currentLanguageRecord = category.CategoryRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);

                currentLanguageRecord = currentLanguageRecord ?? new CategoryRecord();

                model.CategoryID = category.ID;
                model.ParentCategoryID = category.ParentCategoryID.HasValue ? category.ParentCategoryID.Value : 0;
                model.isFeatured = category.isFeatured;
                model.SanitizedName = category.SanitizedName;
                model.Picture = category.Picture;
                model.PictureID = category.PictureID;
                model.PictureMovil = category.PictureMovil;
                model.PictureMovilID = category.PictureMovilID;

                model.CategoryRecordID = currentLanguageRecord.ID;
                model.Name = currentLanguageRecord.Name;
                model.Description = currentLanguageRecord.Description;
                model.Summary = currentLanguageRecord.Summary;
                model.CatalogoID = category.CatalogoID;
            }

            model.Categories = CategoriesService.Instance.GetCategories();
            model.Catalogos = CatalogoService.Instance.GetCatalogos();
            

            return View(model);
        }
        
        
        [HttpPost]        
        public JsonResult ListarCategoriasPorCatalogo(List<string> ids)
        {
            CatalogoCategoryResponse response = new CatalogoCategoryResponse();
            //string[] ids = CatalogoIDs.Split(',');

            List<CategoryResponse> listaResp = new List<CategoryResponse>();
            
            if (ids.Count > 0)
            {
                for (var i = 0; i < ids.Count; i++)
                {
                    int CatalogoID = Int32.Parse(ids[i]);
                    var listaCategorias = CatalogoCategoriaService.Instance.SearchCategoriesByCatalogoID(CatalogoID);

                    if (listaCategorias.Count() > 0)
                    {
                        
                    }

                    listaCategorias.ForEach(x => {
                        var category = CategoriesService.Instance.GetCategoryByID(x.CategoriaId);

                        if (category != null) {

                            var currentLanguageCategoryRecord = category.CategoryRecords.FirstOrDefault(r => r.LanguageID == AppDataHelper.CurrentLanguage.ID);
                            CategoryResponse cat = new CategoryResponse();
                            cat.CategoriaID = category.ID;
                            cat.Nombre = currentLanguageCategoryRecord.Name;
                            listaResp.Add(cat);                            
                        }

                    });                       
                }
            }
            //categorias.Sort((x, y) => x.SanitizedName.CompareTo(y.SanitizedName));
            //

            response.Categorias = listaResp;

            int temanio = response.Categorias.Count;

            return Json(response);            
        }

        [HttpPost]
        public JsonResult Action(CategoryActionViewModel model)
        {
            JsonResult json = new JsonResult();

            try
            {
                if (model.CategoryID > 0)
                {
                    var category = CategoriesService.Instance.GetCategoryByID(model.CategoryID);

                    if (category == null)
                    {
                        throw new Exception("Dashboard.Categories.Action.Validation.CategoryNotFound".LocalizedString());
                    }

                    if (model.ParentCategoryID > 0)
                    {
                        category.ParentCategoryID = model.ParentCategoryID;
                    }
                    else
                    {
                        category.ParentCategoryID = null;
                        category.ParentCategory = null;
                    }

                    category.PictureID = model.PictureID;
                    category.PictureMovilID = model.PictureMovilID;
                    category.isFeatured = model.isFeatured;
                    category.SanitizedName = !string.IsNullOrEmpty(model.SanitizedName) ? model.SanitizedName : model.Name.SanitizeLowerString();
                    category.CatalogoID = 0;

                    category.ModifiedOn = DateTime.Now;
                                        
                    if(!CategoriesService.Instance.UpdateCategory(category))
                    {
                        throw new Exception("Dashboard.Categories.Action.Validation.UnableToUpdateCategory".LocalizedString());
                    }

                    var currentCategoryRecord = category.CategoryRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);

                    var isNewRecord = false;

                    if (currentCategoryRecord == null)
                    {
                        currentCategoryRecord = new CategoryRecord();
                        isNewRecord = true;
                    }

                    currentCategoryRecord.CategoryID = category.ID;                    
                    currentCategoryRecord.LanguageID = AppDataHelper.CurrentLanguage.ID;
                    currentCategoryRecord.Name = model.Name;
                    currentCategoryRecord.Description = model.Description;
                    currentCategoryRecord.Summary = model.Summary;

                    currentCategoryRecord.ModifiedOn = DateTime.Now;

                    var result = false;
                    if (isNewRecord)
                    {
                        result = CategoriesService.Instance.SaveCategoryRecord(currentCategoryRecord);
                    }
                    else
                    {
                        result = CategoriesService.Instance.UpdateCategoryRecord(currentCategoryRecord);
                    }

                    if (!result)
                    {
                        throw new Exception("Dashboard.Categories.Action.Validation.UnableToUpdateCategoryRecord".LocalizedString());
                    }

                    //Guardar Catalogo(s)
                    var ids = model.CatalogoIDs;
                    if (ids.Count > 0)
                    {

                        for (var i = 0; i < ids.Count; i++)
                        {

                            CatalogoCategoria catalogos = new CatalogoCategoria();

                            catalogos.CatalogoId = Int32.Parse(ids[i]);
                            catalogos.CategoriaId = model.CategoryID;
                            catalogos.ModifiedOn = DateTime.Now;
                            catalogos.IsActive = true;

                            if (!CatalogoCategoriaService.Instance.SaveCatalogoCategoria(catalogos))
                            {
                                throw new Exception("No se pudo actualizar los Catalogos para esta Categoria");
                            }
                        }
                    }
                }
                else
                {
                    var category = new Category();

                    if (model.ParentCategoryID > 0)
                    {
                        category.ParentCategoryID = model.ParentCategoryID;
                    }
                    else
                    {
                        category.ParentCategoryID = null;
                        category.ParentCategory = null;
                    }

                    category.PictureID = model.PictureID;
                    category.PictureMovilID = model.PictureMovilID;
                    category.isFeatured = model.isFeatured;
                    category.SanitizedName = !string.IsNullOrEmpty(model.SanitizedName) ? model.SanitizedName : model.Name.SanitizeLowerString();
                    category.CatalogoID = 0;

                    var currentLanguageCategoryRecord = new CategoryRecord
                    {
                        Category = category,
                        LanguageID = AppDataHelper.CurrentLanguage.ID,
                        Name = model.Name,
                        Description = model.Description,
                        Summary = model.Summary,
                        ModifiedOn = DateTime.Now
                    };

                    var result = CategoriesService.Instance.SaveCategoryRecord(currentLanguageCategoryRecord);

                    if (!result)
                    {
                        throw new Exception("Dashboard.Categories.Action.Validation.UnableToCreateCategory".LocalizedString());
                    }

                    //Guardar Catalogo(s)
                    //string[] ids = model.CatalogoIDs.Split(',');
                    var ids = model.CatalogoIDs;
                    if (ids.Count > 0)
                    {
                        int idCategoria = currentLanguageCategoryRecord.Category.ID;

                        for (var i = 0; i < ids.Count; i++)
                        {

                            CatalogoCategoria catalogos = new CatalogoCategoria();

                            catalogos.CatalogoId = Int32.Parse(ids[i]);
                            catalogos.CategoriaId = idCategoria;
                            catalogos.ModifiedOn = DateTime.Now;
                            catalogos.IsActive = true;

                            if (!CatalogoCategoriaService.Instance.SaveCatalogoCategoria(catalogos))
                            {
                                throw new Exception("No se pudo actualizar los Catalogos para esta Categoria");
                            }
                        }
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

        [HttpPost]
        public JsonResult Delete(int ID)
        {
            JsonResult result = new JsonResult();

            try
            {
                var operation = CategoriesService.Instance.DeleteCategory(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Dashboard.Categories.Action.Validation.UnableToDeleteCategory".LocalizedString() };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            
            return result;
        }

        public class CatalogoCategoryResponse
        {
            public List<CategoryResponse> Categorias { get; set; }            
        }

        public class CategoryResponse
        {
            public int CategoriaID { get; set; }
            public string Nombre { get; set; }
        }
    }
}