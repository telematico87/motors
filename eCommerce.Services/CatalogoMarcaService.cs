using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class CatalogoMarcaService
    {
        #region Define as Singleton
        private static CatalogoMarcaService _Instance;

        public static CatalogoMarcaService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CatalogoMarcaService();
                }

                return (_Instance);
            }
        }

        private CatalogoMarcaService()
        {
        }
        #endregion

        public List<CatalogoMarca> SearchCatalogosByMarcaID(int MarcaId)
        {
            var context = DataContextHelper.GetNewContext();

            var catalogoMarcas = context.CatalogoMarcas
                                .Where(x => !x.IsDeleted && x.MarcaId == MarcaId)
                                .AsQueryable();

            return catalogoMarcas.OrderByDescending(x => x.ID).ToList();
        }

        public List<CatalogoMarca> SearchMarcasByCatalogoID(int CatalogId)
        {
            var context = DataContextHelper.GetNewContext();

            var catalogoMarcas = context.CatalogoMarcas
                                .Where(x => !x.IsDeleted && x.CatalogoId == CatalogId)
                                .AsQueryable();

            return catalogoMarcas.OrderByDescending(x => x.ID).ToList();
        }

        public bool SaveCatalogoMarca(CatalogoMarca entity)
        {
            var context = DataContextHelper.GetNewContext();
            context.CatalogoMarcas.Add(entity);
            return context.SaveChanges() > 0;
        }

        public bool UpdateCatalogoMarca(int marcaId, List<CatalogoMarca> newCatalogos)
        {
            var context = DataContextHelper.GetNewContext();

            var oldCatalogos = context.CatalogoMarcas.Where(p => p.MarcaId == marcaId);

            context.CatalogoMarcas.RemoveRange(oldCatalogos);


            context.CatalogoMarcas.AddRange(newCatalogos);

            return context.SaveChanges() > 0;
        }
    }
}
