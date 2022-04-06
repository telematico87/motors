using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.ViewModels
{
    public class FinanciamientoViewModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string NroTelefono { get; set; }
        public string Departamento { get; set; }
        public string Documento { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Perfil { get; set; }
        public string SituacionLaboral { get; set; }
        public double RangoIngreso { get; set; }
        public bool AceptaTermino { get; set; }
        public int TipoFinanciera { get; set; } //1: Efectiva, 2: CajaSullana, 3: Santander
        public double CuotaInicial { get; set; }
        public string ActividadLaboral { get; set; }
        public string Direccion { get; set; }
        public string SituacionSentimental { get; set; }
    }
}