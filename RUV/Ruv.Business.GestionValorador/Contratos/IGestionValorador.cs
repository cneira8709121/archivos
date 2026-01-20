using Ruv.Business.DTO.GestionValorador;
using System.Collections.Generic;
using System;

namespace Ruv.Business.GestionValorador.Contratos
{
    public interface IGestionValorador
    {
        List<clsGestionValorador> ConsultaGestionVal(int PaginaNumber, int SizePagina, ref string cError);
        List<clsDetalleGestionVal> DetalleGestionValorador(int NIdValorador, DateTime FechaConsulta, int PaginaNumber, int SizePagina, ref string cError);
        int ContadorValoradores(ref string cError);
        int DetalleValoradorCount(int NIdValorador, DateTime FechaConsulta, ref string cError);
    }
}
