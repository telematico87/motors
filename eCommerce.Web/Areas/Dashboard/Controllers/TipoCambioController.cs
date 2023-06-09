﻿using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Enums;
using eCommerce.Shared.Helpers;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class TipoCambioController : DashboardBaseController
    {        
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            var pageSize = (int)RecordSizeEnums.Size10;

            TipoCambiosListingViewModel model = new TipoCambiosListingViewModel
            {
                SearchTerm = searchTerm
            };

            model.TCambios = TipoCambioService.Instance.SearchTipoCambio(searchTerm, pageNo, pageSize, out int count);

            model.Pager = new Pager(count, pageNo, pageSize);

            return View(model);
        }


        [HttpGet]
        public ActionResult Action(int? ID)
        {
            TipoCambiosActionViewModels model = new TipoCambiosActionViewModels();

            if (ID.HasValue)
            {
                var tcambio = TipoCambioService.Instance.GetTipoCambioByID(ID.Value);

                if (tcambio == null) return HttpNotFound();

                string ventaStr = tcambio.Venta.ToString();
                ventaStr = ventaStr.Replace(",", ".");

                model.ID = tcambio.ID;
                model.Venta = ventaStr;
                model.Fecha = tcambio.Fecha;
            }

            return View(model);
        }
       

        [HttpPost]
        public JsonResult Action(TipoCambiosActionViewModels model)
        {
            JsonResult json = new JsonResult();

            try
            {
                if (model.ID > 0)
                {
                    var tcambio = TipoCambioService.Instance.GetTipoCambioByID(model.ID);

                    if (tcambio == null)
                    {
                        throw new Exception("Tipo Cambio No Encontrado".LocalizedString());
                    }

                    string ventaStr = model.Venta.Replace(".", ",");
                    //DateTime FechaTipoCambio = Convert.ToDateTime(model.Fecha.ToString("dd-MM-yyyy"));

                    Decimal Ventatmp2 = Convert.ToDecimal(ventaStr);
                    Decimal Ventatmp = Convert.ToDecimal(model.Venta);
                    
                    tcambio.ID = model.ID;
                    tcambio.Venta = Ventatmp;
                    tcambio.Compra = 0;
                    tcambio.Fecha = model.Fecha;

                    if (!TipoCambioService.Instance.UpdateTipoCambio(tcambio))
                    {
                        throw new Exception("No se puede actualizar el tipo de cambio".LocalizedString());
                    }
                    json.Data = new { Success = true };
                }
                else
                {       
                    //string ventaStr = model.Venta.Replace(".", ",");
                    //DateTime FechaTipoCambio = Convert.ToDateTime(model.Fecha.ToString("dd/MM/yyyy"));                    
                    Decimal venta = Convert.ToDecimal(model.Venta);
                    Decimal compra = model.Compra;
                    TipoCambio tcambios = new TipoCambio
                    {
                        ID = model.ID,
                        Venta = venta,
                        Compra = compra,
                        Fecha = model.Fecha,
                        ModifiedOn = DateTime.Now
                    };

                    if (!TipoCambioService.Instance.SaveTipoCambio(tcambios))
                    {
                        throw new Exception("No se puede crear el tipo de cambio".LocalizedString());
                    }                                     
                    json.Data = new { Success = true };
                }
            }
            catch (Exception ex)
            {                
                LogSystemService.Instance.Save(ex.Message);
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
                var operation = TipoCambioService.Instance.DeleteTipoCambio(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "No se puede eliminar el tipo de cambio".LocalizedString() };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }
    }
}