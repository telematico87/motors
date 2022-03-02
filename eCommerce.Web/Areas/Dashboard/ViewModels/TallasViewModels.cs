using eCommerce.Entities;
using eCommerce.Web.ViewModels;
using System.Collections.Generic;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class TallasListingViewModel : PageViewModel
    {
        public List<Talla> Tallas { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class TallasActionViewModels : PageViewModel
    {
        public int ID { get; set; }
        public string Description { get; set; }

    }
}