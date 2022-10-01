using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class ProductColorService
    {
        #region Define as Singleton
        private static ProductColorService _Instance;

        public static ProductColorService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ProductColorService();
                }

                return (_Instance);
            }
        }

        private ProductColorService()
        {
        }
        #endregion

        public List<ProductColor> SearchProductColorByProductId(int ProductID)
        {
            var context = DataContextHelper.GetNewContext();

           

            var productColor = context.ProductsColors.Include("Color").Include("Picture")
                                .Where(x => x.ProductID == ProductID && !x.IsDeleted)
                                .AsQueryable().ToList();
            return productColor;
        }
    }
}
