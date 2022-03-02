using eCommerce.Entities;
using eCommerce.Web.ViewModels;
using System.Collections.Generic;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class ColorsListingViewModel : PageViewModel
    {
        public List<Color> Colors { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class ColorsActionViewModels : PageViewModel
    {
        public int ID { get; set; }
        public string Description { get; set; }

    }

}