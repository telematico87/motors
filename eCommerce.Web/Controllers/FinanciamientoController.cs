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
        [HttpGet]
        public ActionResult FinanciamientoEfectiva()
        {
            FinanciamientosViewModels finacefe = new FinanciamientosViewModels(); 
            finacefe.listaEstadoCivil = new EstadoCivil().ListarEstadoCivil();
            ViewBag.Lista = finacefe;
            return View();
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
                    Celular = model.Celular,
                    Correo = model.Correo,
                    TipoDocumento = model.TipoDocumento,
                    NroDocumento = model.NroDocumento,
                    InteresCompra = model.InteresCompra,
                    Departamento = model.Departamento,
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