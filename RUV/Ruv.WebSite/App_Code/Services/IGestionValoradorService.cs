using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using dto = Ruv.Business.DTO.GestionValorador;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGestionValoradorService" in both code and config file together.
[ServiceContract]
public interface IGestionValoradorService
{
	[OperationContract]
    List<dto::clsGestionValorador> CargaDatosValorador(int PaginaNumber, int SizePagina, ref string cError);
    [OperationContract]
    List<dto::clsDetalleGestionVal> DetalleGestionValorador(int NIdValorador, DateTime FechaSolicitada, int PaginaNumber, int SizePagina, ref string cError);
    [OperationContract]
    int ContadorValoradores(ref string cError);
    int DetalleValoradorContador(int NIdValorador, DateTime FechaSolicitada, ref string cError);
}
