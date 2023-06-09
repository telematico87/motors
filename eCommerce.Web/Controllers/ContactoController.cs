﻿using eCommerce.Entities;
using eCommerce.Services;
using eCommerce.Shared.Helpers;
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
                result.Data = new { Success = res , Codigo = 200};
                //Envió de Correos
                if (res == true )
                {
                    DateTime FechaContacto = Convert.ToDateTime(contacto.Fecha);
                    string fContactoString = FechaContacto.ToString("dd-MM-yyyy");

                    //enviò al cliente
                   // new EmailService().SendToEmailAsync(ConfigurationsHelper.SendGrid_FromEmailAddressName, ConfigurationsHelper.SendGrid_FromEmailAddress, model.Email, "Contacto BM3",
                   //"" + "<br>" +
                   //$"<p><strong>Nombre:</strong> {model.Nombre}</p>" +
                   //$"<p><strong>Email:</strong> {model.Email} <strong></p>" +
                   //$"<p><strong>Asunto:</strong> {model.Asunto}<strong></p>" +
                   //$"<p><strong>Mensaje:</strong> {model.Mensaje}</p>" +
                   //$"<p><strong>Fecha:</strong> {fContactoString}</p>"
                   //);


                    //Envio al Administrador
                    new EmailService().SendToEmailAsync(ConfigurationsHelper.SendGrid_FromEmailAddressName, ConfigurationsHelper.SendGrid_FromEmailAddress, ConfigurationsHelper.SendGrid_FromEmailAddress, "Contacto BM3",
                    "Formulario de Contacto Ecommerce BM3" + "<br>" +
                    $"<p><strong>Nombre:</strong> {model.Nombre}</p>" +
                    $"<p><strong>Email:</strong> {model.Email} <strong></p>" +
                    $"<p><strong>Asunto:</strong> {model.Asunto}<strong></p>" +
                    $"<p><strong>Mensaje:</strong> {model.Mensaje}</p>" +
                    $"<p><strong>Fecha:</strong> {fContactoString}</p>"
                    );
                }


            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }


    }
}