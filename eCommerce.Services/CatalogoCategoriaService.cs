using eCommerce.Entities;
using eCommerce.Entities.Response;
using eCommerce.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class CatalogoCategoriaService
    {
        #region Define as Singleton
        private static CatalogoCategoriaService _Instance;

        public static CatalogoCategoriaService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CatalogoCategoriaService();
                }

                return (_Instance);
            }
        }

        private CatalogoCategoriaService()
        {
        }
        #endregion

        public bool SaveCatalogoCategoria(CatalogoCategoria Catalogo)
        {
            var context = DataContextHelper.GetNewContext();
            context.CatalogoCategorias.Add(Catalogo);
            return context.SaveChanges() > 0;
        }

        public bool UpdateCatalogoCategoria(int categoryId, List<CatalogoCategoria> newCatalogos)
        {
            var context = DataContextHelper.GetNewContext();

            var oldCatalogos = context.CatalogoCategorias.Where(p => p.CategoriaId == categoryId);

            context.CatalogoCategorias.RemoveRange(oldCatalogos);


            context.CatalogoCategorias.AddRange(newCatalogos);

            return context.SaveChanges() > 0;
        }

        public List<CatalogoCategoria> SearchCategoriesByCatalogoID(int CatalogId)
        {
            var context = DataContextHelper.GetNewContext();

            var catalogosCategorias = context.CatalogoCategorias
                                .Where(x => !x.IsDeleted && x.CatalogoId == CatalogId)
                                .AsQueryable();

            return catalogosCategorias.OrderByDescending(x => x.ID).ToList();
        }

        public List<CatalogoCategoria> SearchCatalogosByCategoryID(int CategoryId)
        {
            var context = DataContextHelper.GetNewContext();

            var catalogosCategorias = context.CatalogoCategorias
                                .Where(x => !x.IsDeleted && x.CategoriaId == CategoryId)
                                .AsQueryable();

            return catalogosCategorias.OrderByDescending(x => x.ID).ToList();
        }        
    }
}
