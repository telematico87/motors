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
    public class FinanciamientoController : DashboardBaseController
    {
       
        public ActionResult Index(string searchTerm, int? pageNo)
        {

            try
            {
                var pageSize = (int)RecordSizeEnums.Size10;

                FinanciamientosListingViewModels model = new FinanciamientosListingViewModels
                {
                    SearchTerm = searchTerm
                };

                model.Financiamientos = FinanciamientoService.Instance.BuscarFinanciamiento(searchTerm, pageNo, pageSize, out int count);

                model.Pager = new Pager(count, pageNo, pageSize);

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            FinanciamientosActionViewModels model = new FinanciamientosActionViewModels();

            if (ID.HasValue)
            {
                var financi = FinanciamientoService.Instance.GetFinanciamientoByID(ID.Value);

                if (financi == null) return HttpNotFound();

                model.ID = financi.ID;
                model.Nombre = financi.Nombre;
            }

            return View(model);
        }
    }
}