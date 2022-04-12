using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class CatalogoService
    {
        #region Define as Singleton
        private static CatalogoService _Instance;

        public static CatalogoService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CatalogoService();
                }

                return (_Instance);
            }
        }

        private CatalogoService()
        {
        }
        #endregion

        public List<Catalogo> SearchCatalogo(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var catalogo = context.Catalogos
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                catalogo = catalogo.Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            count = catalogo.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return catalogo.OrderByDescending(x => x.Description).Skip(skipCount).Take(recordSize).ToList();
        }


        public Catalogo GetCatalogoByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Catalogos.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveCatalogo(Catalogo Catalogo)
        {
            var context = DataContextHelper.GetNewContext();

            context.Catalogos.Add(Catalogo);

            return context.SaveChanges() > 0;
        }

        public bool UpdateCatalogo(Catalogo catalogo)
        {
            var context = DataContextHelper.GetNewContext();

            catalogo.ModifiedOn = DateTime.Now;
            context.Entry(catalogo).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteCatalogo(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var catalogo = context.Catalogos.Find(ID);

            catalogo.IsDeleted = true;

            context.Entry(catalogo).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
    }
}
