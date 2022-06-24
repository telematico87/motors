using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.ViewModels
{
    public class ContactosViewModels
    {
       
        public string Nombre { get; set; }  
        public string Email { get; set; } 
        public string Asunto { get; set; } 
        public string Mensaje { get; set; } 
        public DateTime Fecha { get; set; }
    }
}