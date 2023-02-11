using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class TallaService
    {
        #region Define as Singleton
        private static TallaService _Instance;

        public static TallaService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TallaService();
                }

                return (_Instance);
            }
        }

        private TallaService()
        {
        }
        #endregion



        public List<Talla> SearchTalla(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var talla = context.Tallas
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                talla = talla.Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            count = talla.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return talla.OrderBy(x => x.Orden).Skip(skipCount).Take(recordSize).ToList();
        }

        public List<Talla> AllTalla()
        {
            var context = DataContextHelper.GetNewContext();

            var tallas = context.Tallas
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            return tallas.OrderBy(x => x.Orden).ToList();
        }

        public Talla GetTallaByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Tallas.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }

        public bool SaveTalla(Talla Talla)
        {
            var context = DataContextHelper.GetNewContext();

            context.Tallas.Add(Talla);

            return context.SaveChanges() > 0;
        }

        public bool UpdateTalla(Talla talla)
        {
            var context = DataContextHelper.GetNewContext();

            talla.ModifiedOn = DateTime.Now;
            context.Entry(talla).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteTalla(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var talla = context.Tallas.Find(ID);

            talla.IsDeleted = true;

            context.Entry(talla).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }



    }
}
