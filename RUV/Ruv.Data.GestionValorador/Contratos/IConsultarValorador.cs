using System;
using System.Collections.Generic;
using System.Data;
using Ruv.Business.DTO.GestionValorador;

namespace Ruv.Data.GestionValorador.Contratos
{
    public interface IConsultarValorador
    {
        List<clsGestionValorador> ConsultaGestionVal(int PaginaNumber, int SizePagina, ref string cError);
        List<clsDetalleGestionVal> DetalleGestionValorador(int NIdValorador, DateTime FechaConsulta, int PaginaNumber, int SizePagina, ref string cError);
        int ConsultaValoradorCount(ref string cError);
        int DetalleValoradorCount(int NIdValorador, DateTime FechaConsulta, ref string cError);
    }
}
