using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Enums;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class CatalogoController : DashboardBaseController
    {

        public ActionResult Index(string searchTerm, int? pageNo)
        {
            var pageSize = (int)RecordSizeEnums.Size10;

            CatalogosListingViewModels model = new CatalogosListingViewModels
            {
                SearchTerm = searchTerm
            };

            model.Catalogos = CatalogoService.Instance.SearchCatalogo(searchTerm, pageNo, pageSize, out int count);

            model.Pager = new Pager(count, pageNo, pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CatalogosActionViewModels model = new CatalogosActionViewModels();

            if (ID.HasValue)
            {
                var catalogo = CatalogoService.Instance.GetCatalogoByID(ID.Value);

                if (catalogo == null) return HttpNotFound();

                model.ID = catalogo.ID;
                model.Description = catalogo.Description;
            }

            return View(model);
        }


        [HttpPost, ValidateInput(false)]
        public JsonResult Action(CatalogosActionViewModels model)
        {
            JsonResult json = new JsonResult();

            try
            {
                if (model.ID > 0)
                {
                    var catalogo = CatalogoService.Instance.GetCatalogoByID(model.ID);

                    if (catalogo == null)
                    {
                        throw new Exception("Catalogo no encontrado");
                    }

                    catalogo.ID = model.ID;
                    catalogo.Description = model.Description;


                    if (!CatalogoService.Instance.UpdateCatalogo(catalogo))
                    {
                        throw new Exception("No se puede actualizar el catalogo");
                    }

                    json.Data = new { Success = true };
                }
                else
                {
                    Catalogo catalogo = new Catalogo
                    {
                        ID = model.ID,
                        Description = model.Description,

                    };

                    if (!CatalogoService.Instance.SaveCatalogo(catalogo))
                    {
                        throw new Exception("No se puede crear el catalogo");
                    }

                    json.Data = new { Success = true };
                }
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
                var operation = CatalogoService.Instance.DeleteCatalogo(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "No se puede eliminar el catalogo" };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }

    }
}
