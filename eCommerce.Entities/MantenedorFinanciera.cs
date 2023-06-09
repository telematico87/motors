﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
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


        public List<MantenedorFinanciera> ListarAntiguedadLaboral()
        {

            List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
            lis.Add(new MantenedorFinanciera(1, "MENOR A 3 MESES"));
            lis.Add(new MantenedorFinanciera(2, "ENTRE 3 Y 6 MESES"));
            lis.Add(new MantenedorFinanciera(3, "ENTRE 6 MESES Y 1 AÑO"));
            lis.Add(new MantenedorFinanciera(3, "MÁS DE 1 AÑO"));
            return lis;
        }
        public List<MantenedorFinanciera> ListarEstadoCivil()
        {

            List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
            lis.Add(new MantenedorFinanciera(1, "SOLTERO"));
            lis.Add(new MantenedorFinanciera(2, "DIVORCIADO"));
            lis.Add(new MantenedorFinanciera(3, "CASADO"));
            lis.Add(new MantenedorFinanciera(4, "CONVIVIENTE"));
            lis.Add(new MantenedorFinanciera(5, "VIUDO"));

            return lis;
        }

        public List<MantenedorFinanciera> ListarTipoDocumento()
        {

            List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
            lis.Add(new MantenedorFinanciera(1, "DNI"));
            lis.Add(new MantenedorFinanciera(2, "C. EXTRANJERÍA"));
            lis.Add(new MantenedorFinanciera(3, "PASAPORTE"));            
            lis.Add(new MantenedorFinanciera(4, "CPP"));            
            lis.Add(new MantenedorFinanciera(5, "OTROS"));            

            return lis;
        }
        
        public List<MantenedorFinanciera> ListarFinancieras()
        {
            List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
            lis.Add(new MantenedorFinanciera(0, "CUALQUIERA"));
            lis.Add(new MantenedorFinanciera(1, "EFECTIVA"));
            lis.Add(new MantenedorFinanciera(2, "MIGRANTE"));
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
        
        public List<MantenedorFinanciera> ListarSituacionLaboral()
        {
            List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
            lis.Add(new MantenedorFinanciera(1, "FORMAL DEPENDIENTE (PLANILLA)"));
            lis.Add(new MantenedorFinanciera(2, "FORMAL INDEPENDIENTE (RUC 10)"));
            lis.Add(new MantenedorFinanciera(3, "INFORMAL DEPENDIENTE (CARTA DE TRABAJO)"));
            lis.Add(new MantenedorFinanciera(4, "INFORMAL INDEPENDIENTE (NEGOCIO O COMERCIO SIN RUC)"));
            lis.Add(new MantenedorFinanciera(5, "PERSONA JURÍDICA (RUC 20)"));
            return lis;
        }

        public List<MantenedorFinanciera> ListarRangoIngreso()
        {

            List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
            lis.Add(new MantenedorFinanciera(1, "S/930 - S/1,500"));
            lis.Add(new MantenedorFinanciera(2, "S/1,501 - S/3,000"));
            lis.Add(new MantenedorFinanciera(3, "S/3,001 - S/5,000"));
            lis.Add(new MantenedorFinanciera(3, "S/5,001 a más"));
            return lis;
        }

        public List<MantenedorFinanciera> ListarInteresCompra()
        {
            List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
            lis.Add(new MantenedorFinanciera(1, "Lo más pronto posible"));
            lis.Add(new MantenedorFinanciera(5, "Este mes"));
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
            lis.Add(new MantenedorFinanciera(4, "S/. 5,001 - o más"));
            return lis;
        }

        public List<MantenedorFinanciera> ListarTipoFinanciera()
        {

            List<MantenedorFinanciera> lis = new List<MantenedorFinanciera>();
            lis.Add(new MantenedorFinanciera(0, "CUALQUIERA"));
            lis.Add(new MantenedorFinanciera(1, "EFECTIVA"));
            lis.Add(new MantenedorFinanciera(2, "MIGRANTE"));
      
            return lis;
        }

        public string obtenerValor(string mantenedor, int codigo) {

            var model = new MantenedorFinanciera();
            switch (mantenedor)
            {
                case "EstadoCivil":
                    model = ListarEstadoCivil().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                case "TipoDocumento":
                    model = ListarTipoDocumento().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                case "TipoVivienda":
                    model = ListarTipoVivienda().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                case "RangoIngreso":
                    model = ListarRangoIngreso().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                case "InteresCompra":
                    model = ListarInteresCompra().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                case "MontoFinanciar":
                    model = ListarMontoFinanciar().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                case "TipoFinanciera":
                    model = ListarTipoFinanciera().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                case "AntiguedadLaboral":
                    model = ListarAntiguedadLaboral().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                case "SituacionLaboral":
                    model = ListarSituacionLaboral().FirstOrDefault(d => d.Codigo == codigo);
                    break;
                default:
                    model.Valor = "";
                    break;
            }
            
            if (model == null) {
                return "";
            }
            return model.Valor.ToUpper().Trim();        
        } 
    }
}
