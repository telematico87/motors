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
        // GET: Financiamiento

        public ActionResult Index()
        {
            FinanciamientosViewModels model = new FinanciamientosViewModels
            {
                Nombre = "Juan Leder",
                Apellido = "Lozano Pinchi",
                TipoFinanciera = 1
            };
            return View("FinanciamientoEfectiva", model);
        }

        [HttpGet]
        public ActionResult FinanciamientoEfectiva()
        {

            FinanciamientosViewModels model = new FinanciamientosViewModels();
            //Marca model = new Marca();

            model.listaMarca = MarcaService.Instance.ListarMarca();

            //model.listaEstadoCivil = new EstadoCivil().ListarEstadoCivil();

            //ViewBag.Lista = model.listaEstadoCivil;
            return View();

            FinanciamientosViewModels model = new FinanciamientosViewModels { 
                Nombre = "Juan Leder",
                Apellido = "Lozano Pinchi",
                TipoFinanciera = 1
            };
            //model.Nombre = "";
            //model.Apellido = "";
            //finacefe.Celular = "";
            //finacefe.Correo = "";
            //finacefe.TipoDocumento = 1;
            //finacefe.NroDocumento = "";
            //finacefe.InteresCompra = 0;
            //finacefe.Departamento = "";
            //finacefe.Provincia = "";
            //finacefe.Distrito = "";
            //finacefe.Modelo = "";
            //finacefe.TipoVivienda = 0;
            //finacefe.SituacionLaboral = "";
            //finacefe.SituacionSentimental = "";
            //finacefe.RangoIngreso = 0;
            //finacefe.Marca = "";
            //finacefe.PoliticaPrivacidad = true;
            //finacefe.AceptoComunicaciones = true;
            //finacefe.MontoFinanciar = 0;
            //model.TipoFinanciera = 0;

            //finacefe.listaEstadoCivil = new MantenedorFinanciera().ListarEstadoCivil();
            //finacefe.listaInteresCompra = new MantenedorFinanciera().ListarInteresCompra();
            //finacefe.listaRangoIngreso = new MantenedorFinanciera().ListarRangoIngreso();
            //finacefe.listaTipoVivienda = new MantenedorFinanciera().ListarTipoVivienda();
            //ViewBag.Lista = finacefe;
            return View(model);

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
            var model = new EstadoCivil().ListarEstadoCivil();
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
                var financ = new Financiamiento
                {

                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    //Celular = model.Celular,
                    //Correo = model.Correo,
                    //TipoDocumento = model.TipoDocumento,
                    //NroDocumento = model.NroDocumento,
                    //InteresCompra = model.InteresCompra,
                    //Departamento = model.Departamento,
                    //Provincia = model.Provincia,
                    //Distrito = model.Distrito,
                    //Marca = model.Marca,
                    //Modelo = model.Modelo,
                    //TipoVivienda = model.TipoVivienda,
                    //SituacionLaboral = model.SituacionLaboral,
                    //SituacionSentimental = model.SituacionSentimental,
                    //RangoIngreso = model.RangoIngreso,
                    //PoliticaPrivacidad = model.PoliticaPrivacidad,
                    //AceptoComunicaciones = model.AceptoComunicaciones,
                    //MontoFinanciar = model.MontoFinanciar,
                    TipoFinanciera = model.TipoFinanciera
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