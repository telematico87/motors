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
    public class ColorController : DashboardBaseController
    {
        // GET: Dashboard/Color
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            var pageSize = (int)RecordSizeEnums.Size10;

            ColorsListingViewModel model = new ColorsListingViewModel
            {
                SearchTerm = searchTerm
            };

            model.Colors = ColorService.Instance.SearchColor(searchTerm, pageNo, pageSize, out int count);

            model.Pager = new Pager(count, pageNo, pageSize);

            return View(model);
        }


        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ColorsActionViewModels model = new ColorsActionViewModels();

            if (ID.HasValue)
            {
                var color = ColorService.Instance.GetColorByID(ID.Value);

                if (color == null) return HttpNotFound();

                model.ID = color.ID;
                model.Description = color.Description; 
            }

            return View(model);
        }


        [HttpPost, ValidateInput(false)]
        public JsonResult Action(ColorsActionViewModels model)
        {
            JsonResult json = new JsonResult();

            try
            {
                if (model.ID > 0)
                {
                    var color = ColorService.Instance.GetColorByID(model.ID);

                    if (color == null)
                    {
                        throw new Exception("Dashboard.Color.Action.Validation.ColorNotFound".LocalizedString());
                    }

                    color.ID = model.ID;
                    color.Description = model.Description;
                    color.Valor = model.Valor;
                 
                    if (!ColorService.Instance.UpdateColor(color))
                    {
                        throw new Exception("Dashboard.Color.Action.Validation.UnableToUpdateColor".LocalizedString());
                    }
                    json.Data = new { Success = true };
                }
                else
                {
                    Color color = new Color
                    {
                        ID = model.ID,
                        Description = model.Description,
                        Valor = model.Valor
                };

                    if (!ColorService.Instance.SaveColor(color))
                    {
                        throw new Exception("Dashboard.Color.Action.Validation.UnableToCreateColor".LocalizedString());
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
                var operation = ColorService.Instance.DeleteColor(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Dashboard.Color.Action.Validation.UnableToDeleteColor".LocalizedString() };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }
    }
}