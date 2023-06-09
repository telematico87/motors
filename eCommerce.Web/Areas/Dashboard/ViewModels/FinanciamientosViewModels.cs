﻿using eCommerce.Entities;
using eCommerce.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCommerce.Web.Areas.Dashboard.ViewModels
{
    public class FinanciamientosListingViewModels : PageViewModel
    {
        public List<Financiamiento> Financiamientos { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }

        public List<MantenedorFinanciera> listaEstadoCivil { get; set; }
        public List<MantenedorFinanciera> listaTipoVivienda { get; set; }
        public List<MantenedorFinanciera> listaRangoIngreso { get; set; }
        public List<MantenedorFinanciera> listaInteresCompra { get; set; }

        public List<MantenedorFinanciera> listaMontoFinanciar { get; set; }
        public List<MantenedorFinanciera> listaTipoDocumento { get; set; }
    }

    public class FinanciamientosActionViewModels : PageViewModel
    {
        public int ID { get; set; }
        public string Nombre { get; set; } 
        public string Apellido { get; set; } 
        public string FechaNacimiento { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public int TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; } 
        public int TipoVivienda { get; set; }
        public int AntiguedadLaboral { get; set; }
        public string SituacionLaboral { get; set; }
        public string SituacionSentimental { get; set; }
        public int RangoIngreso { get; set; }
        public decimal IngresoNeto { get; set; }
        public decimal MontoAFinanciar { get; set; }
        public int MontoFinanciar { get; set; }
        public int TipoFinanciera { get; set; } //1: Efectiva, 2: CajaSullana, 3: Santander
        public bool TieneInicial { get; set; }
        public decimal MontoInicial { get; set; }
        public DateTime FechaSolicitud { get; set; }

        public List<MantenedorFinanciera> listaEstadoCivil { get; set; }
        public List<MantenedorFinanciera> listaTipoVivienda { get; set; }
        public List<MantenedorFinanciera> listaRangoIngreso { get; set; }
        public List<MantenedorFinanciera> listaInteresCompra { get; set; }

        public List<MantenedorFinanciera> listaMontoFinanciar { get; set; }
        public List<MantenedorFinanciera> listaTipoDocumento { get; set; }
        public List<MantenedorFinanciera> listaTipoFinanciera { get; set; }
        public List<MantenedorFinanciera> listaAntiguedadLaboral { get; set; }

        public MantenedorFinanciera financiera { get; set; }

        public List<Marca> Marcas { get; set; }
      

    }
}