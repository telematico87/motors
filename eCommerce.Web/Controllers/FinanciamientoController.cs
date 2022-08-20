using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static eCommerce.Web.ViewModels.FinanciamientosViewModels;

namespace eCommerce.Web.Controllers
{
    public class FinanciamientoController : PublicBaseController
    {
        
        [HttpGet]
        public ActionResult FinanciamientoEfectiva()
        { 
            return View(); 
        }

        [HttpGet]
        public ActionResult listarDepartamentos()
        {
            //FinanciamientosViewModels model = new FinanciamientosViewModels();
            var model = UbigeoServices.Instance.ListarDepartamento();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listarProvincias(string departamento)
        {
            //FinanciamientosViewModels model = new FinanciamientosViewModels();
            var model = UbigeoServices.Instance.ListarProvincia(departamento);
            return Json(model, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult listarMarca()
        { 
            //FinanciamientosViewModels model = new FinanciamientosViewModels();
            var model = MarcaService.Instance.ListarMarca();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listarEstadoCivil()
        { 
            var model = new MantenedorFinanciera().ListarEstadoCivil();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listaTipoVivienda()
        {
            var model = new MantenedorFinanciera().ListarTipoVivienda();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listaRangoIngreso()
        {
            var model = new MantenedorFinanciera().ListarRangoIngreso();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listaInteresCompra()
        {
            var model = new MantenedorFinanciera().ListarInteresCompra();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listaMontoFinanciar()
        {
            var model = new MantenedorFinanciera().ListarMontoFinanciar();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult FinanciamientoSantander()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GuardarFinanciamiento(FinanciamientosViewModels model)
        {
            JsonResult result = new JsonResult();

            try
            {
                var dpto = model.Departamento.Split('|');
                var dptoNombre = dpto[1];

                var financ = new Financiamiento

                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Celular = model.Celular,
                    Correo = model.Correo,
                    TipoDocumento = model.TipoDocumento,
                    NroDocumento = model.NroDocumento,
                    InteresCompra = model.InteresCompra,
                    Departamento = dptoNombre,
                    Provincia = model.Provincia,
                    Distrito = model.Distrito,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    TipoVivienda = model.TipoVivienda,
                    SituacionLaboral = model.SituacionLaboral,
                    SituacionSentimental = model.SituacionSentimental,
                    RangoIngreso = model.RangoIngreso,
                    PoliticaPrivacidad = model.PoliticaPrivacidad,
                    AceptoComunicaciones = model.AceptoComunicaciones,
                    MontoFinanciar = model.MontoFinanciar,
                    TipoFinanciera = model.TipoFinanciera,
                    TieneInicial = model.TieneInicial,
                    MontoInicial = model.MontoInicial
                };

                var res = FinanciamientoService.Instance.SaveFinanciamiento(financ);

                result.Data = new { Success = res };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }





    }
}