using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Enums;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Web.ViewModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
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
                model.TieneInicial = financi.TieneInicial;
                model.MontoInicial = financi.MontoInicial;
                model.FechaSolicitud = financi.FechaSolicitud;
             
            }
            model.listaTipoDocumento = new MantenedorFinanciera().ListarTipoDocumento();
            model.listaEstadoCivil = new MantenedorFinanciera().ListarEstadoCivil();
            model.listaTipoVivienda = new MantenedorFinanciera().ListarTipoVivienda();
            model.listaRangoIngreso = new MantenedorFinanciera().ListarRangoIngreso();
            model.listaInteresCompra = new MantenedorFinanciera().ListarInteresCompra();
            model.listaMontoFinanciar = new MantenedorFinanciera().ListarMontoFinanciar();
            model.listaTipoFinanciera = new MantenedorFinanciera().ListarTipoFinanciera();            

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

        public void DownloadExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            MantenedorFinanciera m = new MantenedorFinanciera();
            List<Financiamiento> collection = FinanciamientoService.Instance.ListarFinanciamiento();
            
            ExcelPackage Ep = new ExcelPackage();
                 
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Cliente";
            Sheet.Cells["B1"].Value = "Email";
            Sheet.Cells["C1"].Value = "Celular";
            Sheet.Cells["D1"].Value = "Documento";
            Sheet.Cells["E1"].Value = "Departamento";
            Sheet.Cells["F1"].Value = "Provincia";
            Sheet.Cells["G1"].Value = "Marca";
            Sheet.Cells["H1"].Value = "Modelo";
            Sheet.Cells["I1"].Value = "Interes de Compra";
            Sheet.Cells["J1"].Value = "Tipo Vivienda";
            Sheet.Cells["K1"].Value = "Situación Laboral";
            Sheet.Cells["L1"].Value = "Rango de Ingresos";
            Sheet.Cells["M1"].Value = "Financiera";
            Sheet.Cells["N1"].Value = "Estado Civil";
            Sheet.Cells["O1"].Value = "Fecha Solicitud";
            int row = 2;
            foreach (var item in collection)
            {

                var tipoDoc = m.ListarTipoDocumento().FirstOrDefault(d => d.Codigo == item.TipoDocumento);
                var montoFinanciar = m.obtenerValor("MontoFinanciar", item.MontoFinanciar);
                var rangoIngreso = m.obtenerValor("RangoIngreso", item.RangoIngreso);
                var interesCompra = m.obtenerValor("InteresCompra", item.InteresCompra);
                var tipoVivienda = m.obtenerValor("TipoVivienda", item.TipoVivienda);
                var financiera = m.obtenerValor("TipoFinanciera", item.TipoFinanciera);
                var nombreCompleto = item.Nombre.Trim() + " " + item.Apellido.Trim();

                Sheet.Cells[string.Format("A{0}", row)].Value = nombreCompleto.ToUpper();
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Correo;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Celular;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.NroDocumento;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.Departamento.ToUpper();
                Sheet.Cells[string.Format("F{0}", row)].Value = item.Provincia.ToUpper();
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Marca.ToUpper();
                Sheet.Cells[string.Format("H{0}", row)].Value = item.Modelo.ToUpper();
                Sheet.Cells[string.Format("I{0}", row)].Value = interesCompra;
                Sheet.Cells[string.Format("J{0}", row)].Value = tipoVivienda;
                Sheet.Cells[string.Format("K{0}", row)].Value = item.SituacionLaboral.ToUpper();
                Sheet.Cells[string.Format("L{0}", row)].Value = rangoIngreso;
                Sheet.Cells[string.Format("M{0}", row)].Value = financiera;
                Sheet.Cells[string.Format("N{0}", row)].Value = item.SituacionSentimental.ToUpper();
                Sheet.Cells[string.Format("O{0}", row)].Value = item.FechaSolicitud.ToString("dd-MM-yyyy");
                Sheet.Cells[string.Format("O{0}", row)].Style.Numberformat.Format = "dd-MM-yyyy";
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "FinanciamientoReport.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

    }
     
}
