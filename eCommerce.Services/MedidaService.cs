using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class MedidaService
    {
        #region Define as Singleton
        private static MedidaService _Instance;

        public static MedidaService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MedidaService();
                }

                return (_Instance);
            }
        }

        private MedidaService()
        {
        }
        #endregion

        public List<Medida> SearchMedida(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var medida = context.Medidas
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                medida = medida.Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            count = medida.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return medida.OrderByDescending(x => x.Description).Skip(skipCount).Take(recordSize).ToList();
        }


        public Medida GetMedidaByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Medidas.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveMedida(Medida Medida)
        {
            var context = DataContextHelper.GetNewContext();

            context.Medidas.Add(Medida);

            return context.SaveChanges() > 0;
        }

        public bool UpdateMedida(Medida medida)
        {
            var context = DataContextHelper.GetNewContext();

            medida.ModifiedOn = DateTime.Now;
            context.Entry(medida).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteMedida(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var medidas = context.Medidas.Find(ID);

            medidas.IsDeleted = true;

            context.Entry(medidas).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
    }
}
