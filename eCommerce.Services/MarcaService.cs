using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace eCommerce.Services
{
    public class MarcaService
    {

        string con = ConfigurationManager.ConnectionStrings["eCommerceConnectionString_OK"].ConnectionString;
        #region Define as Singleton
        private static MarcaService _Instance;

        public static MarcaService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MarcaService();
                }

                return (_Instance);
            }
        }

        private MarcaService()
        {
        }
        #endregion


        //public List<Catalogo> ListarCatalogos()
        //{

        //    //string consulta = "select * from Catalogoes";
        //    //var cata = new List<Catalogo>();
        //    //using (var connection = new SqlConnection(con))
	       //    // {
        //    //       connection.Open();
        //    //       var resul = con.Query<cata>.ToList();
	       //    // }
             
        //}

        public List<Marca> SearchMarca(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var marca = context.Marcas
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                marca = marca.Where(x => x.Descripcion.ToLower().Contains(searchTerm.ToLower()));
            }

            count = marca.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return marca.OrderByDescending(x => x.Descripcion).Skip(skipCount).Take(recordSize).ToList();
        }


        public Marca GetMarcaByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Marcas.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveMarca(Marca Marca)
        {
            var context = DataContextHelper.GetNewContext();

            context.Marcas.Add(Marca);

            return context.SaveChanges() > 0;
        }

        public bool UpdateMarca(Marca marca)
        {
            var context = DataContextHelper.GetNewContext();

            marca.ModifiedOn = DateTime.Now;
            context.Entry(marca).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteMarca(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var marcas = context.Marcas.Find(ID);

            marcas.IsDeleted = true;

            context.Entry(marcas).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
    }
}
