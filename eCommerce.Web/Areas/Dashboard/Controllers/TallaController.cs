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
    public class TallaController :  DashboardBaseController
    {
        // GET: Dashboard/Talla
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            var pageSize = (int)RecordSizeEnums.Size10;

            TallasListingViewModel model = new TallasListingViewModel
            {
                SearchTerm = searchTerm
            };

            model.Tallas = TallaService.Instance.SearchTalla(searchTerm, pageNo, pageSize, out int count);

            model.Pager = new Pager(count, pageNo, pageSize);

            return View(model);
        }


        [HttpGet]
        public ActionResult Action(int? ID)
        {
            TallasActionViewModels model = new TallasActionViewModels();

            if (ID.HasValue)
            {
                var talla = TallaService.Instance.GetTallaByID(ID.Value);

                if (talla == null) return HttpNotFound();

                model.ID = talla.ID;
                model.Description = talla.Description;
            }

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult Action(TallasActionViewModels model)
        {
            JsonResult json = new JsonResult();

            try
            {
                if (model.ID > 0)
                {
                    var talla = TallaService.Instance.GetTallaByID(model.ID);

                    if (talla == null)
                    {
                        throw new Exception("Dashboard.Talla.Action.Validation.TallaNotFound".LocalizedString());
                    }

                    talla.ID = model.ID;
                    talla.Description = model.Description;


                    if (!TallaService.Instance.UpdateTalla(talla))
                    {
                        throw new Exception("Dashboard.Talla.Action.Validation.UnableToUpdateTalla".LocalizedString());
                    }

                    json.Data = new { Success = true };
                }
                else
                {
                    Talla talla = new Talla
                    {
                        ID = model.ID,
                        Description = model.Description,

                    };

                    if (!TallaService.Instance.SaveTalla(talla))
                    {
                        throw new Exception("Dashboard.Talla.Action.Validation.UnableToCreateTalla".LocalizedString());
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
                var operation = TallaService.Instance.DeleteTalla(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Dashboard.Talla.Action.Validation.UnableToDeleteTalla".LocalizedString() };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }
    }
}