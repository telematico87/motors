using eCommerce.Entities;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class MarcasListingViewModels : PageViewModel
    {
        public List<Marca> Marcas { get; set; } 
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class MarcasActionViewModel : PageViewModel
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public string Resumen { get; set; }
        public string URL { get; set; } 
        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }
        public List<Catalogo> Catalogos { get; set; } 
        public int CatalogoID { get; set; }
        public List<string> CatalogoIDs { get; set; }
    }
}