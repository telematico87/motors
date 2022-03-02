using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Enums;
using eCommerce.Shared.Helpers;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class ModeloController : DashboardBaseController
    {
        // GET: Dashboard/Modelo
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            var pageSize = (int)RecordSizeEnums.Size10;

            ModelosListingViewModel model = new ModelosListingViewModel
            {
                SearchTerm = searchTerm
            };

            model.Modelos = ModeloService.Instance.SearchModelo(searchTerm, pageNo, pageSize, out int count);

            model.Pager = new Pager(count, pageNo, pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ModelosActionViewModels model = new ModelosActionViewModels();

            if (ID.HasValue)
            {
                var modelo = ModeloService.Instance.GetModeloByID(ID.Value);

                if (modelo == null) return HttpNotFound();

                model.ID = modelo.ID;
                model.Description = modelo.Description;
            }

            return View(model);
        }


        [HttpPost, ValidateInput(false)]
        public JsonResult Action(ModelosActionViewModels model)
        {
            JsonResult json = new JsonResult();

            try
            {
                if (model.ID > 0)
                {
                    var modelo = ModeloService.Instance.GetModeloByID(model.ID);

                    if (modelo == null)
                    {
                        throw new Exception("Dashboard.Modelo.Action.Validation.ModeloNotFound".LocalizedString());
                    }

                    modelo.ID = model.ID;
                    modelo.Description = model.Description;


                    if (!ModeloService.Instance.UpdateModelo(modelo))
                    {
                        throw new Exception("Dashboard.Modelo.Action.Validation.UnableToUpdateModelo".LocalizedString());
                    }

                    json.Data = new { Success = true };
                }
                else
                {
                    Modelo modelo = new Modelo
                    {
                        ID = model.ID,
                        Description = model.Description,

                    };

                    if (!ModeloService.Instance.SaveModelo(modelo))
                    {
                        throw new Exception("Dashboard.Modelo.Action.Validation.UnableToCreateModelo".LocalizedString());
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
                var operation = ModeloService.Instance.DeleteModelo(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Dashboard.Modelo.Action.Validation.UnableToDeleteModelo".LocalizedString() };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }
    }
}