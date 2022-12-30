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

        //public List<CategoryResponse> SearchCategoriesByCatalogoID(int CatalogId, int? pageNo, int recordSize, out int count)
        //{
        //    //Consultar Categorias de un Catalogo
        //    List<CategoryResponse> result = new List<CategoryResponse>();


        //    var context = DataContextHelper.GetNewContext();

        //    var catalogosCategorias = context.CatalogoCategorias
        //                        .Where(x => !x.IsDeleted && x.CatalogoId == CatalogId)
        //                        .AsQueryable();

        //    if (catalogosCategorias.Count() > 0) {

        //        //Traer valores de Categorias
        //        foreach(var c in catalogosCategorias) {

        //            var category = context.Categories.FirstOrDefault(x => !x.IsDeleted && x.ID == c.CategoriaId);

        //            if (category!= null) {
        //                var currentLanguageCategoryRecord = category.CategoryRecords.FirstOrDefault(x => x.LanguageID == AppDataHelper.CurrentLanguage.ID);

        //                CategoryResponse cat = new CategoryResponse();
        //                cat.CategoryID = category.ID;
        //                cat.Name = currentLanguageCategoryRecord.Name;
        //                result.Add(cat);
        //            }
        //        }               
        //    }

        public List<CatalogoCategoria> SearchCategoriesByCatalogoID(int CatalogId)
        {
            var context = DataContextHelper.GetNewContext();

            var catalogosCategorias = context.CatalogoCategorias
                                .Where(x => !x.IsDeleted && x.CatalogoId == CatalogId)
                                .AsQueryable();

            return catalogosCategorias.OrderByDescending(x => x.ID).ToList();
        }
    }
}
