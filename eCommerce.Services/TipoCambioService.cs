using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class TipoCambioService
    {
        #region Define as Singleton
        private static TipoCambioService _Instance;

        public static TipoCambioService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TipoCambioService();
                }

                return (_Instance);
            }
        }

        private TipoCambioService()
        {
        }
        #endregion



        public List<TipoCambio> GetAllTipoCambio()
        {
            var context = DataContextHelper.GetNewContext();
            var tcambio = context.TipoCambios.ToList();
            return tcambio.ToList();
        }

         

        public List<TipoCambio> SearchTipoCambio(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var tcambio = context.TipoCambios
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                tcambio = tcambio.Where(x => x.Fecha.ToString().Contains(searchTerm.ToLower()));
            }

            count = tcambio.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return tcambio.OrderByDescending(x => x.Fecha).Skip(skipCount).Take(recordSize).ToList();
        }


        public TipoCambio GetTipoCambioByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.TipoCambios.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveTipoCambio(TipoCambio TipoCambio)
        {
            var context = DataContextHelper.GetNewContext();

            context.TipoCambios.Add(TipoCambio);

            return context.SaveChanges() > 0;
        }

        public bool UpdateTipoCambio(TipoCambio tcambio)
        {
            var context = DataContextHelper.GetNewContext();

            context.Entry(tcambio).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteTipoCambio(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var tcambios = context.TipoCambios.Find(ID);

            tcambios.IsDeleted = true;

            context.Entry(tcambios).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

    }
}
