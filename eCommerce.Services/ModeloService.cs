using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class ModeloService
    {
        #region Define as Singleton
        private static ModeloService _Instance;

        public static ModeloService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ModeloService();
                }

                return (_Instance);
            }
        }

        private ModeloService()
        {
        }
        #endregion

        public List<Modelo> SearchModelo(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var modelo = context.Modelos
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                modelo = modelo.Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            count = modelo.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return modelo.OrderByDescending(x => x.Description).Skip(skipCount).Take(recordSize).ToList();
        }


        public Modelo GetModeloByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Modelos.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveModelo(Modelo Modelo)
        {
            var context = DataContextHelper.GetNewContext();

            context.Modelos.Add(Modelo);

            return context.SaveChanges() > 0;
        }

        public bool UpdateModelo(Modelo modelo)
        {
            var context = DataContextHelper.GetNewContext();

            modelo.ModifiedOn = DateTime.Now;
            context.Entry(modelo).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteModelo(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var modelos = context.Modelos.Find(ID);

            modelos.IsDeleted = true;

            context.Entry(modelos).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
    }
}
