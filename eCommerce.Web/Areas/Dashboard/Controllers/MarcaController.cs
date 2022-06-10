using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Enums;
using eCommerce.Shared.Helpers;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.Areas.Dashboard.Controllers
{
    public class MarcaController : DashboardBaseController
    {
       
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            var pageSize = (int)RecordSizeEnums.Size10;

            MarcasListingViewModels model = new MarcasListingViewModels
            {
                SearchTerm = searchTerm
            };

            model.Marcas = MarcaService.Instance.SearchMarca(searchTerm, pageNo, pageSize, out int count);

            model.Pager = new Pager(count, pageNo, pageSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            MarcasActionViewModel model = new MarcasActionViewModel();

            if (ID.HasValue)
            {
                var marca = MarcaService.Instance.GetMarcaByID(ID.Value);

                if (marca == null) return HttpNotFound();

                model.ID = marca.ID;
                model.Descripcion = marca.Descripcion;
                model.Resumen = marca.Resumen;
                model.URL = marca.URL;
                model.Picture = marca.Picture;
                model.PictureID = marca.PictureID;
                model.CatalogoID = marca.CatalogoID;
            }
             
            model.Catalogos = CatalogoService.Instance.ListarCatalogo();

            return View(model);
        }



        [HttpPost]
        public JsonResult Action(MarcasActionViewModel model)
        {
            JsonResult json = new JsonResult();

            try
            {
                if (model.ID > 0)
                {
                    var marca = MarcaService.Instance.GetMarcaByID(model.ID);

                    if (marca == null)
                    {
                        throw new Exception("Marca no encontrada");
                    }

                    marca.Descripcion = model.Descripcion;
                    marca.Resumen = model.Resumen;
                    marca.URL = model.URL;
                    marca.CatalogoID = model.CatalogoID;
                    marca.ModifiedOn = DateTime.Now;
                    marca.PictureID = model.PictureID;

                    if (!MarcaService.Instance.UpdateMarca(marca))
                    {
                        throw new Exception("No se puede actualizar la marca");
                    }
                    json.Data = new { Success = true };

                }
                else
                {
                    Marca marca = new Marca
                    {
                        ID = model.ID,
                        Descripcion = model.Descripcion,
                        Resumen = model.Resumen,
                        URL = model.URL,
                        CatalogoID = model.CatalogoID,
                        PictureID = model.PictureID      
                    };

                    if (!MarcaService.Instance.SaveMarca(marca))
                    {
                        throw new Exception("No se puede crear la marca");
                    }


                    json.Data = new { Success = true };
                }
            }
            catch (Exception ex)
            {
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
                var operation = MarcaService.Instance.DeleteMarca(ID);

                result.Data = new { Success = operation, Message = operation ? string.Empty : "No se puede eliminar la marca" };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = string.Format("{0}", ex.Message) };
            }

            return result;
        }

    }
}
