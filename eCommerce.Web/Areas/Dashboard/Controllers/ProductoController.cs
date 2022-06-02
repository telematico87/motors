using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        public JsonResult Hola(int catalogo)
        {
            //FinanciamientosViewModels model = new FinanciamientosViewModels();
            Prueba pr = new Prueba();
            pr.resultado = "hola";
            return Json(pr, JsonRequestBehavior.AllowGet);
        }

        public class Prueba
        {
            public string resultado { get; set; }
        }

        [HttpGet]
        public ActionResult Hola2(int catalogo)
        {
            //FinanciamientosViewModels model = new FinanciamientosViewModels();
            Prueba pr = new Prueba();
            pr.resultado = "hola";
            return Json(pr, JsonRequestBehavior.AllowGet);
        }
    }
}