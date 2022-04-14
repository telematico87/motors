using eCommerce.Entities;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class CatalogosListingViewModels : PageViewModel
    {
        public List<Catalogo> Catalogos { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class CatalogosActionViewModels : PageViewModel
    {
        public int ID { get; set; }
        public string Description { get; set; }

    }
}