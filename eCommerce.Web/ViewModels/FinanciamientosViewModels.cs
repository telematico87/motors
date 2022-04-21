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

        public List<EstadoCivil> listaEstadoCivil { get; set; }

        public List<Marca> listaMarca { get; set; }



        public class EstadoCivil
        {
            public int Codigo { get; set; }
            public string Valor { get; set; }

            public EstadoCivil() { }

            public EstadoCivil(int codigo, string valor)
            {
                this.Codigo = codigo;
                this.Valor = valor;
            }


            public List<EstadoCivil> ListarEstadoCivil()
            {

                List<EstadoCivil> lis = new List<EstadoCivil>();
                lis.Add(new EstadoCivil(1, "SOLTERO"));
                lis.Add(new EstadoCivil(2, "DIVORCIADO"));
                lis.Add(new EstadoCivil(3, "CASADO"));
                lis.Add(new EstadoCivil(4, "CONVIVIENTE"));

                return lis;
            }

        }
      
    }
}