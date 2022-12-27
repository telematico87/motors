using eCommerce.Shared.Enums;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}