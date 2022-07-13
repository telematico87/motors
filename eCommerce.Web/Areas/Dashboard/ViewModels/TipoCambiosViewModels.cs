using eCommerce.Entities;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class TipoCambiosListingViewModel : PageViewModel
    {
        public List<TipoCambio> TCambios { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class TipoCambiosActionViewModels : PageViewModel
    {
        public int ID { get; set; }
        public string Venta { get; set; }
        public decimal Compra { get; set; }
        public DateTime Fecha { get; set; }


    }
}