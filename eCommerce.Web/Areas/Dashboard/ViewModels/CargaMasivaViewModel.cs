using eCommerce.Web.ViewModels;
using Remotion.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class CargaMasivaViewModel : PageViewModel
    {
        public int Exito { get; set; }
        public string Mensaje  { get; set; }
        public StringBuilder Validacion  { get; set; }
    }
}