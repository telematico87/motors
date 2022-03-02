using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Enums;
using eCommerce.Shared.Helpers;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Web.ViewModels;
using System;
using System.Web.Mvc;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class MedidaController : DashboardBaseController
    {
        // GET: Dashboard/Medida
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            var pageSize = (int)RecordSizeEnums.Size10;

            MedidasListingViewModel model = new MedidasListingViewModel
            {
                SearchTerm = searchTerm
            };

            model.Medidas = MedidaService.Instance.SearchMedida(searchTerm, pageNo, pageSize, out int count);

            model.Pager = new Pager(count, pageNo, pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            MedidasActionViewModels model = new MedidasActionViewModels();

            if (ID.HasValue)
            {
                var medida = MedidaService.Instance.GetMedidaByID(ID.Value);

                if (medida == null) return HttpNotFound();

                model.ID = medida.ID;
                model.Description = medida.Description;
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
                    var medida = MedidaService.Instance.GetMedidaByID(model.ID);

                    if (medida == null)
                    {
                        throw new Exception("Dashboard.Medida.Action.Validation.MedidaNotFound".LocalizedString());
                    }

                    medida.ID = model.ID;
                    medida.Description = model.Description;


                    if (!MedidaService.Instance.UpdateMedida(medida))
                    {
                        throw new Exception("Dashboard.Medida.Action.Validation.UnableToUpdateMedida".LocalizedString());
                    }

                    json.Data = new { Success = true };
                }
                else
                {
                    Medida medida = new Medida
                    {
                        ID = model.ID,
                        Description = model.Description,

                    };

                    if (!MedidaService.Instance.SaveMedida(medida))
                    {
                        throw new Exception("Dashboard.Medida.Action.Validation.UnableToCreateMedida".LocalizedString());
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
                var operation = MedidaService.Instance.DeleteMedida(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "Dashboard.Medida.Action.Validation.UnableToDeleteMedida".LocalizedString() };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }
    }
}