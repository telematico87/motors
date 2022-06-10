using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class UbigeoServices
    {
        #region Define as Singleton
        private static UbigeoServices _Instance;

        public static UbigeoServices Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UbigeoServices();
                }

                return (_Instance);
            }
        }

        private UbigeoServices()
        {
        }
        #endregion

        public List<Ubigeo> ListarDepartamento()
        {
            //select*
            //from[dbo].[Ubigeos]
            //            where CodProv = '00'
            //and CodDist = '00'
            //and CodPais = '01'
            //and CodDep != '00'
            //order by CodUbigeo;

            var context = DataContextHelper.GetNewContext();
            var depar = context.Ubigeos.Where(x => x.CodProv == "00" && x.CodDist == "00" && x.CodPais == "01" && x.CodDep != "00" );
            var ret = depar.ToList();
            return ret;
        }

        public List<Ubigeo> ListarProvincia(string CodDep)
        {
            //select*
            //from[dbo].[Ubigeos]
            //            where
            //CodProv != '00'
            //and CodDist = '00'
            //and CodPais = '01'
            //and CodDep = '22'
            //order by CodUbigeo;

            var context = DataContextHelper.GetNewContext();
            var depar = context.Ubigeos.Where(x => x.CodProv != "00" && x.CodDist == "00" && x.CodPais == "01" && x.CodDep == CodDep);
            var ret = depar.ToList();
            return ret;
        }

         

    }
}
