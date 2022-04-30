using eCommerce.Entities;
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
                model.listaEstadoCivil = new MantenedorFinanciera().ListarEstadoCivil();
                model.listaTipoVivienda = new MantenedorFinanciera().ListarTipoVivienda();
                model.listaRangoIngreso = new MantenedorFinanciera().ListarRangoIngreso();
                model.listaInteresCompra = new MantenedorFinanciera().ListarInteresCompra();
                model.listaMontoFinanciar = new MantenedorFinanciera().ListarMontoFinanciar();
                model.listaTipoDocumento = new MantenedorFinanciera().ListarTipoDocumento();
                
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
            FinanciamientosListingViewModels model2 = new FinanciamientosListingViewModels();
            FinanciamientosActionViewModels model = new FinanciamientosActionViewModels();

            if (ID.HasValue)
            {
                var financi = FinanciamientoService.Instance.GetFinanciamientoByID(ID.Value);

                if (financi == null) return HttpNotFound();

                model.ID = financi.ID;
                model.Nombre = financi.Nombre;
                model.Apellido = financi.Apellido;
                model.Correo = financi.Correo;
                model.Celular = financi.Celular;
                model.Departamento = financi.Departamento;
                model.Provincia = financi.Provincia;
                model.Marca = financi.Marca;
                model.NroDocumento = financi.NroDocumento;
                model.TipoVivienda = financi.TipoVivienda;
                model.SituacionLaboral = financi.SituacionLaboral;
                model.RangoIngreso = financi.RangoIngreso;
                model.SituacionSentimental = financi.SituacionSentimental;
                model.TipoDocumento = financi.TipoDocumento; 
                model.TipoFinanciera = financi.TipoFinanciera;
                model.MontoFinanciar = financi.MontoFinanciar;
             
            }
            model.listaTipoDocumento = new MantenedorFinanciera().ListarTipoDocumento();
            model.listaEstadoCivil = new MantenedorFinanciera().ListarEstadoCivil();
            model.listaTipoVivienda = new MantenedorFinanciera().ListarTipoVivienda();
            model.listaRangoIngreso = new MantenedorFinanciera().ListarRangoIngreso();
            model.listaInteresCompra = new MantenedorFinanciera().ListarInteresCompra();
            model.listaMontoFinanciar = new MantenedorFinanciera().ListarMontoFinanciar();
            model.listaTipoFinanciera = new MantenedorFinanciera().ListarTipoFinanciera();
            //model.Marca = MarcaService.Instance.ListarMarca();

            return View(model);
        }


        [HttpPost]
        public JsonResult Delete(int ID)
        {
            JsonResult result = new JsonResult();

            try
            {
                var operation = FinanciamientoService.Instance.DeleteFinanciamiento(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "No se puede eliminar" };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }

    }
     
}
