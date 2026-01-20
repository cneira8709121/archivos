using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using Ruv.Infrastructure.Crosscutting.Resources.Dependencias;
using util = Ruv.Infrastructure.Crosscutting.Utilities;
using dto = Ruv.Business.DTO.GestionValorador;
using Ruv.Business.GestionValorador.Contratos;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GestionValoradorService" in code, svc and config file together.
public class GestionValoradorService : IGestionValoradorService
{
    public List<dto::clsGestionValorador> CargaDatosValorador(int PaginaNumber, int SizePagina, ref string cError)
    {
        IGestionValorador iGestionValorador = (IGestionValorador)new Ruv.Business.GestionValorador.GestionValorador();
        return iGestionValorador.ConsultaGestionVal(PaginaNumber, SizePagina, ref cError);
    }

    public List<dto::clsDetalleGestionVal> DetalleGestionValorador(int NIdValorador, DateTime FechaSolicitada, int PaginaNumber, int SizePagina, ref string cError)
    {
        IGestionValorador iGestionValorador = (IGestionValorador)new Ruv.Business.GestionValorador.GestionValorador();
        return iGestionValorador.DetalleGestionValorador(NIdValorador, FechaSolicitada, PaginaNumber, SizePagina, ref cError);
    }

    public int ContadorValoradores(ref string cError)
    {
        IGestionValorador iGestionValorador = (IGestionValorador)new Ruv.Business.GestionValorador.GestionValorador();
        return iGestionValorador.ContadorValoradores(ref cError);
    }

    public int DetalleValoradorContador(int NIdValorador, DateTime FechaSolicitada, ref string cError)
    {
        IGestionValorador iGestionValorador = (IGestionValorador)new Ruv.Business.GestionValorador.GestionValorador();
        return iGestionValorador.DetalleValoradorCount(NIdValorador,  FechaSolicitada, ref cError);
    } 
}
