using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class FinanciamientoService
    {
        #region Define as Singleton
        private static FinanciamientoService _Instance;

        public static FinanciamientoService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FinanciamientoService();
                }

                return (_Instance);
            }
        }

        private FinanciamientoService()
        {
        }
        #endregion

        public List<Financiamiento> BuscarFinanciamiento(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var finac = context.Financiamientos
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                finac = finac.Where(x => x.Nombre.ToLower().Contains(searchTerm.ToLower()));
            }

            count = finac.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return finac.OrderByDescending(x => x.Nombre).Skip(skipCount).Take(recordSize).ToList();
        }
         

        public Financiamiento GetFinanciamientoByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Financiamientos.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveFinanciamiento(Financiamiento Financiamiento)
        {
            var context = DataContextHelper.GetNewContext();

            context.Financiamientos.Add(Financiamiento);

            return context.SaveChanges() > 0;
        }

        public bool UpdateFinanciamiento(Financiamiento financiamiento)
        {
            var context = DataContextHelper.GetNewContext();

            financiamiento.ModifiedOn = DateTime.Now;
            context.Entry(financiamiento).State = System.Data.Entity.EntityState.Modified;

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
