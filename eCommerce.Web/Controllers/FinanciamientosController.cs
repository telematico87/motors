using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Web.Controllers
{
    public class FinanciamientosController : Controller
    {
        // GET: Financiamientos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Financiamientos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Financiamientos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Financiamientos/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public JsonResult CreateFinanciamiento(FinanciamientoViewModel model)
        {
            JsonResult result = new JsonResult();

            try
            {
                var financiamiento = new Financiamiento
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    FechaNacimiento = model.FechaNacimiento,
                    Correo = model.Correo,
                    NroTelefono = model.NroTelefono,
                    Departamento = model.Departamento,
                    Documento = model.Documento,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    Perfil = model.Perfil,
                    SituacionLaboral = model.SituacionLaboral,
                    RangoIngreso = model.RangoIngreso,
                    AceptaTermino = model.AceptaTermino,
                    TipoFinanciera = model.TipoFinanciera,
                    CuotaInicial = model.CuotaInicial,
                    ActividadLaboral = model.ActividadLaboral,
                    Direccion = model.Direccion,
                    SituacionSentimental = model.SituacionSentimental                
                };

                var res = FinanciamientosService.Instance.SaveFinanciamiento(financiamiento);

                result.Data = new { Success = res };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }


        // GET: Financiamientos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Financiamientos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Financiamientos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Financiamientos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
