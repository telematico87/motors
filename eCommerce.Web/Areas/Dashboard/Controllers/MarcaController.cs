using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Enums;
using eCommerce.Web.Areas.Dashboard.ViewModels;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
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

                var catalogos = CatalogoMarcaService.Instance.SearchCatalogosByMarcaID(ID.Value);
                List<string> catalogosId = new List<string>();

                catalogos.ForEach(c => {
                    //Añadir al array catalogosId
                    catalogosId.Add(c.CatalogoId.ToString());                                        
                });

                model.ID = marca.ID;
                model.Descripcion = marca.Descripcion;
                model.Resumen = marca.Resumen;
                model.URL = marca.URL;
                model.Picture = marca.Picture;
                model.PictureID = marca.PictureID;
                model.CatalogoID = marca.CatalogoID;
                model.CatalogoIDs = catalogosId;
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

                    var ids = model.CatalogoIDs;
                    List<CatalogoMarca> listCatalogos = new List<CatalogoMarca>();
                    if (ids.Count > 0)
                    {
                        for (var i = 0; i < ids.Count; i++)
                        {
                            CatalogoMarca catalogo = new CatalogoMarca();

                            catalogo.CatalogoId = Int32.Parse(ids[i]);
                            catalogo.MarcaId = marca.ID;
                            catalogo.ModifiedOn = DateTime.Now;
                            catalogo.IsActive = true;
                            listCatalogos.Add(catalogo);                            
                        }
                    }
                    if (!CatalogoMarcaService.Instance.UpdateCatalogoMarca(marca.ID, listCatalogos))
                    {
                        throw new Exception("No se pudo actualizar los Catalogos para esta Marca");
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

                    //Guardar Marca(s)
                    var ids = model.CatalogoIDs;
                    if (ids.Count > 0)
                    {
                        for (var i = 0; i < ids.Count; i++)
                        {
                            CatalogoMarca marcas = new CatalogoMarca();

                            marcas.CatalogoId = Int32.Parse(ids[i]);
                            marcas.MarcaId = marca.ID;
                            marcas.ModifiedOn = DateTime.Now;
                            marcas.IsActive = true;

                            if (!CatalogoMarcaService.Instance.SaveCatalogoMarca(marcas))
                            {
                                throw new Exception("No se pudo actualizar los Catalogos para esta Marca");
                            }
                        }
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
