using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class TablaMasterService
    {
        #region Define as Singleton
        private static TablaMasterService _Instance;

        public static TablaMasterService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TablaMasterService();
                }

                return (_Instance);
            }
        }

        private TablaMasterService()
        {
        }
        #endregion


        public List<TablaMaster> GetTablaMasterByTipoTabla(string Tabla)
        {            
            var context = DataContextHelper.GetNewContext();

            var tablaMaster = context.TablaMasters
                                    .Where(x => x.TipoTabla == Tabla && !x.IsDeleted)
                                    .OrderBy(x => x.ID)
                                    .AsQueryable();

            return tablaMaster.ToList();
        }

    }
}
