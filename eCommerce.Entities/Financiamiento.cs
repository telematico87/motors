using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Entities
{
    public class Financiamiento : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public int TipoDocumento { get; set; } 
        public string NroDocumento { get; set; }
        public string SituacionSentimental { get; set; }

        public int InteresCompra { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public int TipoVivienda { get; set; }
        public string SituacionLaboral { get; set; }
        public int AntiguedadLaboral { get; set; }

        public int IDMarca { get; set; }
        public string Marca { get; set; }
        public int IDModelo { get; set; }
        public string Modelo { get; set; } 
        public decimal Precio { get; set; } 
        public decimal IngresoNeto { get; set; }
        public decimal MontoInicial { get; set; }
        public decimal MontoAFinanciar { get; set; }
        public string Ocupacion { get; set; }
        public int TipoFinanciera { get; set; } //0: Cualquiera, 1: Efectiva, 2: Migrante
        public bool PoliticaPrivacidad { get; set; }
        public bool AceptoComunicaciones { get; set; }

        //Desfasados
        public int RangoIngreso { get; set; }        
        public int MontoFinanciar { get; set; }         
        public bool TieneInicial { get; set; }       
        public DateTime FechaSolicitud { get; set; }        

    }
}
