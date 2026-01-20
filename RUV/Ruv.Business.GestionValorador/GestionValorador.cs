using System.Collections.Generic;
using System.Linq;
using Ruv.Business.DTO.Reporteador;
using Ruv.Business.GestionValorador.Contratos;
using DataContract = Ruv.Data.GestionValorador.Contratos;
using Ruv.Business.DTO.GestionValorador;
using System;

namespace Ruv.Business.GestionValorador
{
    public class GestionValorador : IGestionValorador
    {
        public List<clsGestionValorador> ConsultaGestionVal(int PaginaNumber, int SizePagina, ref string cError)
        {
            DataContract.IConsultarValorador iConsulta = (DataContract.IConsultarValorador)new Ruv.Data.GestionValorador.ConsultarValorador();
            return iConsulta.ConsultaGestionVal(PaginaNumber, SizePagina, ref cError);
        }
        public List<clsDetalleGestionVal> DetalleGestionValorador(int NIdValorador, DateTime FechaConsulta, int PaginaNumber, int SizePagina, ref string cError)
        {

            DataContract.IConsultarValorador iConsulta = (DataContract.IConsultarValorador)new Ruv.Data.GestionValorador.ConsultarValorador();
            return iConsulta.DetalleGestionValorador(NIdValorador, FechaConsulta, PaginaNumber, SizePagina, ref cError);
        }

        public int ContadorValoradores(ref string cError)
        {
            DataContract.IConsultarValorador iConsulta = (DataContract.IConsultarValorador)new Ruv.Data.GestionValorador.ConsultarValorador();
            return iConsulta.ConsultaValoradorCount(ref cError);
        }

        public int DetalleValoradorCount(int NIdValorador, DateTime FechaConsulta, ref string cError)
        { 
            DataContract.IConsultarValorador iConsulta = (DataContract.IConsultarValorador)new Ruv.Data.GestionValorador.ConsultarValorador();
            return iConsulta.DetalleValoradorCount( NIdValorador, FechaConsulta, ref cError);
        }
    }
}
