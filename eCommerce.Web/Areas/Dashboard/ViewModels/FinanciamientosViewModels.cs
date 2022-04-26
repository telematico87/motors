using eCommerce.Entities;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class FinanciamientosListingViewModels : PageViewModel
    {
        public List<Financiamiento> Financiamientos { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class FinanciamientosActionViewModels : PageViewModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; } 
        public string Apellido { get; set; } 
        public string Celular { get; set; }
        public string Correo { get; set; }
        public int TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; } 
        public int TipoVivienda { get; set; }
        public string SituacionLaboral { get; set; }
        public string SituacionSentimental { get; set; }
        public int RangoIngreso { get; set; }
        public int MontoFinanciar { get; set; }
        public int TipoFinanciera { get; set; } //1: Efectiva, 2: CajaSullana, 3: Santander


    }
}