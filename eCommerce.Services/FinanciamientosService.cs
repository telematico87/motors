using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class FinanciamientosService
    {
        #region Define as Singleton
        private static FinanciamientosService _Instance;

        public static FinanciamientosService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FinanciamientosService();
                }

                return (_Instance);
            }
        }

        private FinanciamientosService()
        {
        }
        #endregion
        public List<Financiamiento> GetAllFinanciamientos(int? pageNo = 1, int? recordSize = 0)
        {
            var context = DataContextHelper.GetNewContext();

            var financiamientos = context.Financiamientos
                                    .Where(x => !x.IsDeleted && x.IsActive)
                                    .OrderBy(x => x.ID)
                                    .AsQueryable();

            if (recordSize.HasValue && recordSize.Value > 0)
            {
                pageNo = pageNo ?? 1;
                var skip = (pageNo.Value - 1) * recordSize.Value;

                financiamientos = financiamientos.Skip(skip)
                                   .Take(recordSize.Value);
            }

            return financiamientos.ToList();
        }

        public Financiamiento GetFinanciamientoByID(int ID, bool activeOnly = true)
        {
            var context = DataContextHelper.GetNewContext();

            var financiamiento = context.Financiamientos.FirstOrDefault(x => x.ID == ID);

            if (activeOnly)
            {
                return financiamiento != null && !financiamiento.IsDeleted && financiamiento.IsActive ? financiamiento : null;
            }
            else return financiamiento != null && !financiamiento.IsDeleted ? financiamiento : null;
        }

        public bool SaveFinanciamiento(Financiamiento financiamiento)
        {
            var context = DataContextHelper.GetNewContext();

            context.Financiamientos.Add(financiamiento);

            return context.SaveChanges() > 0;
        }

        public bool UpdateFinanciamiento(Financiamiento financiamiento)
        {
            var context = DataContextHelper.GetNewContext();

            var existingFinanciamiento = context.Financiamientos.Find(financiamiento.ID);

            context.Entry(existingFinanciamiento).CurrentValues.SetValues(financiamiento);

            return context.SaveChanges() > 0;
        }

        public bool DeleteFinanciamiento(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var financiamiento = context.Financiamientos.Find(ID);

            financiamiento.IsDeleted = true;

            context.Entry(financiamiento).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

    }
}
