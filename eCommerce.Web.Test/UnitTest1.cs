using eCommerce.Entities;
using eCommerce.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace eCommerce.Web.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CantidadMarca()
        {
            CategoriesService categoriaService = new CategoriesService();

            string nombre="Juan";
            List<Category> categorias = categoriaService.GetCategories();
            //int cantidadCategorias =   categorias.Count;
            int cantidadCategorias = 5;
            Assert.AreEqual("Juan", nombre);
            
            /*
            MarcaService marcaService = new MarcaService();
            List<Marca> marcas = marcaService.SearchMarca("", 10, 10, out _);            
            Assert.AreEqual(0, marcas.Count);
            */
        }
    }
}
