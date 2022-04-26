using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.ViewModels
{
    public class FinanciamientosViewModels
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Celular { get; set; }
        public string Correo { get; set; }
        public int TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public int InteresCompra { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int TipoVivienda { get; set; }
        public string SituacionLaboral { get; set; }
        public string SituacionSentimental { get; set; }
        public int RangoIngreso { get; set; }
        public bool PoliticaPrivacidad { get; set; }
        public bool AceptoComunicaciones { get; set; }
        public int MontoFinanciar { get; set; }
        public int TipoFinanciera { get; set; } //1: Efectiva, 2: CajaSullana, 3: Santander


        public List<Marca> listaMarca { get; set; }

        public List<Ubigeo> listaDepart { get; set; }


        public List<MantenedorFinanciera> listaEstadoCivil { get; set; }
        public List<MantenedorFinanciera> listaTipoVivienda { get; set; }
        public List<MantenedorFinanciera> listaRangoIngreso { get; set; }
        public List<MantenedorFinanciera> listaInteresCompra { get; set; }

        public List<MantenedorFinanciera> listaMontoFinanciar { get; set; }


        public class MantenedorFinanciera
        {
            public int Codigo { get; set; }
            public string Valor { get; set; }

            public MantenedorFinanciera() { }

            public MantenedorFinanciera(int codigo, string valor)
            {
                this.Codigo = codigo;
                this.Valor = valor;
            }


            public List<MantenedorFinanciera> ListarEstadoCivil()
            {

                List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
                lis.Add(new MantenedorFinanciera(1, "SOLTERO"));
                lis.Add(new MantenedorFinanciera(2, "DIVORCIADO"));
                lis.Add(new MantenedorFinanciera(3, "CASADO"));
                lis.Add(new MantenedorFinanciera(4, "CONVIVIENTE"));

                return lis;
            }

            public List<MantenedorFinanciera> ListarTipoVivienda()
            {

                List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
                lis.Add(new MantenedorFinanciera(1, "VIVIENDA ALQUILADA"));
                lis.Add(new MantenedorFinanciera(2, "VIVIENDA FAMILIAR"));
                lis.Add(new MantenedorFinanciera(3, "VIVIENDA PROPIA"));
                return lis;
            }

            public List<MantenedorFinanciera> ListarRangoIngreso()
            {

                List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
                lis.Add(new MantenedorFinanciera(1, "S/930 - S/1,500"));
                lis.Add(new MantenedorFinanciera(2, "S/1,501 - S/3,000"));
                lis.Add(new MantenedorFinanciera(3, "S/3,001 a más"));
                return lis;
            }

            public List<MantenedorFinanciera> ListarInteresCompra()
            {

                List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
                lis.Add(new MantenedorFinanciera(1, "Lo más pronto posible"));
                lis.Add(new MantenedorFinanciera(2, "1 a 2 meses"));
                lis.Add(new MantenedorFinanciera(3, "3 a 6 meses"));
                lis.Add(new MantenedorFinanciera(4, "7 meses a más"));
                return lis;
            }


            public List<MantenedorFinanciera> ListarMontoFinanciar()
            {

                List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
                lis.Add(new MantenedorFinanciera(1, "S/. 0 - S/. 1,000"));
                lis.Add(new MantenedorFinanciera(2, "S/. 1,001 - S/. 3,000"));
                lis.Add(new MantenedorFinanciera(3, "S/. 3,001 - S/. 5,000")); 
                return lis;
            }

        }

    }
}