﻿using eCommerce.Entities;
using eCommerce.Shared.Helpers;
using OfficeOpenXml;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseContext = System.ComponentModel.LicenseContext;

namespace eCommerce.Services
{
    public class FinanciamientoService
    {
        #region Define as Singleton
        private static FinanciamientoService _Instance;

        public static FinanciamientoService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new FinanciamientoService();
                }

                return (_Instance);
            }
        }

        private FinanciamientoService()
        {
        }
        #endregion

        public List<Financiamiento> BuscarFinanciamiento(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            var context = DataContextHelper.GetNewContext();

            var finac = context.Financiamientos
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                finac = finac.Where(x => x.Nombre.ToLower().Contains(searchTerm.ToLower()));
            }

            count = finac.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return finac.OrderByDescending(x => x.Nombre).Skip(skipCount).Take(recordSize).ToList();
        }
        
        public List<Financiamiento> ListarFinanciamiento()
        {
            var context = DataContextHelper.GetNewContext();

            var finac = context.Financiamientos
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();
            
            return finac.OrderByDescending(x => x.Nombre).ToList();
        }       

        public Financiamiento GetFinanciamientoByID(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            return context.Financiamientos.FirstOrDefault(x => !x.IsDeleted && x.ID == ID);
        }


        public bool SaveFinanciamiento(Financiamiento Financiamiento)
        {
            var context = DataContextHelper.GetNewContext();

            context.Financiamientos.Add(Financiamiento);

            return context.SaveChanges() > 0;
        }        

        public void sendEmailToUserTemplate(Financiamiento f)
        {
            //Envio al Usuario
            MantenedorFinanciera m = new MantenedorFinanciera();
            var templateId = "d-b354ccd66c3e4bd1a8f00452435641f5";
            var financiera = f.TipoFinanciera.Equals(1) ? "Efectiva" : "Migrante";
            var asunto = "BM3 Motos - Recibimos tu Solicitud de Financiamiento";
            var nombreCompleto = f.Nombre.Trim() + " " + f.Apellido.Trim();
            var montoFinanciar = m.ListarMontoFinanciar().FirstOrDefault(d => d.Codigo == f.MontoFinanciar);

            var dynamicTemplateData = new
            {
                asunto = asunto,
                nombre = nombreCompleto,
                modelo = f.Modelo,
                marca = f.Marca,
                financiamiento = montoFinanciar.Valor,
                financiera = financiera
            };

            new EmailService().SendToEmailAsyncTemplate(ConfigurationsHelper.SendGrid_FromEmailAddressName, 
                ConfigurationsHelper.SendGrid_FromEmailAddress, f.Correo, templateId, dynamicTemplateData);
        }

        public void sendEmailToAdmin(Financiamiento financiamiento)
        {
            MantenedorFinanciera m = new MantenedorFinanciera();
            
            var tipoDoc = m.ListarTipoDocumento().FirstOrDefault(d => d.Codigo == financiamiento.TipoDocumento);
            var montoFinanciar = m.obtenerValor("MontoFinanciar", financiamiento.MontoFinanciar);
            var rangoIngreso = m.obtenerValor("RangoIngreso", financiamiento.RangoIngreso);
            var interesCompra = m.obtenerValor("InteresCompra", financiamiento.InteresCompra);
            var tipoVivienda = m.obtenerValor("TipoVivienda", financiamiento.TipoVivienda);
            var financiera = m.obtenerValor("TipoFinanciera", financiamiento.TipoFinanciera);
            var nombreCompleto = financiamiento.Nombre.Trim() + " " + financiamiento.Apellido.Trim();            
            var nroDocumento = tipoDoc.Valor.Trim() + " " + financiamiento.NroDocumento.Trim();

            //Envio al Administrador
            new EmailService().SendToEmailAsync(ConfigurationsHelper.SendGrid_FromEmailAddressName, ConfigurationsHelper.SendGrid_FromEmailAddress, ConfigurationsHelper.SendGrid_FromEmailAddress, "Solicitud de Financiamiento "+nombreCompleto,
            "<h1>BM3 Motos</h1>" +
            $"<h3>Solicitud de Financiamiento</h3>" +
            $"<h3>FINANCIERA { financiera } </h3>" +
            $"<p>Titular: <strong>{nombreCompleto}</strong></p>" +
            $"<p>Nro Documento:  <strong>{nroDocumento}</strong></p>" +
            $"<p>Email:{financiamiento.Correo}</p>" +
            $"<p>Celular: <strong>{financiamiento.Celular} </strong></p>" +
            $"<p>Modelo: <strong>{financiamiento.Modelo} </strong></p>" +
            $"<p>Marca: <strong>{financiamiento.Marca} </strong></p>" +
            $"<p>Rango de Ingresos: <strong>{rangoIngreso}</strong></p>" +
            $"<p>Inicial: <strong>{financiamiento.MontoInicial}</strong></p>" +
            $"<p>Monto a Financiar: <strong>{montoFinanciar}</strong></p>" +             
            $"<p>Interés de Compra: <strong>{interesCompra}</strong></p>" +             
            $"<p>Tipo Vivienda: <strong>{tipoVivienda}</strong></p>" +             
            $"<p>Situación Laboral: <strong>{financiamiento.SituacionLaboral}</strong></p>" +             
            $"<p>Estado Civil: <strong>{financiamiento.SituacionSentimental}</strong></p>" +                         
            $"<p>Departamento: <strong>{financiamiento.Departamento}</strong></p>" + 
            $"<p>Provincia: <strong>{ financiamiento.Provincia}</strong></p>"            
            );
        }

        public bool UpdateFinanciamiento(Financiamiento financiamiento)
        {
            var context = DataContextHelper.GetNewContext();

            financiamiento.ModifiedOn = DateTime.Now;
            context.Entry(financiamiento).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteFinanciamiento(int ID)
        {
            var context = DataContextHelper.GetNewContext();

            var financiamiento = context.Financiamientos.Find(ID);

            financiamiento.IsDeleted = true;

            context.Entry(financiamiento).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
    }
}
