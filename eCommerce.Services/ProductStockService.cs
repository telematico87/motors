using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class ProductStockService
    {

        #region Define as Singleton
        private static ProductStockService _Instance;

        public static ProductStockService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ProductStockService();
                }

                return (_Instance);
            }
        }

        //private CategoriesService()
        //{
        //}

        public ProductStockService()
        {
        }
        #endregion

        public List<ProductStock> GetProductStockByProductID(int productId)
        {
            var context = DataContextHelper.GetNewContext();
            var product = context.ProductStocks.Where(p => p.ProductID == productId);
            return product.ToList();
        }

        public bool SaveProductStock(ProductStock obj)
        {
            var context = DataContextHelper.GetNewContext();

            context.ProductStocks.Add(obj);

            return context.SaveChanges() > 0;
        }

        public bool SaveProductStockRange(int productID, List<ProductStock> stocks)
        {
            var context = DataContextHelper.GetNewContext();

            var oldStocks = context.ProductStocks.Where(p => p.ProductID == productID);

            context.ProductStocks.RemoveRange(oldStocks);

            context.ProductStocks.AddRange(stocks);

            return context.SaveChanges() > 0;
        }

        public List<ProductStock> GetProductStocksByTallaIDs(int ProductID, List<int> IDs)
        {
            var context = DataContextHelper.GetNewContext();
            var listStocks = context.ProductStocks.Where(p => p.ProductID == ProductID).ToList();   
            return listStocks.Where(r => IDs.Contains(r.TallaID)).OrderBy(x=> x.TallaID).ToList();
        }


    }
}
