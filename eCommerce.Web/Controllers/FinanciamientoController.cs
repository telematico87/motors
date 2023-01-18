using eCommerce.Entities;
using eCommerce.Entities.Response;
using eCommerce.Services;
using eCommerce.Shared.Commons;
using eCommerce.Shared.Helpers;
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
        public ActionResult FormularioRegistro()
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
            var model = MarcaService.Instance.GetMarcaByCatalogoID(eCommerceConstants.CATALOGO_MOTO_ID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listarEstadoCivil()
        { 
            var model = new MantenedorFinanciera().ListarEstadoCivil();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult listarAntiguedadLaboral()
        { 
            var model = new MantenedorFinanciera().ListarAntiguedadLaboral();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listarFinanciera()
        {
            var model = new MantenedorFinanciera().ListarFinancieras();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult listaTipoVivienda()
        {
            var model = new MantenedorFinanciera().ListarTipoVivienda();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult listaSituacionLaboral()
        {
            var model = new MantenedorFinanciera().ListarSituacionLaboral();
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
        public ActionResult listaTipoDocumento()
        {
            var model = new MantenedorFinanciera().ListarTipoDocumento();
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
                DateTime localDate = DateTime.Now;

                decimal tipoCambio = TipoCambioService.Instance.GetTypeUltimateChanged();
                var moto = ProductsService.Instance.GetProductByID(model.IDModelo);
                decimal amountFinance = 0;
                decimal priceFinal = 0;
                if (moto != null) {                    
                    var priceInitial = moto.Discount.HasValue && moto.Discount.Value > 0 ? moto.Discount.Value : moto.Price;
                    priceFinal = moto.TipoMoneda == 2 ? priceInitial * tipoCambio : priceInitial;
                    amountFinance = (priceFinal - model.MontoInicial);
                }
               
                DateTime fechaNacimiento = Convert.ToDateTime(model.FechaNacimiento);

                var financ = new Financiamiento
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    FechaNacimiento = fechaNacimiento,
                    Celular = model.Celular,
                    Correo = model.Correo,
                    TipoDocumento = model.TipoDocumento,
                    NroDocumento = model.NroDocumento,
                    SituacionSentimental = model.SituacionSentimental,

                    InteresCompra = model.InteresCompra,
                    Departamento = dptoNombre,
                    Provincia = model.Provincia,
                    Distrito = model.Distrito,
                    TipoVivienda = model.TipoVivienda,
                    IDSituacionLaboral = model.IDSituacionLaboral,
                    AntiguedadLaboral = model.AntiguedadLaboral,

                    IDMarca = model.IDMarca,
                    Marca = model.Marca,
                    IDModelo = model.IDModelo,
                    Modelo = model.Modelo,
                    Precio = priceFinal,
                    IngresoNeto = model.IngresoNeto,
                    MontoInicial = model.MontoInicial,
                    MontoAFinanciar = amountFinance,

                    Ocupacion = model.Ocupacion,
                    TipoFinanciera = model.TipoFinanciera,
                    PoliticaPrivacidad = model.PoliticaPrivacidad,
                    AceptoComunicaciones = model.AceptoComunicaciones,

                    RangoIngreso = model.RangoIngreso,                    
                    MontoFinanciar = model.MontoFinanciar,                    
                    TieneInicial = model.TieneInicial,
                    
                    FechaSolicitud = localDate,
                    IsActive = true,
                    ModifiedOn = localDate                    
                };

                var res = FinanciamientoService.Instance.SaveFinanciamiento(financ);

                if (res) {                    
                    FinanciamientoService.Instance.sendEmailToUserTemplate(financ);
                    FinanciamientoService.Instance.sendEmailToAdmin(financ);
                }

                result.Data = new { Success = res };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }

        [HttpGet]
        public ActionResult PaginaPrincipal()
        {
            return new RedirectResult(@Url.Home());
        }



    }
}