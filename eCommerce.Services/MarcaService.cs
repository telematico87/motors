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

        public MarcaService()
        {
        }
        #endregion                

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

        public List<Marca> ListarMarca()
        {
            var context = DataContextHelper.GetNewContext(); 
            return context.Marcas.ToList();
        }


        public Marca GetMarcaByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Marcas.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }

        public List<Marca> GetMarcaByCatalogoID(int CatalogoId, int? pageNo = 1, int? recordSize = 0)
        {
            var context = DataContextHelper.GetNewContext();            

            var marcas = from m in context.Marcas
                             join c in context.CatalogoMarcas on m.ID equals c.MarcaId
                             where c.CatalogoId == CatalogoId && !m.IsDeleted
                             orderby m.ID
                             select m;

            return marcas.ToList();
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
            var existingCategory = context.Marcas.Find(marca.ID); 
            context.Entry(existingCategory).CurrentValues.SetValues(marca);

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

        public Marca GetMarcaByName(string marcaName)
        {
            var context = DataContextHelper.GetNewContext();
            marcaName = marcaName.ToUpper().Trim();
            var marca = context.Marcas.FirstOrDefault(x => x.Descripcion.ToUpper().Trim().Equals(marcaName) && !x.IsDeleted);
            return marca != null ? marca : null;
        }
    }
}
