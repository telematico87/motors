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
    public class ContactoController : PublicBaseController
    {
        
        public ActionResult Contacto()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GuardarContacto(ContactosViewModels model)
        {
            JsonResult result = new JsonResult(); 
            try
            { 
                var contacto = new Contacto

                {
                    Nombre = model.Nombre,
                    Email = model.Email,
                    Asunto = model.Asunto,
                    Mensaje = model.Mensaje,
                    Fecha = DateTime.Now,  
                };

                var res = ContactoService.Instance.SaveContacto(contacto); 
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